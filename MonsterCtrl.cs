using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE, PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;

    public GameObject hpBarPrefab;
    public Vector2 hpBarOffset = new Vector2(0.0f, 1.1f);
    public float hp;

    private MonsterHpBar monsterHpBar;
    private const string axeTag = "AXE";
    private float _damage = 10.0f;
    private float initHp = 100.0f;
    private Canvas uiCanvas;
    private GameObject hpBar;

    public float traceDist = 20.0f;
    public float attackDist = 1.0f;
    public bool isDie = false;
    public Collider2D[] punchs;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private GameObject hitEffect;
    private Animator anim;
    private readonly float damping = 10.0f;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("IsHit");
    private readonly int hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        hp = initHp;
        foreach (var item in punchs)
        {
            item.enabled = false;
        }
        SetHpBar();
    }

    private void OnEnable()
    {
        
        Vector2 rot = Vector2.up * (Random.Range(0, 12) * 30.0f);
        monsterTr.rotation = Quaternion.Euler(rot);
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        monsterHpBar = hpBar.GetComponent<MonsterHpBar>();
        monsterHpBar.targetTr = gameObject.transform;
        monsterHpBar.offset = hpBarOffset;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.1f);
            if (state == State.DIE) yield break;
            float dist = Vector2.Distance(playerTr.position, monsterTr.position);
            if (dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    foreach (var item in punchs)
                    {
                        item.enabled = false;
                    }
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.PATROL:
                    break;
                case State.TRACE:
                    foreach (var item in punchs)
                    {
                        item.enabled = false;
                    }
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:
                    foreach (var item in punchs)
                    {
                        item.enabled = true;
                    }
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, true);
                    break;
                case State.DIE:
                    if (hp <= 0.0f)
                    {

                    }
                    foreach (var item in punchs)
                    {
                        item.enabled = false;
                    }
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    GetComponent<Collider2D>().enabled = false;
                    yield return new WaitForSeconds(20.0f);
                    hp = initHp;
                    isDie = false;
                    GetComponent<Collider2D>().enabled = true;
                    state = State.IDLE;
                    gameObject.SetActive(false);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag(axeTag) && hp > 0)
        {
            coll.gameObject.SetActive(false);
            anim.SetTrigger(hashHit);
            Vector2 pos = coll.GetContact(0).point;
            hp -= _damage;
            StartCoroutine(monsterHpBar.DisplayHpBar(hp));
            if (hp <= 0)
            {
                state = State.DIE;
            }
        }
    }

    public void OnDamage()
    {
        anim.SetTrigger(hashHit);
        hp -= _damage;
        StartCoroutine(monsterHpBar.DisplayHpBar(hp));
        if (hp <= 0)
        {
            state = State.DIE;
        }
    }

    void ShowHitEffect(Vector2 pos)
    {
        GameObject _hitEffect = Instantiate<GameObject>(hitEffect, pos, monsterTr.rotation);
    }

    void Update()
    {
        if (agent.remainingDistance >= 2.0f)
        {
            Vector2 dir = agent.desiredVelocity;
            Quaternion rot = Quaternion.LookRotation(dir);
            monsterTr.rotation = Quaternion.Slerp(monsterTr.rotation, rot, Time.deltaTime * damping);
        }
        if (state == State.ATTACK)
        {
            Quaternion rot = Quaternion.LookRotation(playerTr.position - monsterTr.position);
            monsterTr.rotation = Quaternion.Slerp(monsterTr.rotation, rot, Time.deltaTime * damping);
        }
    }
}
