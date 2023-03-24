using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthSystem : MonoBehaviour
{

    public int playerHealth = 3;

    public GameObject HP;
    public GameObject HP1;
    public GameObject HP2;

    private Rigidbody2D rb;

    



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerHealth == 2)
        {
            HP2.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            HP1.SetActive(false);
        }
        else if (playerHealth == 0)
        {
            HP.SetActive(false);
        }
        else if (playerHealth < 0)
        {
            //reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && rb.velocity.y > -0.1)
        {
            playerHealth--;
           
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("KillVolume"))
        {
            playerHealth--;
        }
    }
}
