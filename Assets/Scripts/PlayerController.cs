using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	Rigidbody rb;

	[SerializeField]
	Transform raycastOrigin;

	[SerializeField]
	float speed;

	[SerializeField]
	float strafeSpeed;

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
			if (isFiring)
			{
				currentWeapon.Fire();
			}
		}
		if (GameManager.instance.drawDebug)
		{
			DrawDebugs();
		}
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
	}

	void DrawDebugs()
	{
		Debug.DrawRay(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
	}

	void UpdateMovement()
	{
		if (isAlive)
		{
			if ((isMoving || isStrafing) && rb.velocity.sqrMagnitude < 10.0f)
			{
				rb.AddForce(transform.forward * -moveForce.y * speed);
				rb.AddForce(transform.right * strafeForce.x * strafeSpeed);
			}
			if (!isMoving || !isStrafing)
			{
				Vector3 tempVelo = rb.velocity;
				tempVelo.x = 0;
				tempVelo.z = 0;
				rb.velocity = tempVelo;
			}
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

	public void Crouch(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{

		}
	}
}
