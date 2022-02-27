using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private float move;

    public float jumpforce;
    public bool isJumping;

    public bool isdead;
    public bool ismove;

    public GameManager gameManager;

    [Header("Knight")]
    [SerializeField]
    private int KnightKeys;
    public Text KnightKeysText;

    [Header("Ninja")]
    [SerializeField]
    private int NinjaKeys;
    public Text NinjaKeyText;

    Rigidbody2D rbody;
    Animator anim;

    public void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        NinjaKeys = 0;
        KnightKeys = 0;
    }

    private void Update()
    {
        Movement();
        jump();
        Animations();

        NinjaKeyText.text = NinjaKeysCollected().ToString();
        KnightKeysText.text = KnightKeysCollected().ToString();
    }
    public void Movement()
    {
        move = Input.GetAxisRaw("Horizontal");
        rbody.velocity = new Vector2(move * speed, rbody.velocity.y);

        if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }
    public void jump()
    {
        if (Input.GetButtonDown("Jump") && rbody.velocity.y == 0)
        {
            rbody.AddForce(Vector2.up * jumpforce);
            isJumping = true;

            if (rbody.velocity.y == 0)
            {
                isJumping = false;
            }
            if (rbody.velocity.y > 0)
            {
                isJumping = true;
            }
            if (rbody.velocity.y < 0)
            {
                isJumping = false;
            }
        }

    }
    public void Animations()
    {
        anim.SetFloat("movement", Mathf.Abs(move));
        anim.SetBool("isjumping", isJumping);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //white player
        if (gameManager.KnightMovement == true)
        {
            if (collision.gameObject.CompareTag("BlackTile"))
            {
                collision.collider.isTrigger = true;
            }
            else
            {
                collision.collider.isTrigger = false;
            }
        }
        //black player
        else if (gameManager.NinjaMovement == true)
        {
            if(collision.gameObject.CompareTag("WhiteTile"))
            {
                collision.collider.isTrigger = true;
            }
            else
            {
                collision.collider.isTrigger = false;
            }
        }
    }
    public int KnightKeysCollected()
    {
        return KnightKeys;
    }
    public int NinjaKeysCollected()
    {
        return NinjaKeys;
    }
    //collectables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.CompareTag("WhitePlayer"))
        {
            if (collision.CompareTag("WhiteKey"))
            {
                //play collect audio

                //add
                KnightKeys++;
                //destroy
                Destroy(collision.gameObject);
            }
        }
        else if(this.CompareTag("BlackPlayer"))
        {
            if (collision.CompareTag("BlackKey"))
            {
                //play collect audio

                //add
                NinjaKeys++;
                //destroy
                Destroy(collision.gameObject);
            }
        }
    }
}
