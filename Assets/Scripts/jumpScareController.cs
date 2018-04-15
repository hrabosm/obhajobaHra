using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScareController : MonoBehaviour {

	public GameObject jumpScareCam;
	public playerController playerController;
	public AudioSource jumpScareSound;
	public GameObject youLostScreen;

	void Start () 
	{
		Debug.Log("Player is dead");
		GameObject.FindGameObjectWithTag("enemyRobot").SetActive(false);
		playerController.enabled = false;
		jumpScareCam.SetActive(true);
		jumpScareSound.enabled = true;
		StartCoroutine(wait());
	}
	IEnumerator wait()
	{
		yield return new WaitForSeconds(2);
		youLostScreen.SetActive(true);
	}
}
