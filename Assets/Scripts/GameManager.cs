using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	static public GameManager instance;

	public bool drawDebug = false;

	// Start is called before the first frame update
	void Start()
	{
		if (instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}

		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
