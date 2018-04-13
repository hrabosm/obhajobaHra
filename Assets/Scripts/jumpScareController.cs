using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScareController : MonoBehaviour {

	public GameObject jumpScareCam;
	public playerController playerController;
	public AudioSource jumpScareSound;

	void Start () 
	{
		Debug.Log("Player is dead");
		GameObject.FindGameObjectWithTag("enemyRobot").SetActive(false);
		playerController.enabled = false;
		jumpScareCam.SetActive(true);
		jumpScareSound.enabled = true;
	}
}
