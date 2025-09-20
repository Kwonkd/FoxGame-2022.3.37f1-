using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CherryUI : MonoBehaviour
{
    public PlayerStatus ps;
    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI playerHp;

    // Update is called once per frame
    void Update()
    {
        if (playerHp != null)
        {
            playerHp.text = ps.currentHp.ToString();
            cherryText.text = "x " + ps.numberOfCherry.ToString();
        }
    }
}
