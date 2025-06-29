using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EagleResponer responerEagle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEagle();
    }

    void UpdateEagle()
    {
        if(responerEagle.Eagle)
        {
            Eagle eagle = responerEagle.Eagle.GetComponent<Eagle>();
            if (eagle != null)
            {
                eagle.EaglePosition = responerEagle.gameObject;
            }
        }
    }
}
