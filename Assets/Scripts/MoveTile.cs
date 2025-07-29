using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MoveType
{
    Up,
    Down,
    Wait
}
public class MoveTile : MonoBehaviour
{
    public Tilemap tilemap;
    public MoveType moveType = MoveType.Wait;
    public float waitTime = 1.0f;
    public float moveDistance = 5.0f;
    public float elapsedTime = 0.0f;
    public float positionY;

    void Start()
    {
        positionY = tilemap.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch(moveType)
        {
            case MoveType.Wait:
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= waitTime)
                {
                    if(positionY > tilemap.transform.position.y)
                    {
                        moveType = MoveType.Up;
                    }
                    else
                    {
                        moveType = MoveType.Down;
                    }
                }
                break;

            case MoveType.Up:
                if(tilemap.transform.position.y <= positionY)
                {
                    elapsedTime = 0.0f;
                    UpMove();
                }
                else
                {
                    moveType = MoveType.Wait;
                }
                break;

            case MoveType.Down:
                if (tilemap.transform.position.y >= positionY - moveDistance)
                {
                    elapsedTime = 0.0f;
                    DownMove();
                }
                else
                {
                    moveType = MoveType.Wait;
                }
                break;
        }
    }

    void UpMove()
    {
            tilemap.transform.position += Vector3.up * Time.deltaTime;
    }
    void DownMove()
    {
            tilemap.transform.position += Vector3.down * Time.deltaTime;
    }
}
