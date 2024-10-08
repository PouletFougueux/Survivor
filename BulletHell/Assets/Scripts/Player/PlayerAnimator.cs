using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private playerMovement movement;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<playerMovement>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (movement.moveDirection.x != 0 || movement.moveDirection.y != 0)
        {
            animator.SetBool("Move", true);
            DirectionCheck();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    void DirectionCheck()
    {
        if (movement.lastXValue < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
