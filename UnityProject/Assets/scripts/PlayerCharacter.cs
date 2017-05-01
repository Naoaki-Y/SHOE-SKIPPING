using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField]private float HEIGHT = 1.7f ;
	[SerializeField]private GameObject _playerStart = null ;
	[SerializeField]private GameObject _shoseObject = null ;
	[SerializeField]private GameObject _speedManager = null ;

	private Camera _mainCamera ;

	int speed = 0 ;


	// Use this for initialization
	void Start () {
		
		if( null != _playerStart ){
			transform.position = _playerStart.transform.position;
			transform.rotation = _playerStart.transform.rotation;
		}

		_mainCamera = transform.GetComponentInChildren<Camera>( );

		if( null != _shoseObject ){
			_shoseObject.transform.localScale = _shoseObject.transform.localScale * Mathf.Max((Assets.scripts.GlobalWork.playerHeight * 0.0001f), 0.1f);
		}

	}
	
	// Update is called once per frame
	void Update () {

        Ray ray;

        ray = new Ray(transform.position, Vector3.down);
        if( false == Physics.Raycast(ray, Mathf.Max(Assets.scripts.GlobalWork.playerHeight * 0.01f, HEIGHT)) ){
            transform.position += Vector3.down * 1.0f;
        }

		if( null != _shoseObject ){
		if( true == Input.GetKeyDown(KeyCode.A) ){
			Rigidbody rigidBpdy ;

			rigidBpdy = _shoseObject.GetComponent<Rigidbody>( );
			rigidBpdy.velocity = Vector3.zero;
//			_shoseObject.transform.rotation = transform.rotation;
			_shoseObject.transform.position = transform.position + Vector3.down + Vector3.forward;
		}
		if( true == Input.GetKeyDown(KeyCode.Z) ){
			Rigidbody rigidBpdy ;

			rigidBpdy = _shoseObject.GetComponent<Rigidbody>( );
			rigidBpdy.AddForce((Vector3.forward * 10 + Vector3.up * 2) * 20);
		}
		}

		if( true == Input.GetKeyDown(KeyCode.S) ){
			SwitchShoseView(_mainCamera.gameObject.transform.parent == transform);
		}

		if( true == Input.GetKeyDown(KeyCode.UpArrow) ){
			speed ++;
			_speedManager.SendMessage("UpdateNumber", speed);
		}
		if( true == Input.GetKeyDown(KeyCode.DownArrow) ){
			speed --;
			_speedManager.SendMessage("UpdateNumber", speed);
		}

		if( true == Input.GetKeyDown(KeyCode.Space) ){
			UnityEngine.SceneManagement.SceneManager.LoadScene("result");
		}

	}

	private void SwitchShoseView(bool isShoseView)
	{
		if( null == _mainCamera ){
			return;
		}
		if( true == isShoseView ){
			_mainCamera.gameObject.transform.parent = _shoseObject.transform;
		}
		else{
			_mainCamera.gameObject.transform.parent = transform;
		}
	}

}
