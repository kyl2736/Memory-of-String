using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementSkill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerMovement_Basic Basic;
    public PlayerAnimation anim;
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
    public GameObject vanish_effect;
    public GameObject blink_effect;
    public GameObject arrow;
    public Coroutine rope;
    private bool ankorcontact;
    private bool xpressed = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }
    IEnumerator Spin()
    {   
        spinning = true;
        StartCoroutine(anim.Spin());
        if (!Basic.OnGround()) { body.linearVelocityY = spin_pow; }
        yield return new WaitForSeconds(0.3f);
        spinning = false;
        yield return new WaitForSeconds(spin_cooldown);
        spin = null;
    }

    IEnumerator Blink()
    {

        Instantiate(vanish_effect, trans.position, Quaternion.Euler(90f,0,0));
        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position,new Vector2(1.8f,2.5f),0 , new Vector2(1,0), blink_length+0.8f, ground);
            trans.position += new Vector3((laser.collider != null) ? laser.distance : blink_length, 0, 0);
            body.linearVelocityY /= 4;

        }
        else if (Input.GetAxisRaw("Horizontal") < 0) 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position, new Vector2(1.8f, 2.5f), 0, new Vector2(-1,0), blink_length + 0.8f, ground);
            trans.position += new Vector3((laser.collider != null) ? -(laser.distance) : -blink_length, 0, 0);
            body.linearVelocityY /= 4;

        }
            
        else 
        {
            RaycastHit2D laser = Physics2D.BoxCast(trans.position, new Vector2(1.8f, 2.5f), 0, new Vector2(0, 1), blink_length + 0.8f, ground);
            trans.position += new Vector3(0, (laser.collider != null) ? (laser.distance) : blink_length, 0);
            body.linearVelocityY /= 4;
        }
        Instantiate(blink_effect, trans.position, Quaternion.Euler(90f, 0, 0));
        yield return new WaitForSeconds(blink_cooldown);
        blink = null;
    }

    public void Ankor_move()
    {
        ankorcontact = true;
    }

    IEnumerator Rope()
    {
        ankorcontact = false;
        GameObject ankor;
        Time.timeScale = 0.3f;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.X) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));

        if (Input.GetKey(KeyCode.RightArrow)) 
        { 
            ankor = Instantiate(arrow, trans.position, Quaternion.Euler(0, 0, 0));
            ankor.GetComponent<ArrowScript>().player = this.gameObject;
            Time.timeScale = 1f;
        }

        else if (Input.GetKey(KeyCode.UpArrow)) 
        {
            ankor = Instantiate(arrow, trans.position, Quaternion.Euler(0, 0, 90));
            ankor.GetComponent<ArrowScript>().player = this.gameObject;
            Time.timeScale = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            ankor = Instantiate(arrow, trans.position, Quaternion.Euler(0, 180, 0));
            ankor.GetComponent<ArrowScript>().player = this.gameObject;
            Time.timeScale = 1f;
        }
        else 
        { 
            Time.timeScale = 1f;
            rope = null;
            yield break;
        }

        yield return new WaitUntil(() => ankorcontact || xpressed);
        if (!ankorcontact && Input.GetKey(KeyCode.X))
        {
            Destroy(ankor);
            xpressed = false;
            rope = null;
            yield break;

        }
        else
        {
            body.gravityScale = 0;
            while (!xpressed)
            {
                if (((Vector2)ankor.transform.position - (Vector2)body.transform.position).magnitude > 0.6f)
                {
                    body.linearVelocity = ((Vector2)ankor.transform.position - (Vector2)body.transform.position).normalized * rope_move_speed;
                }
                else { body.linearVelocity = Vector2.zero; }

                yield return new WaitForFixedUpdate();
            }
        }
        xpressed = false;
        body.gravityScale = 2.5f;

        Destroy(ankor);

        yield return new WaitForSeconds(0.2f);
            rope = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X)) { xpressed = true; }

        if (Input.GetKeyDown(KeyCode.C) && spin == null && !Basic.channeling)
        {
            spin = StartCoroutine(Spin());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && blink == null)
        {
            blink = StartCoroutine(Blink());
        }


        if (xpressed && rope == null)   
        {
            xpressed = false;   
            rope = StartCoroutine(Rope()); 
        }

    }
}
