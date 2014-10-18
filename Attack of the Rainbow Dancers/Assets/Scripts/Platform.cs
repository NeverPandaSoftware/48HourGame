using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Platform : MonoBehaviour
{
    //private float timer = 5;
    public BeatValue beatValue = BeatValue.None;
    BeatObserver beatObserver;

    public bool alwaysActive = false;
    public bool enabled = true;

	// Use this for initialization
	void Start ()
    {
        beatObserver = GetComponent<BeatObserver>();
        if (beatValue == BeatValue.None)
            alwaysActive = true;

        if (!alwaysActive)
        {
            if (enabled)
                renderer.material.color = Color.green;
            else
                renderer.material.color = Color.red;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
        {
            if (!alwaysActive)
            {
                if (enabled)
                    Disable();
                else
                    Enable();
            }
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
