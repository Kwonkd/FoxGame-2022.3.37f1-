using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dynamic : MonoBehaviour
{
    public Animator animator; // 애니메이션 컨트롤러
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    [Header("Common")]
    public float Speed; // 이동 속도
    public float ColliderRadius = 0.6f; // 기본 콜라이더 반지름
    public float NormalSpeed = 10f; // 기본 이동 속도

    [Header("Jump")]
    public float JumpPower; // 점프 시 가해지는 힘
    public bool IsGrounded; // 바닥에 닿아있는지 여부
    public float Groundlinesize = 1.0f; // 바닥 감지용 Raycast 길이
    public int jumpCount = 0; // 점프 카운트 (최대 2단 점프)

    [Header("Crouch")]
    public bool IsCrouching; // 엎드리고 있는지 여부
    public float LineSize = 0.3f; // 머리 위 장애물 감지용 Raycast 길이
    public float CrouchingColliderRadius = 0.4f; // 엎드렸을 때 콜라이더 반지름
    public float CrouchSpeed = 5f; // 엎드렸을 때 이동 속도

    [Header("Gravity")]
    public float FallingGravityMultiplier = 2.0f; // 낙하 중력 배율
    private float originalGravityScale = 1.0f; // 기본 중력

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Dynamic script started.");

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();

        IsCrouching = false;
        IsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();       // 웅크리기 상태에 따라 속도와 콜라이더 크기 조절
        Jump();             // 점프 입력 및 실행
        Crouching();        // 웅크리기 입력 및 실행
        RaycastGround();    // 바닥 감지

    }
    private void FixedUpdate()
    {
        // 물리적인 움직임 처리
        movePlayer();       // 플레이어 좌우 이동 처리
        AdjustJumpGravity();// 점프/낙하 시 중력 조절
    }
    void movePlayer()
    {
        // GetKey 대신 GetAxisRaw를 사용하면 더 부드럽고 유연한 입력 처리 (좌: -1, 우: 1, 없음: 0)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // x축 속도는 입력을 받아 제어하고, y축 속도는 현재 값을 유지 (점프/낙하에 영향 X)
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if (moveInput != 0)
        {
            // 스프라이트 방향 전환
            spriteRenderer.flipX = moveInput < 0;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsCrouching == false) // 스페이스바 입력, 엎드린 상태가 아닐 때
        {
            if (jumpCount < 1) //점프 카운트가 2개 미만이라면 // 2단 점프까지만 가능하도록 제한.
            {
                jumpCount++; //점프 카운트 증가
                rb.AddForce(Vector2.up * JumpPower);
            }
        }
    }
    void AdjustJumpGravity()
    {
        // 캐릭터가 상승 중일 때 (y축 속도가 양수)
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.A))
        {
            // 중력을 원래대로 유지하거나 약간 낮게 유지 (옵션)
            rb.gravityScale = originalGravityScale;
            animator.SetBool("Jump", true);
        }
        // 캐릭터가 하강 중일 때 (y축 속도가 음수)
        else if (rb.velocity.y < 0)
        {
            // 떨어질 때 중력을 더 강하게 적용
            rb.gravityScale = originalGravityScale * FallingGravityMultiplier;
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
        // 땅에 있거나 점프 시작 직후 (y축 속도가 0에 가깝거나 점프 키를 누른 상태)
        else
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Jump", false);
            rb.gravityScale = originalGravityScale; // 기본 중력으로 돌아감
        }
    }
    void Crouching()
    {
        if (Input.GetKey(KeyCode.S) && jumpCount == 0) // 점프 카운트가 0일 때만 엎드리기 사용 가능 // 점프 중 엎드리기 불가
        {
            IsCrouching = true;
            animator.SetBool("Crouch", true);
            //Debug.Log("IsCrouching = true");
        }
        // S키를 떼고, 머리 위에 장애물이 없을 때만 일어서기
        else if (!Input.GetKey(KeyCode.S) && !RaycastCrouching())
        {
            IsCrouching = false;
            animator.SetBool("Crouch", false);
        }
    }
    void CheckState()
    {
        if (IsCrouching == true)
        {
            circleCollider.radius = CrouchingColliderRadius;
            Speed = CrouchSpeed;
            RaycastCrouching();
        }
        else
        {
            circleCollider.radius = ColliderRadius;
            Speed = NormalSpeed;
        }
    }

    bool RaycastCrouching()
    {
        // 캐릭터 위치에서 위 방향으로 Raycast 발사
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, LineSize, ~LayerMask.GetMask("Ignore"));
        //Debug.DrawRay(transform.position, Vector3.up * LineSize, Color.red);
        //if (hit.collider != null)
        //{
        //    Debug.Log(hit.collider.name);
        //}
        return hit.collider != null; // 장애물에 닿았는지 여부를 true/false로 반환
    }
    void RaycastGround()
    {
        // 캐릭터 발밑 위치에서 아래 방향으로 Raycast 발사
        //Vector3 rayStartPos = new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Groundlinesize, ~LayerMask.GetMask("Ignore"));

        if (hit) // Raycast에 무언가 닿았다면
        {
            IsGrounded = true;
            jumpCount = 0; // 점프 횟수 초기화
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
        else // 닿지 않았다면 (공중에 있다면)
        {
            IsGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 게임 오브젝트의 태그가 "MovingPlatform"이라면
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // 플레이어를 충돌한 플랫폼의 자식으로 만듭니다.
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝난 게임 오브젝트의 태그가 "MovingPlatform"이라면
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // 플레이어의 부모-자식 관계를 해제합니다.
            transform.SetParent(null);
        }
    }
}
