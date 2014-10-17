using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 4;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float jumpForce = 500;

    private float move = 0.0f;

	// Use this for initialization
	void Start ()
    {
        //anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, whatIsGround);

        //anim.SetBool("Ground", grounded);
        //anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        move = Input.GetAxis("Horizontal");

        //anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
