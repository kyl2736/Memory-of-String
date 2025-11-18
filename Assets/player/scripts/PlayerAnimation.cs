using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimation : MonoBehaviour
{
    private float key_input;
    public bool heading = true;
    public PlayerMovement_Basic playermove;
    public PlayerMovementSkill skillmove;
    private Animator animator;
    private Transform trans;
    public PlayerAttack PlayerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    public IEnumerator Attack()
    {

        animator.SetBool("attacking", true);
        playermove.channeling = true;
        yield return new WaitUntil(() => !PlayerAttack.attacking);

        animator.SetBool("attacking", false);
        playermove.channeling = false;
        
    }

    public IEnumerator Spin()
    {

        animator.SetBool("spinning", true);
        playermove.channeling = true;
        yield return new WaitUntil(() => !skillmove.spinning);

        animator.SetBool("spinning", false);
        playermove.channeling = false;

    }

    // Update is called once per frame
    void Update()
    {
        key_input = Input.GetAxisRaw("Horizontal");
        //좌우 애니메이션
        if (key_input > 0)
        {
            if (!heading && playermove.OnGround() && !playermove.channeling)
            {
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
                heading = true;
            }
            animator.SetBool("running", true);
        }
        else if (key_input < 0)
        {
            if (heading && playermove.OnGround() && !playermove.channeling)
            {
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
                heading = false;
            }
            animator.SetBool("running", true);
        }
        else { animator.SetBool("running", false); }
        //끝

        //점프 애니메이션
        if (!playermove.OnGround()) { animator.SetBool("jumping", true); }
        else { animator.SetBool("jumping", false); }
        //RMx




    }
}
