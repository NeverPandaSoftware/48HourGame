using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SynchronizerData;

public class PlatformInitializer : MonoBehaviour
{
    public BeatCounter wholeBeatCounter;
    public BeatCounter quarterBeatCounter;
    public BeatCounter eighthBeatCounter;
    public BeatCounter sixteenthBeatCounter;

	// Use this for initialization
	void Start ()
    {
	    GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        List<GameObject> wholeBeatPlatforms = new List<GameObject>();
        List<GameObject> quarterBeatPlatforms = new List<GameObject>();
        List<GameObject> eighthBeatPlatforms = new List<GameObject>();
        List<GameObject> sixteenthBeatPlatforms = new List<GameObject>();

        foreach (GameObject platform in platforms)
        {
            BeatValue beatValue = platform.GetComponent<Platform>().beatValue;

            switch (beatValue)
            {
                case BeatValue.WholeBeat:
                    wholeBeatPlatforms.Add(gameObject);
                    break;
                case BeatValue.QuarterBeat:
                    quarterBeatPlatforms.Add(gameObject);
                    break;
                case BeatValue.EighthBeat:
                    eighthBeatPlatforms.Add(gameObject);
                    break;
                case BeatValue.SixteenthBeat:
                    sixteenthBeatPlatforms.Add(gameObject);
                    break;

                default:
                    quarterBeatPlatforms.Add(gameObject);
                    break;
            }

            wholeBeatCounter.observers = wholeBeatPlatforms.ToArray();
            quarterBeatCounter.observers = quarterBeatPlatforms.ToArray();
            eighthBeatCounter.observers = eighthBeatPlatforms.ToArray();
            sixteenthBeatCounter.observers = sixteenthBeatPlatforms.ToArray();
        }

	}
}
