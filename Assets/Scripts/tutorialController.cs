using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour {

	public GameObject tutorialMovement;
	public GameObject tutorialOther;
	private bool moved = false;
	void Update () 
	{
		if((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0) && !moved)
		{
			moved = true;
			StartCoroutine(Moved());
		}
	}
	IEnumerator Moved()
	{
		Debug.Log("Moved");
		yield return new WaitForSeconds(5);
		Destroy(tutorialMovement);
		Destroy(tutorialOther);
		gameObject.GetComponent<tutorialController>().enabled = false;
	}
}
