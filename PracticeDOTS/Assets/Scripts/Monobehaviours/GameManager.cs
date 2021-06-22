using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }
    public bool gameOver;
    public int lives;
    [SerializeField] Text livesText;
    [SerializeField] GameObject gameOverPanel;
   // public bool playerInvincible;
    public float invincibleTime;
    void Awake()
    {
        if(gm==null)
        {
            gm = this.GetComponent<GameManager>();
        }
    }
    
    public void ChangeText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
}
