using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearline : MonoBehaviour
{
    [SerializeField]
    private TimelineControl tc;

    private bool isPlay = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlay == false)
        {
            if(tc != null)
            {
                isPlay = true;
                tc.StageClear();
            }
        }
    }
}
