using UnityEngine;
public enum VerticalStartDirection
{
    Up,
    Down
}

public class VerticalMovePlatform : MonoBehaviour
{
    [Tooltip("플랫폼이 처음 움직일 방향을 선택하세요.")]
    public VerticalStartDirection startDirection = VerticalStartDirection.Up;

    [Tooltip("플랫폼의 이동 속도입니다.")]
    public float moveSpeed = 2.0f;

    [Tooltip("시작 위치로부터 위/아래로 이동할 최대 거리입니다.")]
    public float moveDistance = 5.0f;

    private Vector3 startPosition;
    private Vector3 upTarget;
    private Vector3 downTarget;
    private bool movingUp;

    void Start()
    {
        // 처음 위치를 저장합니다.
        startPosition = transform.position;

        // 이동할 목표 지점(위쪽 끝, 아래쪽 끝)을 계산합니다.
        upTarget = startPosition + new Vector3(0, moveDistance, 0);
        downTarget = startPosition - new Vector3(0, moveDistance, 0);

        // 설정된 시작 방향에 따라 초기 이동 방향을 결정합니다.
        movingUp = (startDirection == VerticalStartDirection.Up);
    }

    void Update()
    {
        // 현재 이동 방향에 따라 목표 지점을 설정합니다.
        Vector3 currentTarget = movingUp ? upTarget : downTarget;

        // 플랫폼을 현재 위치에서 목표 지점으로 이동시킵니다.
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        // 플랫폼이 목표 지점에 거의 도달했는지 확인합니다.
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            // 목표에 도달했다면 이동 방향을 반대로 바꿉니다.
            movingUp = !movingUp;
        }
    }
}