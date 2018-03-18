using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	private GameObject highlightedObj;
	private Material tempMat;
	public Material highlightedMat;
	public Camera playerCam;
	public int examCounter;
	private void PickUp()
	{
		if(highlightedObj != null && highlightedObj.tag == "usable" && highlightedObj.name.Contains("PickablePLaceHolder"))
		{
			Destroy(highlightedObj);
			highlightedObj = null;
			examCounter++;
		}
	}
	void FixedUpdate ()
	{
		ray = new Ray(playerCam.transform.position + new Vector3(0, 0.25f ,0), playerCam.transform.forward);
		if(Physics.SphereCast(ray, 0.2f, out hit, 5f))
		{
			if(hit.transform.gameObject.tag == "usable")
			{
				if(Input.GetButton("E")) PickUp();
				if(highlightedObj != hit.transform.gameObject)
				{
					highlightedObj = hit.transform.gameObject;
					tempMat = highlightedObj.GetComponent<Renderer>().material;
					highlightedObj.GetComponent<Renderer>().material = highlightedMat;
				}
				else
				{
					highlightedObj.GetComponent<Renderer>().material = highlightedMat;
				}
			}
			else
			{
				if(highlightedObj != null)
				{
					highlightedObj.GetComponent<Renderer>().material = tempMat;
					highlightedObj = null;
				}
			}
		}
	}
}
