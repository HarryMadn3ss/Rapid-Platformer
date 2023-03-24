using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreController : MonoBehaviour
{
    public Text scoreText;
    public Text timerText;


    private int playerScore;
    private float countDown;
    private float Timer;

    private void Start()
    {
        playerScore = 0;        
        countDown = 300;        
    }


    private void Update()
    {
        scoreText.text = playerScore.ToString();

        //Timer = Mathf.RoundToInt(Timer - Time.deltaTime);        
        countDown = countDown - Time.deltaTime;
        Timer = Mathf.Round(countDown);
        timerText.text = (Timer).ToString();

        if(Timer < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fruit"))
        {
            playerScore += 100;
        }
    }
}
