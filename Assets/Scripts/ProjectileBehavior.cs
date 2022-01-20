using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
	[SerializeField]
	float maxRange;

	[SerializeField]
	float finalVelocity;

	[SerializeField]
	int damage;

	[SerializeField]
	bool isPenetrating;

	[SerializeField]
	bool isExplosive;

	[SerializeField]
	bool isGravityAffected;

	[SerializeField]
	bool isSelfDamaging = false;

	[SerializeField]
	float explosionRange;

	Rigidbody projRb;

	float projectileLifetimeTimer = 0.0f;

	[SerializeField]
	AnimationCurve effectiveVelocityCurve;

	GameObject shooter;

	Vector3 origin;

	// Start is called before the first frame update
	void Start()
	{
		projRb = GetComponent<Rigidbody>();
		origin = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		projectileLifetimeTimer += Time.deltaTime;
		if (!isGravityAffected)
		{
			if ((projRb.velocity).magnitude <= finalVelocity)
			{
				projRb.AddForce(transform.forward * effectiveVelocityCurve.Evaluate(projectileLifetimeTimer));
			}
		}		

		if (Vector3.Distance(origin,transform.position) >= maxRange)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger == false)
		{
			if (other.gameObject != shooter && !isSelfDamaging)
			{
				EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
				if (enemy != null)
				{
					enemy.RecieveDamage(damage);
					if (!isPenetrating)
					{
						Destroy(this.gameObject);
					}
				}

				PlayerController player = other.GetComponent<PlayerController>();
				if (player != null)
				{
					player.RecieveDamage(damage);
					if (!isPenetrating)
					{
						Destroy(this.gameObject);
					}
				}
			}
			else if (isSelfDamaging)
			{
				EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
				if (enemy != null)
				{
					enemy.RecieveDamage(damage);
					if (!isPenetrating)
					{
						Destroy(this.gameObject);
					}
				}

				PlayerController player = other.GetComponent<PlayerController>();
				if (player != null)
				{
					player.RecieveDamage(damage);
					if (!isPenetrating)
					{
						Destroy(this.gameObject);
					}
				}
			}
		}						
	}

	public void SetShooter(GameObject _shooter)
	{
		shooter = _shooter;
	}
}
