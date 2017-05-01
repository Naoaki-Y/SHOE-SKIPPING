using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingHeight : MonoBehaviour {

	[SerializeField]private RectTransform _sliderObject = null ;
	[SerializeField]private float _sliderObjectYMax = 128.0f ;
	[SerializeField]private NumberImageManager _numberImageManager = null ;

	[SerializeField]private int _height_max = 10000 ;
	[SerializeField]private int _height_min = 100 ;

	[SerializeField]private RectTransform _scaleObject = null ;

	private int _currentHeight = 100 ;

	// Use this for initialization
	void Start () {
		_currentHeight = _height_min;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if( null != _sliderObject ){
			Vector3 newPos ;
			float y ;

			newPos = _sliderObject.position;
			newPos.y += Mathf.Floor(Input.GetAxisRaw("Vertical") * 1);
			newPos.y = Mathf.Max(Mathf.Min(newPos.y, _sliderObjectYMax), 0.0f);
			y = newPos.y;
			y = y * 2;
			_currentHeight = Mathf.FloorToInt(((_height_max - _height_min) / _sliderObjectYMax) * y) + _height_min;
			_currentHeight = System.Math.Min(System.Math.Max(_currentHeight, _height_min), _height_max);
			newPos.y = Mathf.Max(Mathf.Min(newPos.y, _sliderObjectYMax - 56.0f), 0.0f - 56.0f);
			_sliderObject.position = newPos;
		}
		_numberImageManager.UpdateNumber(_currentHeight);
		if( null != _scaleObject ){
			_scaleObject.localScale = Vector3.one * Mathf.Max(_currentHeight * 0.0001f, 0.1f);
		}

		if( true == Input.GetButtonDown("Fire1") ){
			AudioSource audio ;

			audio = GetComponent<AudioSource>( );
			audio.Play( );
			Assets.scripts.GlobalWork.playerHeight = _currentHeight;
			UnityEngine.SceneManagement.SceneManager.LoadScene("test02");
		}

	}
}
