﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

	private NavMeshAgent agent;
	private int lenght;
	RaycastHit hit;
	public int maxRange = 20;
	private float tempSpeed;
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		lenght = globalVars.pickablesList.Length;
		tempSpeed = agent.speed;
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
			yield return new WaitForSeconds(1);
		}
		Debug.Log("I am here!");
		agent.speed = tempSpeed;
		yield return new WaitForSeconds(10);
		travelTo();
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("I see player, i think...");
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Physics.Raycast(transform.position, (other.gameObject.transform.position - transform.position), out hit, maxRange))
			{
				if(hit.transform.tag == "Player")
				{
					agent.speed *= 2f;
					agent.destination = hit.transform.position;
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("I can no longer see player :(");
		}
	}
}
