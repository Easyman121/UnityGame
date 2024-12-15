using UnityEngine;
using System.Collections;
public class FeedFish : MonoBehaviour
{
    public Animator food;
	public bool feed;
	public Transform Player;
    private AISpawner Spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawner = transform.parent.GetComponentInParent<AISpawner>();
        feed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
	{
		if (Player)
		{
			float dist = Vector3.Distance(Player.position, transform.position);
			if (dist < 15f)
			{
				if (feed == false)
				{
					if (Input.GetMouseButtonDown(0))
					{
						StartCoroutine(opening());
					}
				}
			}
		}	
	}

    IEnumerator opening()
	{
		print("you are feeding the fish");
		food.Play("FeedBox", -1, 0f);
		//open = true;
        Spawner.randomWaypoint = false;
		yield return new WaitForSeconds(3f);
        Spawner.randomWaypoint = true;
	}	
}
