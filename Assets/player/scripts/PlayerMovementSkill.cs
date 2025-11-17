using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementSkill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerMovement_Basic Basic;
    private Rigidbody2D body;
    private Transform trans;
    public LayerMask ground;

    public float blink_length;
    public float blink_cooldown;
    public float spin_pow;
    public float spin_cooldown;
    public float rope_speed;
    public float rope_length;
    public float max_bullet_time;
    public float rope_move_speed;

    private Coroutine spin;
    private Coroutine blink;
    public bool spinning = false;
    public GameObject blink_effect;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }
    IEnumerator Spin()
    {   
        spinning = true;
        if (!Basic.OnGround()) { body.linearVelocityY = spin_pow; }
        yield return new WaitForSeconds(0.3f);
        spinning = false;
        yield return new WaitForSeconds(spin_cooldown);
        spin = null;
    }

    IEnumerator Blink()
    {


        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position,new Vector2(1.6f,2f),0 , new Vector2(1,0), blink_length+0.8f, ground);
            trans.position += new Vector3((laser.collider != null) ? laser.distance : blink_length, 0, 0);

        }
        else if (Input.GetAxisRaw("Horizontal") < 0) 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position, new Vector2(1.6f, 2f), 0, new Vector2(-1,0), blink_length + 0.8f, ground);
            trans.position += new Vector3((laser.collider != null) ? -(laser.distance) : -blink_length, 0, 0);

        }
            
        else 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position, new Vector2(1.6f, 2f), 0, new Vector2(0, 1), blink_length + 0.8f, ground);
            trans.position += new Vector3(0, (laser.collider != null) ? (laser.distance) : blink_length, 0);
        }
        Instantiate(blink_effect, trans.position, Quaternion.identity);
        yield return new WaitForSeconds(blink_cooldown);
        blink = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && spin == null)
        { 
            spin = StartCoroutine(Spin());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && blink == null)
        {
            blink = StartCoroutine(Blink());
        }


    }
}
