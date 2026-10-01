using UnityEngine;

public class CarEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
   
    bool left;
    bool right;
    public LayerMask groundLayerMask;
    float dir;
    HelperScript helper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        dir = 1;
        rb = GetComponent<Rigidbody2D>();
        
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {

        left = RayCollisionCheck(-0.4f, 0);
        right = RayCollisionCheck(0.4f, 0);



        //check moving right
        if (dir > 0 && right == false)
        {

            dir = -5;
        }

        //check moving left
        if (dir < 0 && left == false)
        {

            dir = 5;
        }


        rb.linearVelocityX = dir;


        if (dir < 0)
        {
            helper.FlipSprite(false);

        }

        if (dir > 0)
        {
            helper.FlipSprite(true);

        }
    }
    
    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward starting at the sprite's position
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
        }

        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }
}
