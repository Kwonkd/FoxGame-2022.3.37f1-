using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class Eagle : MonoBehaviour
{
    public GameObject objTarget;
    public float Speed = 2;
    public GameObject EaglePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objTarget = collision.gameObject;
        }
        else
        {
            objTarget = EaglePosition; //충돌한 오브젝트가 플레이어가 아닐때 원위치 // 플레이어가 아닌 다른 오브젝트에 닿아야만 제자리로 돌아감 // 원하는 동작은 아님.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        { 
        Destroy(collision.gameObject);
        }
    }
}
