using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
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
        Debug.Log(collision.transform.gameObject.tag);
        if (collision.transform.gameObject.CompareTag("Flying Enemy") || collision.transform.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            Debug.Log(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        if ((collision.transform.gameObject.CompareTag("Enemy") || 
            collision.transform.gameObject.CompareTag("Flying Enemy")) && transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y >= -1)
        {
            Debug.Log("You lose!");
            Destroy(transform.parent.gameObject);
        }
    }
}
