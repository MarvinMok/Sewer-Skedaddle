using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
	bool toP1 = true;

	public float ms = 10f;
	public GameObject point1Object;
	public GameObject point2Object;

	Rigidbody2D rb;

	Vector3 position1;
	Vector3 position2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position1 = point1Object.transform.position;
        position2 = point2Object.transform.position;
    }	

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        if (Vector2.Distance (transform.position, position1) < 0.1f)
        {
        	toP1 = false;
        }
        else if (Vector2.Distance (transform.position, position2) < 0.1f)
        {
        	toP1 = true;
        }
        
        if(toP1)
        {
        	Vector3 direction = (position1 - transform.position).normalized;
        	rb.velocity = new Vector2(direction.x, direction.y) * ms;
        }
        else
        {
        	Vector3 direction = (position2 - transform.position).normalized;
        	rb.velocity = new Vector2(direction.x, direction.y) * ms;
        }
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.transform.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (collision.transform.gameObject.GetComponent<Rigidbody2D>().velocity.y != 10)
            {
                Destroy(collision.transform.gameObject);
            }
        }        
    }
}
