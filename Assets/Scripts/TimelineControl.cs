using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector endDirector;
    public PlayableDirector startDirector;
    virtual public void StageClear()
    { 
        endDirector.Play();
        Invoke("NextStage", 3f);
    }
    public void StopDirector()
    {
        startDirector.Stop();
    }
    virtual public void NextStage()
    {
        SceneManager.LoadScene("Stage 2");
    }
}
