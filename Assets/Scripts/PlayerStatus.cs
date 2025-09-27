using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 5;
    public int currentHp;
    public int numberOfCherry;
    public Animator animator;

    [Header("SuperMode")]
    public bool isUse;
    public float time;
    public SpriteRenderer spriteRenderer;


    private void Awake()
    {
        currentHp = maxHp;
        isUse = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isUse == true)
        {
            Color color = spriteRenderer.color;
            if (color.a == 1) color.a = 0;
            else color.a = 1;
            spriteRenderer.color = color;
        }
    }

    public void TakeDamage(int damage)
    {
        if(currentHp > 0 && isUse == false)
        {
            currentHp -= damage;
            SuperMode();
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
    public void SuperMode()
    {
        StartCoroutine(ProcessTimmer());
    }

    IEnumerator ProcessTimmer()
    {
        isUse = true;
        yield return new WaitForSeconds(time);
        isUse = false;
        spriteRenderer.color = Color.white;
    }
}
