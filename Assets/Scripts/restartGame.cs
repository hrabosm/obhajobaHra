using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour {

	void Update() 
	{
		if(Input.GetButton("R"))
		{
			Debug.Log("R was pressed");
			SceneManager.LoadScene(0);
		}
	}
}
