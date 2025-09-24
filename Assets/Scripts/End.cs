using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject EndUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && EndUI != null)
        {
            gameObject.SetActive(false);
            EndUI.SetActive(true); // 클리어 UI 활성화
            Time.timeScale = 0f; // 게임 일시정지
        }
    }
}
