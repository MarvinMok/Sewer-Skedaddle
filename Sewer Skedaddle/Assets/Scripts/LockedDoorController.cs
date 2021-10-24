using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoorController : MonoBehaviour
{
    public bool isFinalDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("door touched by player");
            if (collision.transform.gameObject.GetComponent<PlayerController>().HasKey())
            {
                Debug.Log("door should open cause player has key");
                collision.transform.gameObject.GetComponent<PlayerController>().DecrementKeys();
                if (isFinalDoor)
                {
                    GameObject temp = GameObject.Find("LevelManager");
                    temp.GetComponent<TransitionController>().loadLevel("WinScene");
                }
                else
                {
                    GameObject temp = GameObject.Find("LevelManager");
                    temp.GetComponent<TransitionController>().loadLevel("StartScene");
                }
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
