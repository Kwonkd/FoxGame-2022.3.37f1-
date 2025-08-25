using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 5;
    public int currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(currentHp > 0)
        {
            currentHp -= damage;
        }
    }
}
