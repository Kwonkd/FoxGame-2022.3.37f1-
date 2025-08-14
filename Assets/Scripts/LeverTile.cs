using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeverTile : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveDistance = 5.0f;
    public float positionY;
    public float currentY;
    public float moveSpeed = 1.0f;
    public MoveType moveType = MoveType.Wait;
    // Start is called before the first frame update
    void Start()
    {
        positionY = tilemap.transform.position.y;
        currentY = tilemap.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveType)
        {
            case MoveType.Wait:
                currentY = tilemap.transform.position.y;
                break;

            case MoveType.Up:
                if (tilemap.transform.position.y <= positionY)
                {
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
                    DownMove();
                }
                else
                {
                    moveType = MoveType.Wait;
                }
                break;
        }
    }
    public void MoveTile()
    {
        if (moveType == MoveType.Wait)
        {
            if (currentY < positionY)
            {
                moveType = MoveType.Up;
            }
            else if (currentY >= positionY - moveDistance)
            {
                moveType = MoveType.Down;
            }
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
