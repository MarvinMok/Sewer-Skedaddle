using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float bounceForce;
    bool died = false;
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
            Debug.Log("enemy died");
            died = true;
            collision.transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.transform.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                bounceForce);
            if (transform.parent.gameObject.CompareTag("Enemy"))
            {
                //BoxCollider2D box = transform.parent.gameObject.GetComponent<BoxCollider2D>();
                //CircleCollider2D circle = transform.parent.gameObject.GetComponent<CircleCollider2D>();
                //circle.enabled = false;
                //box.enabled = false;
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
            
            
        }        
    }

    private void OnBecameInvisible()
    {
        if (died == true)
        {
            Destroy(transform.parent.gameObject);
        }
        
    }
}
