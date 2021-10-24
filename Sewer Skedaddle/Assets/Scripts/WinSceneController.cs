using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuOnClick()
    {
        GameObject temp = GameObject.Find("LevelManager");
        temp.GetComponent<TransitionController>().loadLevel("StartScene");
    }

    public void QuitOnClick()
    {
        Application.Quit();
    }
}
