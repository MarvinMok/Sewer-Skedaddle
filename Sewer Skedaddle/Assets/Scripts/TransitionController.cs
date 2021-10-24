using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public Animator animator;
    public float transitionDelayTime = 1.0f;

    void Awake()
    {
        animator = GameObject.Find("Transition").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //As an example, we'll be using GetKey() to test out the transition
        //between game scenes, so if you are implementing this with this code
        //make sure to modify the code according to your needs.
        
     
    }

    public void loadLevel(string scenename)
    {
        StartCoroutine(DelayLoadLevel(scenename));
    }

    IEnumerator DelayLoadLevel(string scenename)
    {
        animator.SetTrigger("TriggerTransition");
        yield return new WaitForSeconds(transitionDelayTime);
        SceneManager.LoadScene(scenename);
    }
}
