using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour {

	public GameObject tutorialMovement;
	public GameObject tutorialOther;
	void Update () 
	{
		if(Input.GetAxis("Horizontal") > 1 || Input.GetAxis("Vertical") > 1 || Input.GetAxis("Horizontal") < 1 || Input.GetAxis("Vertical") < 1)
		{
			StartCoroutine(Moved());
		}
	}
	IEnumerator Moved()
	{
		yield return new WaitForSeconds(5);
		Destroy(tutorialMovement);
		Destroy(tutorialOther);
		gameObject.GetComponent<tutorialController>().enabled = false;
	}
}
