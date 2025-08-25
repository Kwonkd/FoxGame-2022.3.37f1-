using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class Eagle : MonoBehaviour
{
    public GameObject objTarget;
    public float Speed = 2;
    public GameObject EaglePosition;

    public float curTime = -1;
    public float MaxTime = 2;

    // Update is called once per frame
    void Update()
    {
        MoveDirectrion();

        if (objTarget != null)
        {
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = objTarget.transform.position;
            Vector3 vDist = vTargetPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;
            //   Debug.DrawLine(vPos, vTargetPos, Color.red);

            if (fDist > Time.deltaTime)
            {
                transform.position += vDir * Speed * Time.deltaTime;
            }
        }

        if (objTarget == null)
        {
            if (curTime >= 0)
            {
                if (curTime < MaxTime)
                {
                    curTime += Time.deltaTime;
                }
                else
                {
                    objTarget = EaglePosition;
                    curTime = -1;
                }
            }
            else
            {
                if (objTarget == null)
                {
                    curTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objTarget = collision.gameObject;
            //if (curTime > -1)
            //{
            //    curTime -= 0.5f * Time.deltaTime;
            //}
        }
        //else
        //{
        //    objTarget = EaglePosition; //충돌한 오브젝트가 플레이어가 아닐때 원위치 // 플레이어가 아닌 다른 오브젝트에 닿아야만 제자리로 돌아감 // 원하는 동작은 아님.
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objTarget = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        { 
        Destroy(collision.gameObject);
        }
    }
    void MoveDirectrion() //독수리가 바라보는 방향을 변경
    {
        if (objTarget != null)
        {
            float currentX = transform.position.x; //독수리의 현재 x좌표
            float targetX = objTarget.transform.position.x; //독수리의 타겟의 x좌표
            float moveX = currentX - targetX;
            if(moveX > 0) // 타겟이 독수리보다 왼쪽에 위치
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (moveX < 0) // 타겟이 독수링보다 오른쪽에 위치
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
