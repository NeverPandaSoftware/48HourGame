using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public float moveSpeed = 3;

    public Vector2 resetLocation;

	// Use this for initialization
	void Start ()
    {
        resetLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
	}

    public void Reset()
    {
        transform.position = resetLocation;
    }
}
