using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector endDirector;
    private void Awake()
    {
        endDirector.Pause();
    }

    public void StageClear()
    { 
        endDirector.Play();
    }
}
