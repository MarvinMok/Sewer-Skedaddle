using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOnClick()
    {
        GameObject temp = GameObject.Find("LevelManager");
        temp.GetComponent<TransitionController>().loadLevel("SampleScene");
    }

    public void ControlsOnClick()
    {
        GameObject temp = GameObject.Find("LevelManager");
        temp.GetComponent<TransitionController>().loadLevel("ControlScene");
    }

    public void TutorialOnClick()
    {
        GameObject temp = GameObject.Find("LevelManager");
        temp.GetComponent<TransitionController>().loadLevel("SampleScene 1");
    }
}
