using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerObject;

    private bool dancing;
    private PlayerController player;
    private float danceTime = 0.0f;

    private float timer;
    private float spawnTime = 10;

    public GameObject dancerPreFab;

	// Use this for initialization
	void Start ()
    {
        player = playerObject.GetComponent<PlayerController>();
        timer = spawnTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        dancing = player.isDancing();

        if (dancing)
        {
            danceTime += Time.deltaTime;
        }
        else
        {
            danceTime -= Time.deltaTime;
        }
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = spawnTime;
            SpawnEnemy();
        }
	}

    void SpawnEnemy()
    {
        if (danceTime < -10)
        {
            SpawnStraight();
            SpawnAngleRight();
        }

        if (danceTime > -10 && danceTime < 0)
        {
            SpawnStraight();
            SpawnAngleLeft();
        }

        if (danceTime > 0 && danceTime < 10)
        {
            SpawnAngleLeft();
        }

        if (danceTime > 10)
        {

        }
    }

    void SpawnAngleLeft()
    {
        Vector3 spawnLocation = new Vector3(player.transform.position.x - 5, player.transform.position.y + 8, 0);
        GameObject dancer = (GameObject) Instantiate(dancerPreFab, spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.AngleFromLeft;
        dancer.GetComponent<Dancer>().player = player.transform;
    }

    void SpawnStraight()
    {
        Vector3 spawnLocation = new Vector3(player.transform.position.x, player.transform.position.y + 8, 0);
        GameObject dancer = (GameObject)Instantiate(dancerPreFab, spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.Straight;
        dancer.GetComponent<Dancer>().player = player.transform;
    }

    void SpawnAngleRight()
    {
        Vector3 spawnLocation = new Vector3(player.transform.position.x + 5, player.transform.position.y + 8, 0);
        GameObject dancer = (GameObject)Instantiate(dancerPreFab, spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.AngleFromRight;
        dancer.GetComponent<Dancer>().player = player.transform;
    }
}
