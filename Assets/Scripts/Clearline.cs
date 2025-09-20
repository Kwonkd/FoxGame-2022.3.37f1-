using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearline : MonoBehaviour
{
    TimelineControl tc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(tc == null)
            {
                tc = FindObjectOfType<TimelineControl>();
            }
            else
            {
                tc.StageClear();
            }
        }
    }
}
