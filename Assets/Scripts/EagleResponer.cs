using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleResponer : MonoBehaviour
{
    public float curTime = -1;
    public float MaxTime;
    public GameObject player;
    public GameObject prefab_player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curTime >= 0)
        {
            if (curTime < MaxTime)
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
            if (player == null)
            {
                curTime = 0;
            }
        }
    }
}