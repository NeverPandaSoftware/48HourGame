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
    public bool consumedByDance = false;

    public Transform startPoint;

    private RaycastHit2D platform;

    public GameObject AllTimeBestUFO;
    public GameObject PersonalBestUFO;

    public GameObject end;
    private bool atEnd = false;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        float atb = PlayerPrefs.GetInt("highscorePos1");

        if (atb > 0)
        {
            GameObject atbUFO = (GameObject)Instantiate(AllTimeBestUFO, new Vector2(atb, 0), Quaternion.identity);
            atbUFO.name = "AllTimeBest";
        }
        else
        {
            PlayerPrefs.SetInt("highscorePos2", 100);
            GameObject atbUFO = (GameObject)Instantiate(AllTimeBestUFO, new Vector2(100, 0), Quaternion.identity);
            atbUFO.name = "AllTimeBest";
        }

        float pb = PlayerPrefs.GetFloat("PersonalBest");

        if (pb > 0)
        {
            GameObject pbUFO = (GameObject)Instantiate(PersonalBestUFO, new Vector2(pb, 0), Quaternion.identity);
            pbUFO.name = "PersonalBest";
        }
        else
        {
            PlayerPrefs.SetFloat("PersonalBest", 50);
            GameObject pbUFO = (GameObject)Instantiate(PersonalBestUFO, new Vector2(50, 0), Quaternion.identity);
            pbUFO.name = "PersonalBest";
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

        if (musicPlayer.GetComponent<AudioSource>().isPlaying || atEnd)
            move = moveSpeed;
        else
            move = 0;

        if(consumedByDance)
        {
            dancing = true;
            anim.SetBool("Dance", true);
        }

        if ((Input.GetButton("Dance") && grounded))
        {
            dancing = true;
            consumedByDance = false;
            anim.SetBool("Dance", true);
        }
        else
        {
            if (!consumedByDance)
            {
                dancing = false;
                anim.SetBool("Dance", false);
            }
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

        if (col.gameObject.tag == "Finish")
        {
            anim.SetBool("End", true);
            atEnd = true;
            col.gameObject.GetComponent<HighScore>().OnLevelComplete(715);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dancer")
        {
            Debug.Log("EXIT");
            //consumedByDance = false;
        }

        if (col.gameObject.tag == "Finish")
        {
            EndGame();
        }
    }

    void Respawn()
    {
        Debug.Log("RESPAWN");
        
        attempts--;
        if (attempts == 0)
        {
            end.GetComponent<HighScore>().OnLevelComplete(Mathf.RoundToInt(transform.position.x));
            EndGame();
        }

        GameObject[] dancers = GameObject.FindGameObjectsWithTag("Dancer");

        foreach(GameObject d in dancers)
        {
            d.GetComponent<Dancer>().destroy(); ;
        }

        musicPlayer.audio.Stop();

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject p in platforms)
        {
            p.GetComponent<Platform>().ResetState();
        }

        musicPlayer.GetComponent<BeatSynchronizer>().RestartAudio();

        transform.position = startPoint.transform.position;
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
        PlayerPrefs.Save();
        Application.LoadLevel("Menu");
    }
}
