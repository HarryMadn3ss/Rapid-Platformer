using UnityEngine;
using System.Collections;


//--------------------------------------------
/*Better Character Controller Includes:
     - Fixed Update / Update Input seperation
     - Better grounding using a overlap box
     - Basic Multi Jump
 */
//--------------------------------------------
public class BetterCharacterController : MonoBehaviour
{
    protected bool facingRight = true;
    protected bool jumped;
    protected bool dashed;
    public int maxJumps;
    protected int currentjumpCount;
    

    public float speed = 5.0f;
    public float jumpForce = 1000;
    public float dashForce = 200;
    public float pushForce = 250;

    private float horizInput;

    public bool grounded;

    public Rigidbody2D rb;

    public LayerMask groundedLayers;

    protected Collider2D charCollision;
    protected Vector2 playerSize, boxSize;

    Animator animator;
    public bool isJumping;

    public GameObject ghost;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        charCollision = GetComponent<Collider2D>();
        playerSize = charCollision.bounds.extents;
        boxSize = new Vector2(playerSize.x, 0.05f);
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Box Overlap Ground Check
        Vector2 boxCenter = new Vector2(transform.position.x + charCollision.offset.x, transform.position.y + -(playerSize.y + boxSize.y - 0.01f) + charCollision.offset.y);
        grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundedLayers) != null;

        //Move Character
        rb.velocity = new Vector2(horizInput * speed * Time.fixedDeltaTime, rb.velocity.y);


        if(rb.velocity.y > 0.5f)
        {
            animator.SetBool("isJumping", true);      
        }
        else if(rb.velocity.y < -0.5f)
        {
            animator.SetBool("isFalling", true);            
        }       
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }


        //Jump
        if (jumped == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            Debug.Log("Jumping!");

            jumped = false;
            animator.SetBool("isJumping", true);
        }

        // Detect if character sprite needs flipping.
        if (horizInput > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (horizInput < 0 && facingRight)
        {
            FlipSprite();
        }

        //dash
        if(dashed == true)
        {
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            if(facingRight)
            {
                rb.AddForce(new Vector2(dashForce, 0f), ForceMode2D.Impulse);
                dashed = false;
            }
            if(!facingRight)
            {
                rb.AddForce(new Vector2(-dashForce, 0f), ForceMode2D.Impulse);
                dashed = false;
            }
            Destroy(currentGhost, 1f);
        }
        
    }

    void Update()
    {
        
       
            if (grounded)
            {
                currentjumpCount = maxJumps;
                //animator.SetBool("isJumping", false);
                isJumping = false;

            if (Input.GetButtonDown("Jump"))
            {
                //animator.SetBool("isJumping", true);
                jumped = true;
            }
        }

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                dashed = true;
            }
        

            //Input for jumping ***Multi Jumping***
            //if (Input.GetButtonDown("Jump") && currentjumpCount == 1)
            //{
            //    jumped = true;
            //    currentjumpCount--;
            //    Debug.Log("Should jump");
            //    isJumping = true;
            //    //animator.SetBool("isJumping", true);
            //}

            //if(Input.GetButtonDown("Jump"))
            //{
            //    //animator.SetBool("isJumping", true);
            //    isJumping = true;
            //}

            //Get Player input 
            horizInput = Input.GetAxis("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(horizInput * speed * Time.fixedDeltaTime));

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", true);
            }
            else if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", false);
            }       
       
       
    }

    // Flip Character Sprite
    void FlipSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Enemy"))
        {

            Vector2 playerPos = transform.position;

            enemyController enemyController = collision.gameObject.GetComponent<enemyController>();
            Vector2 enemyPos = enemyController.transform.position;

            Vector2 towardPlayer = playerPos - enemyPos;
            //towardPlayer.Normalize();

            rb.AddForce(towardPlayer * pushForce, ForceMode2D.Force);
        }
    }

}

