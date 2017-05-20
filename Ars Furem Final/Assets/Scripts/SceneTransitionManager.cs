using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {
	//[SerializeField] public Scene nextScene;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//If currently on the start screen, start the main game if any button is pressed
		if (SceneManager.GetActiveScene() == (SceneManager.GetSceneByName ("Start Screen"))) {
			if (Input.anyKeyDown) {
				if (!SceneManager.GetSceneByName ("Main").isLoaded) {
					SceneManager.LoadScene ("Main");
				}
				SceneManager.SetActiveScene (SceneManager.GetSceneByName ("Main"));
				GameManager.instance = null;
				SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Start Screen"));
			}
		}



		//if currently on the the end scene, load start screen if any button is pressed
		if (SceneManager.GetActiveScene() == (SceneManager.GetSceneByName ("End Screen"))) {
			if (Input.anyKeyDown) {
				if (!SceneManager.GetSceneByName ("Start Screen").isLoaded) {
					SceneManager.LoadScene ("Start Screen");
				}
				SceneManager.SetActiveScene (SceneManager.GetSceneByName ("Start Screen"));
				SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("End Screen"));
			}
		}
	}
	/*
	public void GoToNextScene(){
		if (!this.nextScene.isLoaded) {
			SceneManager.LoadScene (this.nextScene.name);
		}

		SceneManager.SetActiveScene (this.nextScene.name);
		SceneManager.UnloadSceneAsync (
	}
	*/
}
