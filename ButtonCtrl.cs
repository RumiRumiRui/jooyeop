using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    [Header("Button")]
    public Button tapStatButton;
    public Button tapSkillButton;
    public Button tapAbilityButton;

    [Header("Panel")]
    public GameObject statPanel;
    public GameObject skillPanel;
    public GameObject abilityPanel;

    [Header("TapFocus")]
    public GameObject tapFocus_Stat;
    public GameObject tapFocus_Skill;
    public GameObject tapFocus_Ability;

    private GameObject tapFocus;
    void Start()
    {
        statPanel.SetActive(true);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);
        tapFocus_Stat.SetActive(true);
        tapFocus_Skill.SetActive(false);
        tapFocus_Ability.SetActive(false);
        tapStatButton.onClick.AddListener(StatClick);
        tapSkillButton.onClick.AddListener(SkillClick);
        tapAbilityButton.onClick.AddListener(AbilityClick);
    }

    public void StatClick()
    {
        statPanel.SetActive(true);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);
        tapFocus_Stat.SetActive(true);
        tapFocus_Skill.SetActive(false);
        tapFocus_Ability.SetActive(false);
    }

    public void SkillClick()
    {
        statPanel.SetActive(false);
        skillPanel.SetActive(true);
        abilityPanel.SetActive(false);
        tapFocus_Stat.SetActive(false);
        tapFocus_Skill.SetActive(true);
        tapFocus_Ability.SetActive(false);
    }

    public void AbilityClick()
    {
        statPanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(true);
        tapFocus_Stat.SetActive(false);
        tapFocus_Skill.SetActive(false);
        tapFocus_Ability.SetActive(true);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) == GameObject.Find("Tap_Stat"))
    //    {
    //        this.gameObject.GetComponentInChildren<Image>().enabled = true;
    //        statPanel.SetActive(true);
    //        Debug.Log("Stat");
    //        skillPanel.SetActive(false);
    //        abilityPanel.SetActive(false);
    //    }
    //    else if (Input.GetMouseButtonDown(0) == GameObject.Find("Tap_Skill"))
    //    {
    //        skillPanel.SetActive(true);
    //        Debug.Log("Skill");
    //        statPanel.SetActive(false);
    //        abilityPanel.SetActive(false);
    //    }
    //    else if (Input.GetMouseButtonDown(0) == GameObject.Find("Tap_Ability"))
    //    {
    //        abilityPanel.SetActive(true);
    //        Debug.Log("Ability");
    //        statPanel.SetActive(false);
    //        skillPanel.SetActive(false);
    //    }
    //}
}
