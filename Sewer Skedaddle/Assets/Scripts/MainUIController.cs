using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Button pauseBtn;
    public Sprite pause;
    public Sprite play;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseOnClick()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseBtn.image.sprite = play;
            Time.timeScale = 0f;
            Camera.main.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            pauseBtn.image.sprite = pause;
            Time.timeScale = 1f;
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
    }
}
