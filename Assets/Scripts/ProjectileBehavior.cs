using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
	[SerializeField]
	float maxDistance;

	[SerializeField]
	float finalVelocity;

	[SerializeField]
	int damage;

	[SerializeField]
	bool isExplosive;

	[SerializeField]
	float explosionRange;

	Rigidbody projRb;

	[HideInInspector]
	public float effectiveVelocity;

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
		if ((effectiveVelocity * transform.forward).magnitude < finalVelocity)
		{
			projRb.velocity =  transform.forward * effectiveVelocity;
		}

		if (Vector3.Distance(origin,transform.position) >= maxDistance)
		{
			Destroy(this.gameObject);
		}
	}
}
