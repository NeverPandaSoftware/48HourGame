﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    bool facingRight = true;

    Animator anim;

    public bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float jumpForce = 2000;

    public GameObject musicPlayer;

    private float move = 0.0f;

    public Transform checkPointParent;

    public Transform[] checkpoints;
    private int currentCheckPoint = 0;
    private float audioResetTime = 20.0f;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        List<Transform> checkpts = new List<Transform>();

        foreach (Transform child in checkPointParent)
        {
            checkpts.Add(child);
        }

        checkpoints = checkpts.ToArray();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void FixedUpdate()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, whatIsGround);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        if (musicPlayer.GetComponent<AudioSource>().isPlaying)
            move = moveSpeed;
        else
            move = 0;

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move, rigidbody2D.velocity.y);

        if (grounded && Input.GetButton("Jump"))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (transform.position.y < -100)
            Respawn();

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Respawn()
    {
        Debug.Log("RESPAWN");
        musicPlayer.audio.Stop();
        transform.position = checkpoints[currentCheckPoint].transform.position;

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject p in platforms)
        {
            p.GetComponent<Platform>().ResetState();
        }

        musicPlayer.GetComponent<BeatSynchronizer>().RestartAudio(audioResetTime, 0);
        //musicPlayer.audio.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Checkpoint cp = other.GetComponent<Checkpoint>();
            audioResetTime = cp.audioTime;
        }
    }
}
