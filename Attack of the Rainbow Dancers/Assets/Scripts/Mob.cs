using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public float moveSpeed = 3;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
	}
}
