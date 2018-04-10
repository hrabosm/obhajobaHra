using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScareController : MonoBehaviour {

	void Start () 
	{
		Debug.Log("Player is dead");
		Time.timeScale = 0.0f;
	}
}
