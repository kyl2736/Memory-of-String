using UnityEngine;

public class ZansangScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SpriteRenderer sprite;
    private Transform trans;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
    }

    // ★ 외부(플레이어)에서 이미지를 받아오는 함수
    public void SetSprite(Sprite dad, Vector3 scale)
    {
        sprite.sprite = dad;       
        trans.localScale = scale;

        sprite.sortingLayerName = "zansang";
        Color tempc = sprite.color;
        tempc.a = 0.5f;
        sprite.color = tempc;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tempcol = sprite.color;
        tempcol.a -= 2f * Time.deltaTime;
        sprite.color = tempcol;
        if (sprite.color.a <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
