using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
	[SerializeField]
	float healthPointMax;

	[SerializeField]
	[Range(1,10)]
	float wanderDistance;

	float currentHealthPoints;

	bool isAlive = true;
	bool isAggroed = false;

	NavMeshAgent agent;

	float wanderingTimer = 9999999.0f;

	GameObject player;

	void Awake()
	{
		currentHealthPoints = healthPointMax;
	}

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (isAlive)
		{
			if (isAggroed)
			{
				agent.SetDestination(player.transform.position);
			}
			else
			{
				Wander();
			}
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public void RecieveDamage(float damageAmount)
	{
		currentHealthPoints -= damageAmount;
		if (currentHealthPoints <= 0)
		{
			isAlive = false;
		}
		if (!isAggroed)
		{
			isAggroed = true;
		}
	}

	public void Wander()
	{
		wanderingTimer += Time.deltaTime;
		if (wanderingTimer >= 2.5f)
		{
			Vector3 randomDirection = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1.0f, 5.0f), Random.Range(-5.0f, 5.0f));


			Vector3 wanderPoint = transform.position + ((randomDirection.normalized) * wanderDistance);

			Debug.Log("WanderPoint = " + wanderPoint);

			NavMeshHit hit;
			if (NavMesh.SamplePosition(wanderPoint, out hit, 20.0f, NavMesh.AllAreas))
			{
				Debug.Log("Hit position : " + hit.position);
				agent.SetDestination(hit.position);

				wanderingTimer = 0.0f;
			}
		}
	}
}
