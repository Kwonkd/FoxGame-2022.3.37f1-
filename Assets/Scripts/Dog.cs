using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DogState
{
    Idle,
    Running
}
public enum MoveDirection
{
    Left,
    Right
}
public class Dog : MonoBehaviour
{
    public int attackDamage = 1;
    public float speed = 3.0f;
    public float moveDistance = 5.0f; // 이동할 최대 거리
    public DogState state = DogState.Idle;
    Vector2 startPosition;
    Vector2 movePosition;
    Vector2 target;
    public MoveDirection moveDirection = MoveDirection.Right; // 이동 방향
    public Animator animator;
    public float idleTime = 2.0f; // 대기 시간
    public float runningTime = 5.0f; // 이동 시간
    public float timer = 0.0f;
    private float previousXPosition;

    private void Start()
    {
        startPosition = this.transform.position;
        target = startPosition;
        if (moveDirection == MoveDirection.Right)
        {
            movePosition = startPosition + new Vector2(moveDistance, 0);
        }
        else
        {
            movePosition = startPosition - new Vector2(moveDistance, 0);
        }
    }
    public void Update()
    {
        Direction();
        switch(state)
        {
            case DogState.Idle:
                {
                    if(animator != null)
                    {
                        animator.SetBool("Running", false);
                    }
                    if(idleTime > timer)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        state = DogState.Running;
                        timer = 0.0f;
                    }
                    break;
                }
            case DogState.Running:
                {
                    if (runningTime > timer)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                        animator.SetBool("Running", true);
                        timer += Time.deltaTime;
                        if (Vector3.Distance(transform.position, startPosition) < 0.01f)
                        {
                            target = movePosition;
                        }
                        else if (Vector3.Distance(transform.position, movePosition) < 0.01f)
                        {
                            target = startPosition;
                        }
                    }
                    else
                    {
                        state = DogState.Idle;
                        timer = 0.0f;
                    }
                        break;
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus ps = collision.gameObject.GetComponent<PlayerStatus>();
            ps.TakeDamage(attackDamage);
        }
    }

    void Direction()
    {
        float currentXPosition = transform.position.x;

        // X 좌표가 이전 프레임과 다르면 (변화가 감지되면)
        if (currentXPosition != previousXPosition)
        {
            // X 좌표가 증가했는지 감소했는지 확인
            if (currentXPosition > previousXPosition)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            // 다음 프레임을 위해 현재 X 좌표를 저장
            previousXPosition = currentXPosition;
        }
    }
}
