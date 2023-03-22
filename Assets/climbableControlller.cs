using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbableControlller : MonoBehaviour
{

    Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            playerRB.gravityScale = 1;

        }
    }
}
