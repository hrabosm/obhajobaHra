using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {

	private NavMeshAgent agent;
	private int lenght;
	private Animator robotAnim;
	RaycastHit hit;
	public int maxRange = 20;
	private float tempSpeed;
	private float runningSpeed = 1.75f;
	public AudioSource walking;
	public AudioSource running;
	public Color color;
	public Light robotFlashLight;
	private Color tempColor;
	void Start () 
	{
		robotAnim = transform.GetChild(0).GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		lenght = globalVars.pickablesList.Length;
		tempSpeed = agent.speed;
		walking.enabled = true;
		running.enabled = true;
		tempColor = robotFlashLight.color;
		travelTo();
	}
	private void travelTo()
	{
		Debug.Log(agent.pathStatus);
		Debug.Log("Generating new destination.");
		agent.destination = globalVars.pickablesList[Random.Range(0,lenght)].transform.position;
		robotFlashLight.color = tempColor;
		robotFlashLight.spotAngle = 50f;
		robotFlashLight.intensity = 0.5f;
		robotAnim.SetFloat("Speed", 1f);
		walking.mute = false;
		running.mute = true;
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
		robotAnim.SetFloat("Speed", 0f);
		walking.mute = true;
		running.mute = true;
		yield return new WaitForSeconds(5);
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
					agent.speed = runningSpeed;
					if(robotAnim.GetFloat("Speed") < 2f)
					{
						robotAnim.SetFloat("Speed",2f);
						walking.mute = true;
						running.mute = false;
						robotFlashLight.color = color;
						robotFlashLight.spotAngle = 40f;
						robotFlashLight.intensity = 0.75f;
					}
					Debug.Log(robotAnim.GetFloat("Speed"));
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
