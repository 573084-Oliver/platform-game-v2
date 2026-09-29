using UnityEngine;
using UnityEngine.InputSystem;
public class NewMonoBehaviourScript : MonoBehaviour
{

    //declare the variables
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public LayerMask groundLayerMask;
    bool isGrounded;
    bool result;
       

    void Start()
    {
        //initialise the variables
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        //required: a 2D Rigidbody component attached to the Game Object
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();      //initialise the rigidbody component
        sr = GetComponent<SpriteRenderer>();
        groundLayerMask = LayerMask.GetMask("Ground");
        
    }

    // Update is called once per frame
    void Update()
    {
        // read the x-axis and output it to the rigidbody
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * 3, moveVel.y *3);
        Jump();
        FlipSprite();
        isGrounded = RayCollisionCheck(0, 0.4f);

        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (moveVel.y <= -0.5f)
        {
            anim.SetBool("crouch", true);
        }

        else
        {
            anim.SetBool("crouch", false);
        }

       
    }

    void Jump()
    {
        if( jumpAction.WasPressedThisFrame() && (isGrounded == true) )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 80);
        }
    }

    void FlipSprite()
    {
        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = true;
        }
        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = false;
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
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }
     

}