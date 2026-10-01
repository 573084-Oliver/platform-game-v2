using UnityEngine;
using UnityEngine.InputSystem;
public class NewMonoBehaviourScript : MonoBehaviour
{

    //declare the variables
    InputAction moveAction;
    InputAction jumpAction;
    InputAction destroyAction;
    Rigidbody2D rb;
    Animator anim;
    public LayerMask groundLayerMask;
    bool isGrounded;
    bool result;
    HelperScript helper;

    void Start()
    {
        //initialise the variables
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        destroyAction = InputSystem.actions.FindAction("Destroy");
        //required: a 2D Rigidbody component attached to the Game Object
        rb = GetComponent<Rigidbody2D>();    
        anim = GetComponent<Animator>();      //initialise the rigidbody component
        groundLayerMask = LayerMask.GetMask("Ground");
        print("kept you waiting, huh?");
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // read the x-axis and output it to the rigidbody
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * 3, rb.linearVelocity.y);
        Jump();
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

        if (rb.linearVelocityX < -0.1f)
        {
            helper.FlipSprite(true);

        }
        if (rb.linearVelocityX > 0.1f)
        {
            helper.FlipSprite(false);

        }
        if (destroyAction.WasPressedThisFrame())
        {
            helper.DestroyObject(true);
        }
        
    }

    void Jump()
    {
        if( jumpAction.WasPressedThisFrame() && (isGrounded == true) )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        if ((other.gameObject.tag == "Enemy") && (moveVel.y > -0.5))
        {
            helper.DestroyObject(true);
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