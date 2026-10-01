using UnityEngine;

public class FollowEnemyScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Self;
    Rigidbody2D rb;
    
    float dir;
    HelperScript helper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        dir = 0.8f;
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x > Self.transform.position.x)
        {
            helper.FlipSprite(true);

        }
        else
        {
            helper.FlipSprite(false);

        }
        if (Player.transform.position.x > Self.transform.position.x)
        {
            rb.linearVelocityX = dir;
        }
        else
        {
            rb.linearVelocityX = -dir;
        }
    }
    
}
