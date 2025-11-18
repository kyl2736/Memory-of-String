using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();   
    }

    void Start()
    {
        body.linearVelocity = transform.right * 20;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8)
        {    
            body.linearVelocity = Vector2.zero;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
