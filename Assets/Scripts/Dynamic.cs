using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
    public float JumpPower;
    public float Speed;
    public int Score;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            Debug.Log("오른쪽 이동");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            Debug.Log("왼쪽 이동");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * JumpPower);
            Debug.Log("점프");
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), $"Score:{Score}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"OnCollisionEnter2D:{collision.gameObject.name}");
        if (collision.collider.name == "cherry")
        {
            Score++;
            Destroy(collision.gameObject);
        }
    }
}
