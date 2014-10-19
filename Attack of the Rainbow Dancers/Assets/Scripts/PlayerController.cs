using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    bool facingRight = true;

    public int attempts = 9;

    Animator anim;

    public bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float jumpForce = 2000;

    public GameObject musicPlayer;

    private float move = 0.0f;
    private bool dancing = false;
    private bool consumedByDance = false;
    private float danceTime = 0.0f;

    public Transform startPoint;

    private RaycastHit2D platform;

    private BeatObserver beatObserver;

    public GameObject AllTimeBestUFO;
    public GameObject PersonalBestUFO;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        beatObserver = GetComponent<BeatObserver>();

        float atb = PlayerPrefs.GetFloat("AllTimeBest");

        if (atb > 0)
        {
            GameObject atbUFO = (GameObject)Instantiate(AllTimeBestUFO, new Vector2(atb, 3), Quaternion.identity);
        }
        else
        {
            PlayerPrefs.SetFloat("AllTimeBest", 100);
            GameObject atbUFO = (GameObject)Instantiate(AllTimeBestUFO, new Vector2(100, 3), Quaternion.identity);
        }

        float pb = PlayerPrefs.GetFloat("PersonalBest");

        if (pb > 0)
        {
            GameObject pbUFO = (GameObject)Instantiate(PersonalBestUFO, new Vector2(pb, 3), Quaternion.identity);
        }
        else
        {
            PlayerPrefs.SetFloat("PersonalBest", 50);
            GameObject pbUFO = (GameObject)Instantiate(PersonalBestUFO, new Vector2(50, 3), Quaternion.identity);
        }
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

        if ((Input.GetButton("Dance") && grounded) || consumedByDance)
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
        {
            if (transform.position.x > PlayerPrefs.GetFloat("PersonalBest"))
            {
                PlayerPrefs.SetFloat("PersonalBest", transform.position.x);
            }

            if (transform.position.x > PlayerPrefs.GetFloat("AllTimeBest"))
            {
                PlayerPrefs.SetFloat("AllTimeBest", transform.position.x);
            }

            Respawn();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dancer" && grounded)
        {
            consumedByDance = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dancer")
        {
            Debug.Log("EXIT");
            consumedByDance = false;
        }
    }

    void Respawn()
    {
        Debug.Log("RESPAWN");
        
        attempts--;
        if (attempts == 0)
        {
            EndGame();
        }

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

    public bool isDancing()
    {
        return dancing;
    }

    public void EndGame()
    {
        Application.LoadLevel("Menu");
    }
}
