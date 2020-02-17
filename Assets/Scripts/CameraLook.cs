using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraLook : MonoBehaviour
{
	[SerializeField]
	float sensitivity;

	[SerializeField]
	float smoothness;

	[SerializeField]
	GameObject player;

	PlayerController playerComponent;

	Vector2 mouseLook;

	Vector2 smoothVector;

	// Start is called before the first frame update
	void Start()
	{
		player = this.transform.parent.gameObject;
		playerComponent = player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (playerComponent.isAlive)
		{
			Vector2Control mouseDeltaC = Pointer.current.delta;
			Vector2 mouseDelta = new Vector2(mouseDeltaC.x.ReadValue(), mouseDeltaC.y.ReadValue());
			mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothness, sensitivity * smoothness));

			smoothVector.x = Mathf.Lerp(smoothVector.x, mouseDelta.x, 1.0f / smoothness);
			smoothVector.y = Mathf.Lerp(smoothVector.y, mouseDelta.y, 1.0f / smoothness);

			mouseLook += smoothVector;

			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
		}		
	}
}
