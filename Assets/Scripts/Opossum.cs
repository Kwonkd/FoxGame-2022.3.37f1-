using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public float Speed;
    public bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Opossum script started.");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
