using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

	private NavMeshAgent agent;
	private int lenght;
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		lenght = globalVars.pickablesList.Length;
		travelTo();
	}
	private void travelTo()
	{
		Debug.Log(agent.pathStatus);
		Debug.Log("Generating new destination.");
		agent.destination = globalVars.pickablesList[Random.Range(0,lenght)].transform.position;
		Debug.Log("Getting to my destination.");
		StartCoroutine(Wait());
	}
	IEnumerator Wait()
	{
		Debug.Log("Starting to wait!");
		while(agent.gameObject.transform.position.x != agent.destination.x && agent.gameObject.transform.position.y != agent.destination.y)
		{

		}
		Debug.Log("I am here!");
		yield return new WaitForSeconds(10);
		travelTo();
	}
}
