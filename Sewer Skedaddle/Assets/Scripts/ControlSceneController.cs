using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSceneController : MonoBehaviour
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

    public void BackOnClick()
    {
        GameObject temp = GameObject.Find("LevelManager");
        temp.GetComponent<TransitionController>().loadLevel("StartScene");
    }
}
