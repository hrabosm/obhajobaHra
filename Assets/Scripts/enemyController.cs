using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

	//public Transform target;

	// Use this for initialization
	void Start () 
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		//agent.destination = target.position;	
		agent.destination = new Vector3(1f,0.05f,3f);	
	}
}
