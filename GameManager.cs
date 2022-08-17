using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Transform> listPoints = new List<Transform>();
    public List<GameObject> monsterPool = new List<GameObject>();
    public List<GameObject> bossMonsterPool = new List<GameObject>();
    public GameObject player;
    public GameObject monster;
    public GameObject bossMonster;
    public float createTime = 3.0f;
    public int maxMonsters = 20;

    //private GameObject slotList;
    private bool isGameOver;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    public static GameManager instance = null;

    public List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();

    #region

    public int playerGold;
    public int playerDamage;
    public int playerDefence;
    public int playerCritical;
    public int skillDamage;
    public float skillRange;
    public float colldown;
    public float speed;
    public float drop;
    public float attackSpeed;
    public float dodge;

    #endregion

    public delegate void StageEndHandler();
    public static event StageEndHandler OnStageEnd;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        CreateMonster();
        
    }

    private void Start()
    {
        Transform spwanPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        foreach (Transform item in spwanPointGroup)
        {
            listPoints.Add(item);
        }
        InvokeRepeating("CreateMonster", 2.0f, createTime);
        
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, listPoints.Count);
        GameObject _monster = GetMonsterInPool();
        _monster?.transform.SetPositionAndRotation(listPoints[idx].position, listPoints[idx].rotation);
        _monster?.SetActive(true);
    }

    GameObject GetMonsterInPool()
    {
        foreach (var item in monsterPool)
        {
            if (item.activeSelf == false)
            {
                return item;
            }
        }
        return null;
    }

    IEnumerator StageRestart()
    {
        yield return new WaitForSeconds(0.2f);
        Transform spwanPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform item in spwanPointGroup)
        {
            listPoints.Add(item);
        }
        InvokeRepeating("CreateMonster", 2.0f, createTime);
        Transform startTr = GameObject.FindGameObjectWithTag("STAGE_START").GetComponent<Transform>();
        player.transform.position = startTr.position;
    }

    public void StageEnd()
    {
        
    }

    void Update()
    {
        
    }
}
