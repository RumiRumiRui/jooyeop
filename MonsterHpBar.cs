using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    private float initHp = 100.0f;
    private Image monsterHpBarImage;
    private Image hpBarImage;
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTr;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = GetComponent<RectTransform>();

        monsterHpBarImage = GetComponent<Image>();
        hpBarImage = GetComponentsInChildren<Image>()[1];
        HpBarClear();
        
    }

    public IEnumerator DisplayHpBar(float hp)
    {
        hpBarImage.fillAmount = hp / initHp;
        yield return new WaitForSeconds(3.0f);
        HpBarClear();
    }

    public void HpBarClear()
    {
        monsterHpBarImage.color = Color.clear;
        hpBarImage.color = Color.clear;
    }

    void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        if (screenPos.z < 0.0f) screenPos *= -1.0f;
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos,
            uiCamera, out localPos);
        rectHp.localPosition = localPos;

    }
}
