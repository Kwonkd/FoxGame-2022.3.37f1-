using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CherryUI : MonoBehaviour
{
    public PlayerStatus ps;
    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI playerHp;
    public GameObject[] heart;

    // Update is called once per frame
    void Update()
    {
        if (playerHp != null)
        {
            int currentHp = ps.currentHp;
            for(int i = 0; i < heart.Length; i++)
            {
                if (i < currentHp)
                {
                    heart[i].SetActive(true);
                }
                else
                {
                    heart[i].SetActive(false);
                }
            }
            //playerHp.text = ps.currentHp.ToString();
            cherryText.text = "x " + ps.numberOfCherry.ToString();
        }
    }
}
