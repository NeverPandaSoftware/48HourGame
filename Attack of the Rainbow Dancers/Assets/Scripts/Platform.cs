using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    private float timer = 5;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Disable();
	}

    void Disable()
    {
        collider2D.enabled = false;
    }

    void Enable()
    {
        collider2D.enabled = true;
    }
}
