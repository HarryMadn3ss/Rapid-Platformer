using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    public bool left = true;
    public float speed = 10;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(left)
        {
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(!left)
        {
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionExit2D(Collision2D boxCollider)
    {
        if(boxCollider != null)
        {
            if(left)
            {
                left = false;
            }
            else
            {
                left = true;
            }
        }
    }
}
