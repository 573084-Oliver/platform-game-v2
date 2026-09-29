using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    bool left;
    bool right;
    bool result;
    public LayerMask groundLayerMask;
    float dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();      
        sr = GetComponent<SpriteRenderer>();
        groundLayerMask = LayerMask.GetMask("Ground");
        dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        if (sr.flipX == false)
        {
            left = RayCollisionCheck(-0.4f, 0);
        }
        else
        {
            right = RayCollisionCheck(0.4f, 0);
        }
       
        if (sr.flipX == false)
        {
            rb.linearVelocityX = -dir;
        }
        else
        {
            rb.linearVelocityX = dir;
        }
       
        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

    }
    void FlipSprite()
    {
        if ( (dir<0) && (left == false) )
        {
            sr.flipX = true;
        }

       /* if ( (dir>0) && (right == false) )
        {
            sr.flipX = false;
        } */
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
            left = true;
            right = true;
        }
        else
        {
            left = false;
            right = false;
        }
        
        
        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }


}
