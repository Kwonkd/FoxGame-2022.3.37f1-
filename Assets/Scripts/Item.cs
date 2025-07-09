using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Item script started.");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Dynamic>().Number += number;
            Destroy(gameObject);
        }
    }
}
