using UnityEngine;
using System.Collections;

public enum DancerType
{
    AngleFromLeft,
    Straight,
    AngleFromRight
}

public class Dancer : MonoBehaviour
{
    public float moveSpeed = 5;
    public DancerType dancerType;
    public Transform player;
    private Vector2 spawnLocation;
    private float previousX;

	// Use this for initialization
	void Start ()
    {
        previousX = transform.position.x;
        spawnLocation = transform.position;

        Animator anim = GetComponent<Animator>();

        anim.GetComponent<Animation>()["Dance"].time = Random.Range(0, 20);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dancerType == DancerType.Straight)
        {
            spawnLocation.x = player.position.x;
        }
        else
        {
            spawnLocation.x += (transform.position.x - previousX);
            previousX = spawnLocation.x;
        }

        if (player.gameObject.GetComponent<PlayerController>().isDancing())
        {
            switch (dancerType)
            {
                case DancerType.AngleFromLeft:
                    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed / 2 * Time.deltaTime);
                    break;
                case DancerType.Straight:
                    transform.position = Vector2.MoveTowards(transform.position, spawnLocation, moveSpeed * Time.deltaTime);
                    break;
                case DancerType.AngleFromRight:
                    transform.position = Vector2.MoveTowards(transform.position, spawnLocation, moveSpeed * Time.deltaTime);
                    break;
            }

            if (!gameObject.renderer.isVisible)
                Destroy(gameObject);
        }
        else
        {
            switch (dancerType)
            {
                case DancerType.AngleFromLeft:
                    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                    break;
                case DancerType.Straight:
                    transform.position = new Vector2(player.transform.position.x - 1, transform.position.y - (moveSpeed / 10) * Time.deltaTime);
                    break;
                case DancerType.AngleFromRight:
                    //transform.position = new Vector2(player.transform.position.x - 1, transform.position.y - (moveSpeed / 10) * Time.deltaTime);
                    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                    break;
            }
        }
	}
}
