using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float ms = 3.0f;
    public float changeSpeed;
    public float maxSpeed;
    private float timer;

    public float slowDistance;
    
    float onCamSpeed = 2.0f;

    float offScreenMs;
    bool onScreen = true;
    void Start()
    {

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (GameObject.Find("Player") == null)
        {
            ms = 0;
            GameObject temp = GameObject.Find("LevelManager");
            temp.GetComponent<TransitionController>().loadLevel("GameOverScene");

        }
        if (GameObject.Find("Player") != null)
        {
            if (GameObject.Find("Player").transform.position.x - transform.position.x <= slowDistance)
            {
                Debug.Log("within distance");
                onScreen = true;
            }
            else
            {
                Debug.Log("exited distance");
                onScreen = false;
            }
        }
    }

    public void FixedUpdate()
    {

        if (timer > 2)
        {
            if (ms < maxSpeed)
            {
                ms += Time.deltaTime * changeSpeed;

            }           
        }

        if(onScreen)
        {
        	//Debug.Log("on");
        	transform.position += new Vector3(onCamSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
        	//Debug.Log("off");
        	transform.position += new Vector3(ms * Time.deltaTime, 0, 0);
        }
        
     	
        
    }

    //void OnBecameVisible()
    //{

    //	onScreen = true;
    //}

    //void OnBecameInvisible()
    //{
    //	onScreen = false;
    //}

    void LateUpdate()
    {
        //Debug.Log(transform.lossyScale.x);
        //Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 temp = transform.position;

        temp.x = Mathf.Clamp(temp.x, Camera.main.transform.position.x - 18f, float.MaxValue);
        transform.position = temp;
    }
}
