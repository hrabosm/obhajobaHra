using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private Vector3 moveDir;
	private Vector3 rotation;
	public GameObject player;
	public Camera playerCam;
	public float speed = 1.36f;
	public float sensitivity = 2f;
	CharacterController controller;

	void Start ()
	{
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}
	void Update () 
	{
		moveDir = new Vector3(Input.GetAxis("Horizontal"),0 ,Input.GetAxis("Vertical"));
		moveDir = transform.TransformDirection(moveDir);
		rotation = new Vector3(0, Input.GetAxis("Mouse X") * -1, 0);
		player.transform.Rotate(0,rotation.y * -1,0);
		playerCam.transform.eulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), 0, 0);
		if(Input.GetKey(KeyCode.LeftShift))
		{
			controller.Move(moveDir  * Time.deltaTime * speed * 2f);
		}
		else
		{
			controller.Move(moveDir  * Time.deltaTime * speed);
		}
	}
}
