using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerObject;

    private bool dancing;
    private PlayerController player;
    private float danceTime = 0.0f;

    public bool spawningEnabled = false;

    private float timer;
    private float spawnTime = 10;

    private float starTimer;
    public GameObject star;
    public GameObject[] dancerPreFabs;

	// Use this for initialization
	void Start ()
    {
        player = playerObject.GetComponent<PlayerController>();
        timer = spawnTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.transform.position.x > 75 && !spawningEnabled)
        {
            InitialSpawn();
        }

        dancing = player.isDancing();

        if (dancing)
        {
            danceTime += Time.deltaTime;
        }
        else
        {
            danceTime -= Time.deltaTime;
        }

        if (spawningEnabled)
        {
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

        if (starTimer > 0)
        {
            starTimer -= Time.deltaTime;
        }
        else
        {
            starTimer = Random.Range(3, 10);
            Instantiate(star, new Vector2(player.transform.position.x + Random.Range(-5, 15), player.transform.position.y + 8), Quaternion.identity);
        }
	}

    void InitialSpawn()
    {
        SpawnStraight();
        SpawnAngleRight();
        SpawnAngleLeft();
        spawningEnabled = true;
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
        GameObject dancer = (GameObject) Instantiate(dancerPreFabs[Random.Range(0, 2)], spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.AngleFromLeft;
        dancer.GetComponent<Dancer>().player = player.transform;
    }

    void SpawnStraight()
    {
        Vector3 spawnLocation = new Vector3(player.transform.position.x, player.transform.position.y + 8, 0);
        GameObject dancer = (GameObject)Instantiate(dancerPreFabs[Random.Range(0, 2)], spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.Straight;
        dancer.GetComponent<Dancer>().player = player.transform;
    }

    void SpawnAngleRight()
    {
        Vector3 spawnLocation = new Vector3(player.transform.position.x + 5, player.transform.position.y + 8, 0);
        GameObject dancer = (GameObject)Instantiate(dancerPreFabs[Random.Range(0, 2)], spawnLocation, Quaternion.identity);
        dancer.name = "Dancer";
        dancer.GetComponent<Dancer>().dancerType = DancerType.AngleFromRight;
        dancer.GetComponent<Dancer>().player = player.transform;
    }
}
