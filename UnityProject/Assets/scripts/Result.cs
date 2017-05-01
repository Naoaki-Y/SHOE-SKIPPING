using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {

	[SerializeField]private NumberImageManager _kmNumberManager = null ;
	[SerializeField]private NumberImageManager _lapsNumberManager = null ;

	[SerializeField]private float _kmCountupTimeSeconds = 0.1f ;
	[SerializeField]private float _lapsCountupTimeSeconds = 0.5f ;
	[SerializeField]private float _countupSoundDelayTime = 1.0f ;

	private int _currentKm = 0 ;
	private int _resultKm = 0 ;
	private int _currentLaps = 0 ;
	private int _resultLaps = 0 ;

	private float _lastCountupKmTime = 0.0f ;
	private float _lastCountupLapsTime = 0.0f ;
	private float _lastCountupSoundPlayTime = 0.0f ;

	private AudioSource _countupSe = null ;

	// Use this for initialization
	void Start () {

		_resultKm = Assets.scripts.GlobalWork.resultDistance;
		_resultLaps = Assets.scripts.GlobalWork.resultLaps;

		_lastCountupKmTime = _lastCountupLapsTime = Time.time;
		_lastCountupSoundPlayTime = _lastCountupKmTime;
		
		_countupSe = GetComponent<AudioSource>( );

	}
	
	// Update is called once per frame
	void Update () {
		
		CountupNumber( );

		if( true == Input.GetButtonDown("Fire1") ){
			UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
		}

	}

	private void CountupNumber( )
	{
		float currentTime, diffTime ;

		currentTime = Time.time;

		if( _currentKm < _resultKm ){
			diffTime = currentTime - _lastCountupKmTime;
			if( _kmCountupTimeSeconds <= diffTime ) {
				_currentKm += Mathf.RoundToInt(diffTime / _kmCountupTimeSeconds);
				_currentKm = System.Math.Min(_currentKm, _resultKm);
				_lastCountupKmTime = currentTime;
				if( _countupSoundDelayTime < (currentTime - _lastCountupSoundPlayTime) ){
					_countupSe.Play( );
					_lastCountupSoundPlayTime = currentTime;
				}
			}
		}

		if( _currentLaps < _resultLaps ){
			diffTime = currentTime - _lastCountupLapsTime;
			if( _lapsCountupTimeSeconds <= diffTime ) {
				_currentLaps += Mathf.RoundToInt(diffTime / _lapsCountupTimeSeconds);
				_currentLaps = System.Math.Min(_currentLaps, _resultLaps);
				_lastCountupLapsTime = currentTime;
			}
		}

		if( null != _kmNumberManager ){
			_kmNumberManager.UpdateNumber(_currentKm);
		}
		if( null != _lapsNumberManager ){
			_lapsNumberManager.UpdateNumber(_currentLaps);
		}

		return;
	}




}
