using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoaderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")){
			GameObject.Find("Panel").GetComponent<Animator>().SetTrigger("FadeOut");
		}
	}
	public void LoadTitleScene(){
		SceneManager.LoadScene ("ParameterScene");
	}
	public void LoadParameterScene(){
		SceneManager.LoadScene ("ParameterScene");
	}
	public void LoadGameScene(){
		SceneManager.LoadScene ("ParameterScene");
	}
}
