using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerMovement_Basic playermove;
    public PlayerHealth playerhealth;
    public PlayerAnimation anim;
    private Coroutine attack;
    public bool attacking = false;



    IEnumerator Attack()
    {
        attacking = true;
        StartCoroutine(anim.Attack());
        playerhealth.Hair_steal( 16 );
        yield return new WaitForSeconds(0.25f);

        attacking = false;
        yield return new WaitForSeconds(0.2f);
        attack = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && attack == null && !playermove.channeling) 
        {
            attack = StartCoroutine(Attack());

        }


         //print(attacking);
    }
}
