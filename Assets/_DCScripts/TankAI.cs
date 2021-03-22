using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour {

	Animator anim;
	public GameObject player;

	public GameObject GetPlayer()
	{
		return player;
	}

	void Fire()
	{
		print("firing at player");
	}

	public void StopFiring()
	{
		CancelInvoke("Fire");
	}

	public void StartFiring()
	{
		InvokeRepeating("Fire",0.5f,0.5f);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetFloat("distance", Vector3.Distance(transform.position,player.transform.position));	
	}
}
