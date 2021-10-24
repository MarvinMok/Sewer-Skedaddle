using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    public LayerMask ground;
    public Transform rightCheck;

    [Header("Stats")]
    public float width;
    public float height;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        if (Random.Range(0, 2) == 1)
        {
            Turn();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics2D.OverlapCircle(rightCheck.position, 0.1f, ground))
        {
            Turn();
        }
    }

    void Turn()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed = -speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
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
        else if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            Turn();
        }
    }
}
