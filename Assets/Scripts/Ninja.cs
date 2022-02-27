using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    public float speed;
    private float move;

    public float jumpforce;
    public bool isJumping;

    public bool isdead = false;
    public bool ismove = true;

    public GameManager gameManager;

    public AudioSource JumpAudio; //once
    public AudioSource UnlockKeyAudio; //once
    public AudioSource collectKeyAudio; //once
    public AudioSource CaughtSpiralAudio; //once

    public float WaitforTime = 2f;

    public Text UnlockedTxt;

    [Header("Ninja Keys")]
    [SerializeField]
    public int NinjaKeys;
    public Text NinjaKeyText;

    Rigidbody2D rbody;
    Animator anim;

    public void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        NinjaKeys = 0;
        ismove = true;
        isdead = false;
    }

    private void Update()
    {
        Animations();

        NinjaKeyText.text = NinjaKeysCollected().ToString();

        if(ismove)
        {
            Movement();
            jump();
        }
        else
        {

        }
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
            JumpAudio.Play();

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
        if (collision.gameObject.CompareTag("WhiteTile"))
        {
            collision.collider.isTrigger = true;
        }
        else
        {
            //make istrigger to false
            collision.collider.isTrigger = false;
        }
    }
    public int NinjaKeysCollected()
    {
        return NinjaKeys;
    }
    //collectables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.CompareTag("BlackPlayer"))
        {
            if (collision.CompareTag("BlackKey"))
            {
                //play collect audio
                collectKeyAudio.Play();
                //add
                NinjaKeys++;
                //destroy
                Destroy(collision.gameObject);
            }
        }
        if(collision.CompareTag("Spiral"))
        {
            CaughtSpiralAudio.Play();
            //game over
            Debug.Log("is dead is true");

            isdead = true;
            ismove = false;
        }
        if (collision.CompareTag("BlackKeyUnlock"))
        {
            //play Unlock sound

            //add one to the unlock keys
            if(NinjaKeys == 6)
            {
                UnlockKeyAudio.Play();

                gameManager.UnlockedKeys++;

                Invoke("EndUnlock", WaitforTime);

                UnlockedTxt.gameObject.SetActive(true);
            }

            
        }
    }

    public void EndUnlock()
    {
        ismove = false;
    }
}
