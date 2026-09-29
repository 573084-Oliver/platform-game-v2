using UnityEngine;

public class FollowEnemyScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Self;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dir = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        if (Player.transform.position.x > Self.transform.position.x)
        {
            rb.linearVelocityX = dir;
        }
        else
        {
            rb.linearVelocityX = -dir;
        }
    }
    void FlipSprite()
    {
        if (Player.transform.position.x > Self.transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
