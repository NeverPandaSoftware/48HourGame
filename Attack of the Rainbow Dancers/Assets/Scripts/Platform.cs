using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Platform : MonoBehaviour
{
    //private float timer = 5;
    public BeatValue beatValue = BeatValue.None;
    BeatObserver beatObserver;

    public bool enabled = true;

	// Use this for initialization
	void Start ()
    {
        beatObserver = GetComponent<BeatObserver>();
        renderer.material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
        {
            if (enabled && beatValue != BeatValue.None)
                Disable();
            else
                Enable();
        }

        /*timer -= Time.deltaTime;
        if (timer <= 0)
            Disable();*/
	}

    void Disable()
    {
        enabled = false;
        renderer.material.color = Color.red;
        collider2D.enabled = false;
    }

    void Enable()
    {
        enabled = true;
        renderer.material.color = Color.green;
        collider2D.enabled = true;
    }
}
