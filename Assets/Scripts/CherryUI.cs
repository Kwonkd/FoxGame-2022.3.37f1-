using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CherryUI : MonoBehaviour
{
    public PlayerStatus ps;
    public TextMeshProUGUI cherryText;

    // Update is called once per frame
    void Update()
    {
        cherryText.text = "x " + ps.numberOfCherry.ToString();
    }
}
