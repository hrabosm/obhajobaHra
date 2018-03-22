using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject[] pickablesList;
	private System.Random random;
	void Start () 
	{
		pickablesList = globalVars.pickablesList;
		agent = GetComponent<NavMeshAgent>();
		travelTo();
	}
	private void travelTo()
	{
		Debug.Log("Generating new destination.");
		agent.destination = pickablesList[0].transform.position;
		Debug.Log("Getting to my destination.");
		if(agent.pathStatus == NavMeshPathStatus.PathComplete)
		{
			Debug.Log("Complete");
		}
		Debug.Log("Arrived at my destination!");
		travelTo();
	}
}
