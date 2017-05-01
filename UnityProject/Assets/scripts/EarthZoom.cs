using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthZoom : MonoBehaviour {

	[SerializeField]private Animation _eathZoomAnim = null ;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if( null != _eathZoomAnim ){
			if( false == _eathZoomAnim.isPlaying ) {
//				UnityEngine.SceneManagement.SceneManager.LoadScene("GameMainShoese");
				UnityEngine.SceneManagement.SceneManager.LoadScene("GameMainShoese");
			}
		}

	}
}
