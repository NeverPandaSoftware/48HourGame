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
    private int beat = 3;
    private int danceStartBeat = 3;
    private bool stoppedToDance = false;
    private bool readyToMove = false;
    private bool canMove = true;

    public Transform checkPointParent;

    public Transform[] checkpoints;
    private int currentCheckPoint = 0;
    private float audioResetTime = 20.0f;

    private RaycastHit2D platform;

    private BeatObserver beatObserver;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        beatObserver = GetComponent<BeatObserver>();

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
        if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
        {

            if (beat <= 3)
                beat++;
            else
            {
                beat = 1;
            }
            Debug.Log("BEAT: " + beat);
        }

        if (readyToMove && beat == danceStartBeat)
        {
            canMove = true;
            readyToMove = false;
        }
	}

    void FixedUpdate()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, whatIsGround);

        if (grounded)
            platform = Physics2D.Linecast(transform.position, groundCheck.transform.position, whatIsGround);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        if (musicPlayer.GetComponent<AudioSource>().isPlaying && canMove)
            move = moveSpeed;
        else
            move = 0;

        bool onActive = platform.transform.gameObject.GetComponent<Platform>().alwaysActive;

        if (Input.GetButtonDown("Dance") && onActive)
        {
            danceStartBeat = beat;
        }

        if (Input.GetButton("Dance") && onActive)
        {
            canMove = false;
            readyToMove = false;
            anim.SetBool("Dance", true);
        }
        else
        {
            anim.SetBool("Dance", false);
            readyToMove = true;
        }

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
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (other.gameObject == checkpoints[i].gameObject)
                    currentCheckPoint = i;
            }
            Checkpoint cp = other.GetComponent<Checkpoint>();
            audioResetTime = cp.audioTime;
        }
    }
}
