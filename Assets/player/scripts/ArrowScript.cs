using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D body;
    public GameObject player;
    private PlayerMovementSkill skillmove;
    public PlayerConfig config;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();   
    }

    void Start()
    {
        body.linearVelocity = transform.right * config.ankor_speed;
        skillmove = player.GetComponent<PlayerMovementSkill>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8)
        {   
            
            body.linearVelocity = Vector2.zero;
            skillmove.Ankor_move();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
