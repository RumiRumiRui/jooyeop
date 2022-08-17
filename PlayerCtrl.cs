using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animation anim;

    public float moveSpeed = 10.0f;

    private IEnumerator Start()
    {
        tr = gameObject.GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("idle");

        yield return new WaitForSeconds(0.3f);
    }

    void Update()
    {
        
    }
}
