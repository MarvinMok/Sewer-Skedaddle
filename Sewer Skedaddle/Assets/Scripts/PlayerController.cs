using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isWalking = false;

    [Header("Movement")]
    public float catMS = 6f;
    public float bugMS = 5f;
    public float frogMS = 4f;
    
    public float CatjumpStrength = 9f;
    public float BugjumpStrength = 6.5f;
    public float FrogjumpStrength = 12f;

    public float catGravity = 2f;
    public float frogGravity = 1.5f;
    public float bugGravity = 1.75f;

    public float xWallForce = 6f;
    public float yWallForce = 8f;
    float ms;
    float jumpStrength;

    

    [Header("Ground/Wall Check")]

    public float wallJumpTime = 0.25f;
    public float wallSlideSpeed = 3.0f;

    public float verticalCastLength = 0.5f;
    public float horizontalCastLength = 0.9f;

    public float catVertLength;
    public float catHoriLength;

    public float bugVertLength;
    public float bugHoriLength;

    public float frogVertLength;
    public float frogHoriLength;


    public LayerMask groundLayer;

    [Header("HitBoxes")]
    
    public GameObject Hitbox;
    
    public float catBoxXOffset;
    public float catBoxYOffset;
    public float catBoxXSize;
    public float catBoxYSize;
    public float catCircleXOffset;
    public float catCircleYOffset;
    public float catCircleRadius;

    public float bugBoxXOffset;
    public float bugBoxYOffset;
    public float bugBoxXSize;
    public float bugBoxYSize;
    public float bugCircleXOffset;
    public float bugCircleYOffset;
    public float bugCircleRadius;

    public float frogBoxXOffset;
    public float frogBoxYOffset;
    public float frogBoxXSize;
    public float frogBoxYSize;
    public float frogCircleXOffset;
    public float frogCircleYOffset;
    public float frogCircleRadius;

    bool canJumpLeft, canJumpRight;

    AudioSource walkNoise;
    
    Vector2 screenBounds;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    BoxCollider2D bc;
    CircleCollider2D cc;

    Vector2 velocity;
    float horizontal;
    bool onGround;
    bool wallJump = false;
    bool onWall;

    bool isTouchingRight;
    bool isTouchingLeft;
    bool wallSliding;


    
    PlayerState curState = PlayerState.cat;
    Color c1 = Color.green;
    Color c2 = Color.white;

    private enum PlayerState
    {
        cat,
        bug,
        frog
    };

    public int numKeys = 0;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        
        jumpStrength = CatjumpStrength;
        ms = catMS;
        rb.gravityScale = catGravity;

        changeForm();

    }

    // Update is called once per frame
    void Update()
    {
        

        onGround = Physics2D.Raycast(transform.position, Vector2.down, verticalCastLength, groundLayer);
        isTouchingRight = Physics2D.Raycast(transform.position, Vector2.right, horizontalCastLength, groundLayer);
        isTouchingLeft = Physics2D.Raycast(transform.position, Vector2.left, horizontalCastLength, groundLayer);

        if(onGround || isTouchingLeft || isTouchingRight)
        {
            //Debug.Log("jump=f");
            anim.SetBool("jumping", false);
        }

        velocity = Vector2.zero;
        horizontal = Input.GetAxis("Horizontal");

        if (!isWalking && horizontal != 0)
        {
            isWalking = true;
            AudioManager.Instance.PlaySteps();
        }
        else if (isWalking && horizontal == 0)
        {
            isWalking = false;
            AudioManager.Instance.StopSteps();
        }

        flip();

       
        
        if (onGround)
        {
            canJumpLeft = true;
            canJumpRight = true;
            wallJump = false;
        }
        

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpUpdate();
        }

        /*if (isTouchingLeft || isTouchingRight && !onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, float.MinValue, 0));
        }*/

        if ((isTouchingLeft || isTouchingRight) && horizontal != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
        
        if(wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        getForm();
        anim.SetBool("isGrounded", onGround);
        anim.SetBool("moving", rb.velocity.x != 0f);
        anim.SetBool("onWall", (isTouchingLeft || isTouchingRight));
        //anim.SetFloat("yVelocity", rb.velocity.y);

    }

    public void FixedUpdate()
    {       
        
        jumpFixedUpdate();

    }


    void LateUpdate()
    {
        //Debug.Log(transform.lossyScale.x);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x - Mathf.Abs(transform.lossyScale.x) / 2f),
            Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y), transform.position.z);
    }

    void jumpUpdate()
    {
        //Debug.Log("Uparrow");
        if (onGround)
        {
            //rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            rb.velocity = Vector2.up * jumpStrength;
            //Debug.Log("norm");
            //Debug.Log("jump=true");
            anim.SetBool("jumping", true);
            //Invoke("animJumpToFalse", 0.1f);
            AudioManager.Instance.PlayJump();    
            
        }
        
        else if (curState == PlayerState.cat && (isTouchingLeft || isTouchingRight))
        {
            wallJump = true;
            
            
            if(isTouchingLeft && canJumpLeft)
            {   
                rb.velocity = new Vector2(horizontal + xWallForce, 0);
                canJumpLeft = false;
                canJumpRight = true;
                rb.velocity = new Vector2(rb.velocity.x, yWallForce);
                Invoke("wallJumpToFalse", wallJumpTime);
                //Debug.Log("jump=true");
                anim.SetBool("jumping", true);
                //Invoke("animJumpToFalse", 0.1f);
                AudioManager.Instance.PlayJump();
                
                

            }
            else if (isTouchingRight && canJumpRight)
            {
                rb.velocity = new Vector2(horizontal - xWallForce, 0);
                canJumpLeft = true;
                canJumpRight = false;
                rb.velocity = new Vector2(rb.velocity.x, yWallForce);
                Invoke("wallJumpToFalse", wallJumpTime);
                //Debug.Log("jump=true");
                anim.SetBool("jumping", true);
                //Invoke("animJumpToFalse", 0.1f);
                AudioManager.Instance.PlayJump();               
                

            }          
        }
        
    }

    void jumpFixedUpdate()
    {
        if(!wallJump)
            {
                if(onGround)
                {
                    rb.velocity = new Vector2(horizontal * ms, rb.velocity.y);
                }
                else
                {
                    if(!canJumpRight && horizontal > 0)
                    {
                        rb.velocity = new Vector2(Mathf.Clamp(horizontal * ms, -float.MaxValue, horizontal), rb.velocity.y);
                        transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                    else if (!canJumpLeft && horizontal < 0)
                    {
                        rb.velocity = new Vector2(Mathf.Clamp(horizontal * ms, horizontal, float.MaxValue), rb.velocity.y);
                        transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                    else
                    {
                        rb.velocity = new Vector2(horizontal * ms, rb.velocity.y);
                    }
                }
            }
    }

    void flip()
    {
        if(!onGround && (isTouchingRight || isTouchingLeft))
        {
            if (isTouchingRight)
            {
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (isTouchingLeft)
            {
                transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        
    }

    void getForm()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            curState = PlayerState.cat;
            changeForm();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            curState = PlayerState.bug;
            changeForm();
        }
        else if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            curState = PlayerState.frog;
            changeForm();
        }
    }

    void changeForm()
    {
        if (curState == PlayerState.cat)
        {

            toCat();   
        }
        else if (curState == PlayerState.bug)
        {

            toBug();
        }
        else //if(curState == PlayerState.frog)
        {

            toFrog();
        }
    }

    void toCat()
    {
        AudioManager.Instance.PlayCatTransformation();
        anim.SetInteger("animal", 0);

        jumpStrength = CatjumpStrength;
        ms = catMS;
        rb.gravityScale = catGravity;
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //sr.color = c2;
        
        horizontalCastLength = catHoriLength;
        verticalCastLength = catVertLength;

        bc.offset = new Vector2(catBoxXOffset, catBoxYOffset);
        bc.size = new Vector2(catBoxXSize, catBoxYSize);

        cc.offset = new Vector2(catCircleXOffset, catCircleYOffset);
        cc.radius = catCircleRadius;

        Hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(catBoxXOffset, catBoxYOffset);
        Hitbox.GetComponent<BoxCollider2D>().size = new Vector2(catBoxXOffset, catBoxYOffset);
    }

    void toBug()
    {
        AudioManager.Instance.PlayBugTransformation();
        anim.SetInteger("animal", 1);

        jumpStrength = BugjumpStrength;
        ms = bugMS;
        rb.gravityScale = bugGravity;
        //transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //sr.color = c2;
        
        horizontalCastLength = bugHoriLength;
        verticalCastLength = bugVertLength;

        bc.offset = new Vector2(bugBoxXOffset, bugBoxYOffset);
        bc.size = new Vector2(bugBoxXSize, bugBoxYSize);

        cc.offset = new Vector2(bugCircleXOffset, bugCircleYOffset);
        cc.radius = bugCircleRadius;

        Hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(bugBoxXOffset, bugBoxYOffset);
        Hitbox.GetComponent<BoxCollider2D>().size = new Vector2(bugBoxXOffset, bugBoxYOffset);
    
    }

    void toFrog()
    {
        AudioManager.Instance.PlayFrogTransfromation();
        anim.SetInteger("animal", 2);

        jumpStrength = FrogjumpStrength;
        ms = frogMS;
        rb.gravityScale = frogGravity;
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //sr.color = c1;

        horizontalCastLength = frogHoriLength;
        verticalCastLength = frogVertLength;

        bc.offset = new Vector2(frogBoxXOffset, frogBoxYOffset);
        bc.size = new Vector2(frogBoxXSize, frogBoxYSize);

        cc.offset = new Vector2(frogCircleXOffset, frogCircleYOffset);
        cc.radius = frogCircleRadius;

        Hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(frogBoxXOffset, frogBoxYOffset);
        Hitbox.GetComponent<BoxCollider2D>().size = new Vector2(frogBoxXOffset, frogBoxYOffset);
    
    }

    void wallJumpToFalse()
    {
        wallJump = false;
    }

    void animJumpToFalse()
    {
    	anim.SetBool("jumping", false);
    }
    public void IncrementKeys()
    {
        numKeys++;
    }

    public void DecrementKeys()
    {
        numKeys--;
    }

    public bool HasKey()
    {
        return numKeys > 0;
    }
}
