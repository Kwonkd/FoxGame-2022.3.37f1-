using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dynamic : MonoBehaviour
{
    public float JumpPower;
    public float Speed;
    public int Score;
    public bool IsCrouching;
    public float ColliderRadius = 0.6f;
    public float CrouchingColliderRadius = 0.4f;
    public int jumpCount = 0;
    public float LineSize;

    private Rigidbody2D rb;

    public float FallingGravityMultiplier = 2.0f;
    private float originalGravityScale = 1.0f;

    bool RaycastCrouching()
    {
        Vector3 rayTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z); // 레이캐스트 위치를 플레이어의 위치로 설정 //위치 수정을 위해 따로 정의함
        Debug.DrawRay(rayTransform, transform.forward + Vector3.up * LineSize, Color.blue);
        LayerMask player = LayerMask.GetMask("Player");

        RaycastHit2D Raycast = Physics2D.Raycast(rayTransform, transform.up); //레이캐스트를 플레이어 머리 위로 //레이캐스트가 플레이어를 무시하도록
        if (Raycast && IsCrouching)
        {
            Debug.Log($"Raycast Hit: {Raycast.collider.gameObject.name}");
            return true;
        }
        else
        {
            return false;
        }
    }
    void movePlayer()
    {
        float MoveInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveInput = 1f;
            Debug.Log("오른쪽 이동");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveInput = -1f;
            Debug.Log("왼쪽 이동");
        }

        transform.position += Vector3.right * MoveInput * Speed * Time.deltaTime;
    }
    void Crouching()
    {
        if (Input.GetKey(KeyCode.S) && jumpCount == 0 || RaycastCrouching()) // 점프 카운트가 0일 때만 엎드리기 사용 가능 // 점프 중 엎드리기 불가
        {
            IsCrouching = true;
            Debug.Log("IsCrouching = true");
        }
        else
        {
            IsCrouching = false;
        }
    }
    void CheckState()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (IsCrouching == true)
        {
            collider.radius = CrouchingColliderRadius;
            Speed = 5;
            RaycastCrouching();
        }
        else
        {
            collider.radius = ColliderRadius;
            Speed = 10;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.A) && IsCrouching == false)
        {
            if (jumpCount < 2) //점프 카운트가 2개 미만이라면 // 2단 점프까지만 가능하도록
            {
                rb.AddForce(Vector2.up * JumpPower);
                float horizonInput = 0f;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    horizonInput = 1f; //오른쪽 이동
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    horizonInput = -1f; //왼쪽 이동
                }

                if(horizonInput != 0f)
                {
                    rb.AddForce(Vector2.right * horizonInput * JumpPower * 0.01f,ForceMode2D.Impulse); //수평 이동
                }
                jumpCount++; //점프 카운트 증가
                Debug.Log("점프");
            }
        }
    }
    void AdjustJumpGravity()
    {
        // 캐릭터가 상승 중일 때 (y축 속도가 양수)
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.A)) // 점프 키를 누르지 않고 상승 중
        {
            // 중력을 원래대로 유지하거나 약간 낮게 유지 (옵션)
            rb.gravityScale = originalGravityScale;
        }
        // 캐릭터가 하강 중일 때 (y축 속도가 음수)
        else if (rb.velocity.y < 0)
        {
            // 떨어질 때 중력을 더 강하게 적용
            rb.gravityScale = originalGravityScale * FallingGravityMultiplier;
        }
        // 땅에 있거나 점프 시작 직후 (y축 속도가 0에 가깝거나 점프 키를 누른 상태)
        else
        {
            rb.gravityScale = originalGravityScale; // 기본 중력으로 돌아감
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Dynamic script started.");
        IsCrouching = false;
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        movePlayer();
        Jump();
        Crouching();
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
