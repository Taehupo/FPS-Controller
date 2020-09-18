using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	Rigidbody rb;

	[SerializeField]
	Transform raycastOrigin;

	[SerializeField]
	float speed;

	[SerializeField]
	float sprintSpeed;

	[SerializeField]
	float strafeSpeed;

	[SerializeField]
	float speedLimit;

	[SerializeField]
	float jumpForce;

	[SerializeField]
	Weapon currentWeapon;

	[SerializeField]
	GameObject defaultWeapon;

	[SerializeField]
	float maxHealth;

	float currentHealth;

	[HideInInspector]
	public bool isAlive = true;

	bool isMoving;
	bool isStrafing;
	bool isFiring;
	bool isSprinting = false;

	[SerializeField]
	TextMeshPro AmmoText;

	Vector2 moveForce;
	Vector2 strafeForce;

	// Start is called before the first frame update
	void Start()
	{
		InitializePlayer();
	}

	// Update is called once per frame
	void Update()
	{
		if (isAlive)
		{			
			if (isFiring && !currentWeapon.isReloading)
			{
				if (rb != null)
				{
					currentWeapon.Fire(rb);
				}
				else
				{
					currentWeapon.Fire(null);
				}
			}
		}
		if (GameManager.instance.drawDebug)
		{
			DrawDebugs();
		}
		UpdatePlayerUI();
	}

	void FixedUpdate()
	{
		UpdateMovement();
	}

	void InitializePlayer()
	{
		rb = GetComponent<Rigidbody>();
		moveForce = new Vector2();
		strafeForce = new Vector2();
		GameObject defaultWeap = Instantiate(defaultWeapon, transform.GetChild(0).transform.GetChild(0).position, Quaternion.Euler(90.0f, 0.0f, 0.0f), transform.GetChild(0).transform.GetChild(0).transform);
		defaultWeap.transform.localPosition = defaultWeap.GetComponent<Weapon>().offset;
		currentWeapon = defaultWeap.GetComponent<Weapon>();
		currentHealth = maxHealth;
		AmmoText = currentWeapon.GetComponentInChildren<TextMeshPro>();
	}

	void DrawDebugs()
	{
		Debug.DrawRay(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
	}

	void UpdateMovement()
	{
		if (isAlive)
		{
			if ((isMoving || isStrafing) && rb.velocity.sqrMagnitude <= speedLimit)
			{
				Vector3 movingForce;
				if (isSprinting)
				{
					movingForce = Vector3.Normalize((transform.forward * -moveForce.y) + (transform.right * strafeForce.x)) * sprintSpeed;
				}
				else
				{
					movingForce = Vector3.Normalize((transform.forward * -moveForce.y) + (transform.right * strafeForce.x)) * speed;
				}
				
				rb.AddForce(movingForce);
			}
			if (!isMoving && !isStrafing)
			{
				Vector3 tempVelo = rb.velocity;
				tempVelo.x = 0;
				tempVelo.z = 0;
				rb.velocity = tempVelo;
			}
		}		
	}

	void UpdatePlayerUI()
	{
		if (!currentWeapon.isReloading)
		{
			AmmoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.GetMaxAmmo();
		}
		else
		{
			AmmoText.text = "Reloading : " + (currentWeapon.reloadTime - currentWeapon.reloadTimer).ToString("N1") + " s";
		}		
	}

	public void RecieveDamage(float damageAmount)
	{
		currentHealth -= damageAmount;
		if (currentHealth <= 0)
		{
			isAlive = false;
		}
	}

	//Input management starts here

	public void Move(InputAction.CallbackContext context)
	{
		if(context.phase == InputActionPhase.Started)
		{
			isMoving = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			isMoving = false;
		}
		moveForce = context.ReadValue<Vector2>();
	}

	public void Strafe(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			isStrafing = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			isStrafing = false;
		}
		strafeForce = context.ReadValue<Vector2>();
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			rb.AddForce(transform.up * jumpForce);
		}
	}

	public void Fire(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			isFiring = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			isFiring = false;
		}
	}

	public void SpecialClassAction(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			isSprinting = true;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			isSprinting = false;
		}
	}

	public void Reload(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			currentWeapon.StartReload();
		}
	}
}
