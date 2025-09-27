using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Responer : MonoBehaviour
{
    public float curTime = -1; 
    public float MaxTime;
    public GameObject player;
    public GameObject prefab_player;

    void Update()
    {
        if(curTime >= 0)
        {
            if(curTime < MaxTime)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                player = Instantiate(prefab_player, transform.position, Quaternion.identity);
                curTime = -1;
            }
        }
        else
        {
            if(player == null)
            {
                curTime = 0;
            }
        }
    }
}