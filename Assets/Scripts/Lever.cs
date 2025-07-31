using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public TextMeshProUGUI leverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        { 
            leverText.text = "Press Q Button";
            leverText.gameObject.SetActive(true);
            leverText.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leverText.gameObject.SetActive(false);
        }
    }
}
