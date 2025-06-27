using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleResponer : MonoBehaviour
{
    public float curTime = -1;
    public float MaxTime = 1;
    public GameObject Eagle;
    public GameObject prefab_Eagle;
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
                Eagle = Instantiate(prefab_Eagle, transform.position, Quaternion.identity);
                //Eagle.GetComponent<Eagle>().EaglePosition = this.gameObject;
                curTime = -1;
            }
        }
        else
        {
            if (Eagle == null)
            {
                curTime = 0;
            }
        }
    }
}