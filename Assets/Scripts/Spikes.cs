using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 1;
    public float speed = 0.2f;
    public float waitTime = 0.5f;
    public float maxY;
    public int spikStatus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        PlayerStatus ps = collision.gameObject.GetComponent<PlayerStatus>();
        if (ps != null)
        {
            ps.TakeDamage(damage);
        }
    }
}
