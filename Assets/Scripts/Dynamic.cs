using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
    public float JumpPower;
    public float Speed;
    public int Score;
    public bool IsCrouching;
    public float ColliderRadius = 0.6f;
    public float CrouchingColliderRadius = 0.4f;
    public int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Dynamic script started.");
        IsCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (IsCrouching == true)
        {
            collider.radius = CrouchingColliderRadius;
            Speed = 5;
        }
        else
        {
            collider.radius = ColliderRadius;
            Speed = 10;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
            Debug.Log("오른쪽 이동");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
            Debug.Log("왼쪽 이동");
        }
        if (Input.GetKeyDown(KeyCode.A) && IsCrouching == false)
        {
            if(jumpCount < 2) //점프 카운트가 2개 미만이라면 // 2단 점프까지만 가능하도록
            { 
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * JumpPower);
                jumpCount++; //점프 카운트 증가
                Debug.Log("점프");
            }
        }
        if(Input.GetKey(KeyCode.S) && jumpCount == 0) // 점프 카운트가 0일 때만 엎드리기 사용 가능 // 점프 중 엎드리기 불가
        {
            IsCrouching = true;
            Debug.Log("IsCrouching = true");
        }
        else
        {
            IsCrouching = false;
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), $"Score:{Score}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //  {
    //      Debug.Log($"OnCollisionEnter2D:{collision.gameObject.name}");
    //      if (collision.gameObject.name == "cherry")
    //      {
    //          Score++;
    //          Destroy(collision.gameObject);
    //      }
    //  }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log($"OnTriggerEnter@d:{collision.gameObject.tag}");
    //    if (collision.gameObject.tag == "Item")
    //    {
    //        Score++;
    //        Destroy(collision.gameObject);
    //    }
    //}
}
