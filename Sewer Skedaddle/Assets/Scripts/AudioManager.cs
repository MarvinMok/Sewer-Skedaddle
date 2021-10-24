using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject manager = new GameObject();
                    manager.hideFlags = HideFlags.HideAndDontSave;
                    instance = manager.AddComponent<AudioManager>();
                }
            }
            return instance;
        }        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySteps()
    {
        GameObject.Find("Steps").GetComponent<AudioSource>().Play();
    }

    public void StopSteps()
    {
        GameObject.Find("Steps").GetComponent<AudioSource>().Stop();
    }

    public void PlayBugTransformation()
    {
        StartCoroutine(Transform("BugTransformation"));
    }

    public void PlayCatTransformation()
    {
        StartCoroutine(Transform("CatTransformation"));
    }

    public void PlayFrogTransfromation()
    {
        StartCoroutine(Transform("FrogTransformation"));
    }

    public void PlayJump()
    {
        GameObject.Find("Jump").GetComponent<AudioSource>().Play();
    }

    IEnumerator Transform(string sfx)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find(sfx).GetComponent<AudioSource>().Play();
    }
}
