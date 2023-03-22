using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public CapsuleCollider2D capsuleCollider;
    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
       
    bool facingRight = true;
    int direction = 1;
    public float speed = 10;

    float maxVelocity = 3;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        rb.AddForce(new Vector2(speed * direction, 0), ForceMode2D.Force);            
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        if(rb.velocity.x == 0)
        {
            rb.AddForce(new Vector2(speed * direction, 5));
            Debug.Log("Panik!");
        }
            
        
        //if(facingRight)
        //{
        //    rb.AddForce(new Vector2(speed, 0), ForceMode2D.Force);            
        //    rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
    }

    private void OnCollisionExit2D(Collision2D boxCollider)
    {
        transform.Rotate(0, 180, 0);
        if (facingRight)
        {
            facingRight = false;            
        }
        else
        {
            facingRight = true;            
        }
    }

    private void OnCollisionEnter2D(Collision2D capsuleCollider)
    {
        playerRB = capsuleCollider.gameObject.GetComponent<Rigidbody2D>();

        if (capsuleCollider.gameObject.CompareTag("Player") && playerRB.velocity.y < -1)
        {
            Destroy(this.gameObject);
        }
      
    }

    
}
