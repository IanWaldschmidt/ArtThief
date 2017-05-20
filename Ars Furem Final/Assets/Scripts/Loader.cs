using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameManager;

	public void resetLevel(){
		this.Start ();
	}

	// Use this for initialization
	void Start () {
		if (GameManager.instance == null)
			Instantiate(gameManager);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
