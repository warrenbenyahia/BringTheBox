using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    private Animator animator;
    private BoxCollider boxCollider;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlManager.IsTaped() && ControlManager.IsTouchingGameObject(gameObject) && animator)
        {
            animator.SetBool("IsTouched", true);
            Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
            boxCollider.enabled = false;
        }
    }
}
