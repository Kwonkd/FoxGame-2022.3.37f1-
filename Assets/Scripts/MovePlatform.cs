using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlatformMoveType
{
    Wait,
    Move
}
public class MovePlatform : MonoBehaviour
{
    public PlatformMoveType moveType = PlatformMoveType.Wait;
    public float waitTime = 1.0f;
    public float moveDistance = 5.0f;
    public float elapsedTime = 0.0f;
    public float positionY;
    public float moveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        positionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveType)
        { 
            case PlatformMoveType.Wait:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= waitTime)
                {
                    moveType = PlatformMoveType.Move;
                    elapsedTime = 0.0f;
                }
                break;
            case PlatformMoveType.Move:
                if (transform.position.y <= positionY + moveDistance)
                {
                    PlatformMove();
                }
                else
                {
                    moveType = PlatformMoveType.Wait;
                }
                break;
        }
    }
    void PlatformMove()
    {
        if(transform.position.y <= positionY + moveDistance)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }   
    }
}
