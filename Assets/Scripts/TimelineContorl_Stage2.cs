using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineContorl_Stage2 : TimelineControl
{

    public override void StageClear()
    {
        endDirector.Play();
        Invoke("NextStage", 5f);
    }
    public override void NextStage()
    {
        SceneManager.LoadScene("Stage 3");
    }
}
