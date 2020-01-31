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
	float firingRate;

	[SerializeField]
	Weapon currentWeapon;

	[SerializeField]
	GameObject defaultWeapon;

	float firingRateTimer = 9999999.0f;

	bool isMoving;
	bool isStrafing;
	bool isFiring;
	bool hasFired = false;

	Vector2 moveForce;
	Vector2 strafeForce;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		moveForce = new Vector2();
		strafeForce = new Vector2();
		GameObject defaultWeap = Instantiate(defaultWeapon, transform.GetChild(0).transform.GetChild(0).position, Quaternion.Euler(90.0f,0.0f,0.0f), transform.GetChild(0).transform.GetChild(0).transform);
		defaultWeap.transform.localPosition = defaultWeap.GetComponent<Weapon>().offset;
		currentWeapon = defaultWeap.GetComponent<Weapon>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isFiring)
		{
			currentWeapon.Fire();
		}

		Debug.DrawRay(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
	}

	void FixedUpdate()
	{
		if ((isMoving || isStrafing) && rb.velocity.sqrMagnitude < 10.0f)
		{
			rb.AddForce(transform.forward * -moveForce.y * speed);
			rb.AddForce(transform.right * strafeForce.x * strafeSpeed);
		}
		if (!isMoving || isStrafing)
		{
			Vector3 tempVelo = rb.velocity;
			tempVelo.x = 0;
			tempVelo.z = 0;
			rb.velocity = tempVelo;
		}
	}

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
