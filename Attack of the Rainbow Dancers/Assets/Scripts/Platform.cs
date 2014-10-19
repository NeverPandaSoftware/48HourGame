using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Platform : MonoBehaviour
{
    //private float timer = 5;
    BeatObserver beatObserver;

    private bool startState;

    public bool alwaysActive = false;
    public bool enabled = true;
    public bool doUpdates = true;

	// Use this for initialization
	void Start ()
    {
        startState = enabled;
        //enabled = !enabled;

        beatObserver = GetComponent<BeatObserver>();

        if (!alwaysActive)
        {
            if (enabled)
            {
                //renderer.material.color = Color.green;
                GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 1f);
            }
            else
            {
                //renderer.material.color = Color.red;
                GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, .3f);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!alwaysActive)
        {
            if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
            {
                if (doUpdates)
                {
                    if (enabled)
                        Disable();
                    else
                        Enable();
                }
            }
        }
    }

    void Disable()
    {
        enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, .3f);
        collider2D.enabled = false;
    }

    void Enable()
    {
        enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 1f);
        collider2D.enabled = true;
    }

    public void ResetState()
    {
        enabled = startState;
    }
}
