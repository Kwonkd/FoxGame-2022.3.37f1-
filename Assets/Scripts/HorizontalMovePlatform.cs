using UnityEngine;

// 플랫폼이 처음 움직이기 시작할 방향을 정의합니다.
public enum StartDirection
{
    Left,
    Right
}

public class HorizontalMovePlatform : MonoBehaviour
{
    [Tooltip("플랫폼이 처음 움직일 방향을 선택하세요.")]
    public StartDirection startDirection = StartDirection.Left;

    [Tooltip("플랫폼의 이동 속도입니다.")]
    public float moveSpeed = 2.0f;

    [Tooltip("시작 위치로부터 좌/우로 이동할 최대 거리입니다.")]
    public float moveDistance = 5.0f;

    private Vector3 startPosition;
    private Vector3 leftTarget;
    private Vector3 rightTarget;
    private bool movingRight;

    void Start()
    {
        // 처음 위치를 저장합니다.
        startPosition = transform.position;

        // 이동할 목표 지점(왼쪽 끝, 오른쪽 끝)을 계산합니다.
        leftTarget = startPosition - new Vector3(moveDistance, 0, 0);
        rightTarget = startPosition + new Vector3(moveDistance, 0, 0);

        // 설정된 시작 방향에 따라 초기 이동 방향을 결정합니다.
        // startDirection이 Right이면 true, Left이면 false가 됩니다.
        movingRight = (startDirection == StartDirection.Right);
    }

    void Update()
    {
        // 현재 이동 방향에 따라 목표 지점을 설정합니다.
        Vector3 currentTarget = movingRight ? rightTarget : leftTarget;

        // 플랫폼을 현재 위치에서 목표 지점으로 이동시킵니다.
        // Vector3.MoveTowards를 사용하면 프레임 속도에 관계없이 일정한 속도로 움직입니다.
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        // 플랫폼이 목표 지점에 거의 도달했는지 확인합니다.
        // (컴퓨터의 부동 소수점 오차를 감안하여 거리가 매우 가깝다면 도달한 것으로 간주)
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            // 목표에 도달했다면 이동 방향을 반대로 바꿉니다.
            movingRight = !movingRight;
        }
    }
}