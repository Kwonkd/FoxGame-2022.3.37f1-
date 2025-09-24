using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelinControl_Stage3 : TimelineControl
{
    public override void StageClear()
    {
        endDirector.Play();
        Invoke("NextStage", 2f);
    }
    public override void NextStage()
    {
        SceneManager.LoadScene("End");
    }
}
