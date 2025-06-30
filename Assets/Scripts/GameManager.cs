using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EagleResponer responerEagle;
    public Responer responer;
    public CameraTracker cameraTracker;

    void UpdateEagle()
    {
        if (responerEagle.Eagle)
        {
            Eagle eagle = responerEagle.Eagle.GetComponent<Eagle>();
            if (eagle != null)
            {
                eagle.EaglePosition = responerEagle.gameObject;
            }
        }
    }
    void UpdateCamera()
    {
        if (cameraTracker)
        {
            //Debug.Log("GameManager UpdateCamera - responer.player: " + responer.player);
            //Debug.Log("GameManager UpdateCamera - cameraTracker.objTarget: " + cameraTracker.objTarget);

            if (responer.player != null && cameraTracker.objTarget != responer.player)
            {
                Debug.Log("GameManager UpdateCamera - Setting objTarget to new player!");
                cameraTracker.objTarget = responer.player;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEagle();
        UpdateCamera();
    }
}
