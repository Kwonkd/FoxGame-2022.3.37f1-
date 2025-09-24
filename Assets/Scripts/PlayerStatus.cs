using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 5;
    public int currentHp;
    public int numberOfCherry;



    private void Awake()
    {
        currentHp = maxHp;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if(currentHp > 0)
        {
            currentHp -= damage;
        }
        if(currentHp <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        PauseUI pauseUI = FindObjectOfType<PauseUI>();
        pauseUI.GameOverUI();
    }
}
