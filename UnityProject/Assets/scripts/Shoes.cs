using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : MonoBehaviour {

	private ParticleSystem[] _particleList ;
	private Vector3 prevPosition = new Vector3( ) ;
	private Vector3 _startPosition ;
	[SerializeField]private ShoesController _shoesController ;

	// Use this for initialization
	void Start () {
		
		_particleList = GetComponentsInChildren<ParticleSystem>( );
		_startPosition = transform.position;

		if( null != _shoesController ){
			_shoesController.OnReceive += this.OnReceiveHandler;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if( 0.3 < Vector3.Distance(prevPosition, transform.position) ){
			foreach( ParticleSystem tmp in _particleList ){
				tmp.Play( );
			}
		}
		else{
			foreach( ParticleSystem tmp in _particleList ){
				tmp.Stop( );
			}
		}

		prevPosition = transform.position;

		Assets.scripts.GlobalWork.resultDistance = (int)Vector3.Distance(_startPosition, transform.position) * 100;
		Assets.scripts.GlobalWork.resultLaps = Assets.scripts.GlobalWork.resultDistance / 10000;

	}

    public void OnReceiveHandler(ShoesInfo info)
	{
		Rigidbody rigidBpdy ;

		rigidBpdy = GetComponent<Rigidbody>( );
		rigidBpdy.AddForce(new Vector3((float)10.0f, (float)info.Y / 10.0f, (float)info.Z) * Mathf.Min((float)info.Speed, 20.0f));

		AudioSource audio ;

		audio = GetComponent<AudioSource>( );
		audio.Play( );
	}


}
