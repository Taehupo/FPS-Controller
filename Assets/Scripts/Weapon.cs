using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Accuracy
{
	Spp = 0,
	Sp,
	S,
	A,
	B,
	C,
	D,
	E
}

public class Weapon : MonoBehaviour
{
	[SerializeField]
	public Vector3 offset;

	[SerializeField]
	GameObject projectile;

	[SerializeField]
	public float fireRate;

	[SerializeField]
	Accuracy weapAccracy;

	[SerializeField]
	Transform defaultFireOrigin;

	bool hasFired;

	float firingRateTimer = 999999999f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (hasFired)
		{
			firingRateTimer += Time.deltaTime;
		}
		if (firingRateTimer >= fireRate)
		{
			hasFired = false;
		}		
	}

	public void Fire()
	{
		if (firingRateTimer >= fireRate)
		{
			GameObject proj = Instantiate(projectile, defaultFireOrigin.position, Quaternion.identity);
			proj.transform.forward = defaultFireOrigin.forward;
			firingRateTimer = 0.0f;
			hasFired = true;
		}
	}
}
