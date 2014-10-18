using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;

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
    private bool dancing = false;

    public Transform startPoint;

    private RaycastHit2D platform;

    private BeatObserver beatObserver;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        beatObserver = GetComponent<BeatObserver>();
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

        if (Input.GetButton("Dance") && grounded)
        {
            dancing = true;
            anim.SetBool("Dance", true);
        }
        else
        {
            dancing = false;
            anim.SetBool("Dance", false);
        }

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move, rigidbody2D.velocity.y);

        if (!dancing && grounded && Input.GetButton("Jump"))
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

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject p in platforms)
        {
            p.GetComponent<Platform>().ResetState();
        }

        musicPlayer.GetComponent<BeatSynchronizer>().RestartAudio();

        transform.position = startPoint.transform.position;
        GameObject mob = GameObject.FindGameObjectWithTag("Mob");
        mob.GetComponent<Mob>().Reset();
    }

    void stopPlatformUpdates()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject p in platforms)
        {
            p.GetComponent<Platform>().doUpdates = false;
        }
    }

    void startPlatformUpdates()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject p in platforms)
        {
            p.GetComponent<Platform>().doUpdates = true;
        }
    }
}
