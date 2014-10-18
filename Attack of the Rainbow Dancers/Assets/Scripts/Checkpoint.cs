using UnityEngine;
using System.Collections;
using SynchronizerData;

public class Checkpoint : MonoBehaviour
{
    public float audioTime;
    public bool activated = false;
    private BeatObserver beatObserver;
    public GameObject audioSource;

	// Use this for initialization
	void Start ()
    {
        beatObserver = GetComponent<BeatObserver>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
        {
            audioTime = audioSource.audio.time;
            //Debug.Log("Current time: " + audioTime);
        }
	}
}
