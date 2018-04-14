using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	private GameObject highlightedObj;
	private Material tempMat;
	public Material highlightedMat;
	public Camera playerCam;
	public Text counter;
	public int examCounter;
	public GameObject winScreen;
	void Start()
	{
		counter.text = "0";
	}
	private void PickUp()
	{
		if(highlightedObj != null && highlightedObj.tag == "usable" && highlightedObj.name.Contains("PickablePLaceHolder"))
		{
			highlightedObj.SetActive(false);
			highlightedObj = null;
			examCounter++;
			counter.text = examCounter.ToString() +"/7";
		}
		else if(highlightedObj != null && highlightedObj.tag == "usable" && highlightedObj.name.Contains("DoorPlaceHolder") && examCounter == 7)
		{
			winScreen.SetActive(true);
			Time.timeScale = 0.0f;
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
