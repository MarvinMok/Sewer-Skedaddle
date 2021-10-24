using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (GameObject.Find("Player") != null)
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z), time);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("You lose!");
        }
    }
}
