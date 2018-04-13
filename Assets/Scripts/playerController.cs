using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private Vector3 moveDir;
	private float xMax = 0.7f;
	private float xMin = -0.7f;
	public GameObject player;
	public Camera playerCam;
	public float speed = 1.36f;
	public float sensitivity = 1f;
	CharacterController controller;

	void Start ()
	{
		controller = GetComponent<CharacterController>();
		
	}
	void Update () 
	{
		if(Cursor.lockState != CursorLockMode.Locked || Cursor.visible)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		moveDir = new Vector3(Input.GetAxis("Horizontal"),0 ,Input.GetAxis("Vertical"));
		moveDir = transform.TransformDirection(moveDir);
		player.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0));
		if(playerCam.transform.localRotation.x > xMin && playerCam.transform.localRotation.x < xMax) //Camera movement lock
		{
			playerCam.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * sensitivity * -1, 0, 0));
		}
		else if(playerCam.transform.localRotation.x > xMin && Input.GetAxis("Mouse Y")*-1 < 0)
		{
			playerCam.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * sensitivity * -1, 0, 0));
		}
		else if(playerCam.transform.localRotation.x < xMax && Input.GetAxis("Mouse Y")*-1 > 0)
		{
			playerCam.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * sensitivity * -1, 0, 0));
		}
		else
		{
			playerCam.transform.Rotate(Vector3.zero);
		}
		if(Input.GetKey(KeyCode.LeftShift)) //Sprint
		{
			controller.Move(moveDir  * Time.deltaTime * speed * 2f);
		}
		else
		{
			controller.Move(moveDir  * Time.deltaTime * speed);
		}
	}
}
