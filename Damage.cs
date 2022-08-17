using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string punchTag = "PUNCH";
    private float initHp = 100.0f;
    private Image hpBar;

    public float currHp;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag(punchTag) && currHp >= 0.0f)
        {
            currHp -= 5.0f;
        }
        Debug.Log($"Player Hp = {(currHp / initHp) * 100.0f} %");
        DisplayHealth();
        if (currHp <= 0.0f)
        {
            PlayerDie();
        }
    }

    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }

    void PlayerDie()
    {
        Debug.Log("Die");
        OnPlayerDie();
        GameManager.instance.IsGameOver = true;
        this.gameObject.SetActive(false);
    }

}
