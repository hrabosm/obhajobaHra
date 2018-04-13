using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDied : MonoBehaviour {
	GameObject gameController;
	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("Player died!");
			gameController.GetComponent<jumpScareController>().enabled = true;
		}
	}
}
