using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

	[SerializeField]
	int maxAmmo;

	[HideInInspector]
	public int currentAmmo;

	[SerializeField]
	public float reloadTime;

	bool hasFired;
	[HideInInspector]
	public bool isReloading;

	float firingRateTimer = 999999999f;

	[HideInInspector]
	public float reloadTimer = 0.0f;

	[SerializeField]
	Animator weaponAnimator;

	// Start is called before the first frame update
	void Start()
	{
		currentAmmo = maxAmmo;
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
		if (isReloading)
		{
			if (reloadTimer < reloadTime)
			{
				reloadTimer += Time.deltaTime;
			}
			else
			{
				FinishReload();
			}
		}
	}

	public void Fire(Rigidbody firingEntity)
	{
		if (firingRateTimer >= fireRate && currentAmmo > 0)
		{
			weaponAnimator.SetBool("Firing", true);
			GameObject proj = Instantiate(projectile, defaultFireOrigin.position, Quaternion.identity);
			Vector3 localForward = transform.InverseTransformDirection(defaultFireOrigin.forward);
			proj.transform.forward = transform.TransformDirection(ComputeInnacuracy(localForward));
			if (firingEntity != null)
			{
				proj.GetComponent<Rigidbody>().velocity = firingEntity.velocity;
			}
			firingRateTimer = 0.0f;
			hasFired = true;
			currentAmmo--;
		}
		if (currentAmmo <= 0)
		{
			StartReload();
		}
	}

	void StopFiring()
	{
		weaponAnimator.SetBool("Firing", false);
	}

	public void StartReload()
	{
		isReloading = true;
		StopFiring();
		weaponAnimator.SetBool("Reloading", true);
		//To Do : Have reload weaponAnimatorations.
	}

	void FinishReload()
	{
		weaponAnimator.SetBool("Reloading", false);
		currentAmmo = maxAmmo;
		reloadTimer = 0.0f;
		isReloading = false;		
	}

	public int GetMaxAmmo()
	{
		return maxAmmo;
	}

	Vector3 ComputeInnacuracy(Vector3 localForward)
	{
		Vector3 result;
		switch (weapAccracy)
		{
			case Accuracy.Spp:
				result = localForward;
				break;
			case Accuracy.Sp:
				result = localForward;
				result.x += Random.Range(0.0f,0.02f);
				result.y += Random.Range(0.0f, 0.02f);
				result.z += Random.Range(0.0f, 0.02f);
				break;
			case Accuracy.S:
				result = localForward;
				result.x += Random.Range(0.0f, 0.05f);
				result.y += Random.Range(0.0f, 0.05f);
				result.z += Random.Range(0.0f, 0.05f);
				break;
			case Accuracy.A:
				result = localForward;
				result.x += Random.Range(0.0f, 0.1f);
				result.y += Random.Range(0.0f, 0.1f);
				result.z += Random.Range(0.0f, 0.1f);
				break;
			case Accuracy.B:
				result = localForward;
				result.x += Random.Range(0.0f, 0.15f);
				result.y += Random.Range(0.0f, 0.15f);
				result.z += Random.Range(0.0f, 0.15f);
				break;
			case Accuracy.C:
				result = localForward;
				result.x += Random.Range(0.0f, 0.2f);
				result.y += Random.Range(0.0f, 0.2f);
				result.z += Random.Range(0.0f, 0.2f);
				break;
			case Accuracy.D:
				result = localForward;
				result.x += Random.Range(0.0f, 0.3f);
				result.y += Random.Range(0.0f, 0.3f);
				result.z += Random.Range(0.0f, 0.3f);
				break;
			case Accuracy.E:
				result = localForward;
				result.x += Random.Range(0.0f, 0.4f);
				result.y += Random.Range(0.0f, 0.4f);
				result.z += Random.Range(0.0f, 0.4f);
				break;
			default:
				result = localForward;
				break;
		}
		return result;
	}
}
