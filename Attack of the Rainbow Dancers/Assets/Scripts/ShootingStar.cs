using UnityEngine;
using System.Collections;

public class ShootingStar : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(transform.position.x - 3 * Time.deltaTime, transform.position.y - 6 * Time.deltaTime);

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
	}
}
