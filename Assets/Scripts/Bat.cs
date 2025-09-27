using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatState
{
    Idle,
    Chase,
    Return
}
public class Bat : MonoBehaviour
{
    public BatState state = BatState.Idle;
    public Transform target;
    public float speed = 1.0f;
    public float chaseRange = 5.0f;
    public float attackRange = 1.0f;
    public int attackDamage = 1;
    public Transform returnPoint;
    public Animator animator;
    public float returnDelay = 2.0f;
    public float returnTimer = 0.0f;

    void Update()
    {
        MoveDirectrion();
        switch (state)
        {
            case BatState.Idle:
                {
                    if(animator != null)
                    {
                        animator.SetBool("Chase", false);
                    }
                    target = null;
                    break;
                }
            case BatState.Chase:
                {
                    Chase();
                    if (animator != null)
                    {
                        animator.SetBool("Chase", true);
                    }
                    break;
                }
            case BatState.Return:
                {
                    if (returnDelay > returnTimer)
                    {
                        returnTimer += Time.deltaTime;
                    }
                    else
                    {
                        if(returnPoint != null)
                        {
                            target = returnPoint;
                            Vector3 vPos = this.transform.position;
                            Vector3 vTargetPos = target.transform.position;
                            Vector3 vDist = vTargetPos - vPos;
                            Vector3 vDir = vDist.normalized;
                            float fDist = vDist.magnitude;
                            if (fDist <= 0.1f)
                            {
                                state = BatState.Idle;
                                returnTimer = 0.0f;
                            }
                            else
                            {
                                Chase();
                            }
                        }
                    }
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
            if(playerStatus != null)
            {
                playerStatus.TakeDamage(attackDamage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            state = BatState.Chase;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
            state = BatState.Return;
        }
    }
    void Chase()
    {
        if (target != null)
        {
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = target.transform.position;
            Vector3 vDist = vTargetPos - vPos;
            Vector3 vDir = vDist.normalized;
            float fDist = vDist.magnitude;
            //   Debug.DrawLine(vPos, vTargetPos, Color.red);

            if (fDist > Time.deltaTime)
            {
                transform.position += vDir * speed * Time.deltaTime;
            }
        }
    }
    void MoveDirectrion()
    {
        if (target != null)
        {
            float currentX = transform.position.x;
            float targetX = target.transform.position.x;
            float moveX = currentX - targetX;
            if (moveX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (moveX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
