using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 1;

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
