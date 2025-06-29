using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject objTarget;
    public float Speed;
    public float CameraZ = -20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }
    void MoveUpdate()
    {
        if (objTarget != null)
        {
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = objTarget.transform.position;

            Vector3 vtargetCameraPos = new Vector3(vTargetPos.x, vTargetPos.y, CameraZ); //추적하는 오브젝트의 좌표인 vTargetPos의 x,y 좌표를 가져오는건 동일 하지만 z 좌표는 미리 설정해둔 값을 사용함

            Vector3 vDist = vtargetCameraPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;

            //   Debug.DrawLine(vPos, vTargetPos, Color.red);

            if (fDist > Time.deltaTime)
            {
                transform.position += vDir * Speed * Time.deltaTime;
            }
        }
    }
}
