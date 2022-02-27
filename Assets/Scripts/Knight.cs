using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    public float speed;
    private float move;

    public float jumpforce;
    public bool isJumping;

    public bool isdead;
    public bool ismove;

    public Text UnlockedText;

    public AudioSource JumpAudio; //once
    public AudioSource UnlockKeyAudio; //once
    public AudioSource collectKeyAudio; //once
    public AudioSource CaughtSpiralAudio; //once

    public float WaitforTime = 0.5f;

    public GameManager gameManager;

    [Header("Knight Keys")]
    [SerializeField]
    public int KnightKeys;
    public Text KnightKeysText;

    Rigidbody2D rbody;
    Animator anim;

    public void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        KnightKeys = 0;
        ismove = true;
        isdead = false;
    }

    private void Update()
    {
        if (ismove)
        {
            Movement();
            jump();
        }
        else
        {

        }
        Animations();

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
    public int KnightKeysCollected()
    {
        return KnightKeys;
    }
    //collectables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.CompareTag("WhitePlayer"))
        {
            if (collision.CompareTag("WhiteKey"))
            {
                //play collect audio
                collectKeyAudio.Play();
                //add
                KnightKeys++;
                //destroy
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Spiral"))
        {
            //game over
            Debug.Log("is dead is true");

            isdead = true;
            ismove = false;

            CaughtSpiralAudio.Play();
        }
        if (collision.CompareTag("WhiteKeyUnlock"))
        {
            //play Unlock sound

            //add one to the unlock keys
            if (KnightKeys == 6)
            {
                gameManager.UnlockedKeys++;

                Invoke("Unlock", WaitforTime);

                UnlockedText.gameObject.SetActive(true);

                UnlockKeyAudio.Play();
            }
        }
    }

    public void Unlock()
    {
        ismove = false;
    }
}
