using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthZoom : MonoBehaviour {

	[SerializeField]private Animation _eathZoomAnim = null ;
	private AudioSource _zoomAudio = null ;


	// Use this for initialization
	void Start () {

		_zoomAudio = GetComponent<AudioSource>( );

		return;
	}
	
	// Update is called once per frame
	void Update () {
		
		if( null != _eathZoomAnim ){
			if( false == _eathZoomAnim.isPlaying ) {
//				UnityEngine.SceneManagement.SceneManager.LoadScene("GameMainShoese");
				UnityEngine.SceneManagement.SceneManager.LoadScene("GameMainShoese");
			}
			else if( null != _zoomAudio ){
				bool isPlay ; 

				isPlay = false;
				foreach( AnimationState animState in _eathZoomAnim ){
					if( 0.5f < _eathZoomAnim[animState.name].normalizedTime ){
						isPlay = true;
					}
				}
				if( true == isPlay ){
					_zoomAudio.Play( );
					_zoomAudio = null;
				}
			}
		}

	}
}
