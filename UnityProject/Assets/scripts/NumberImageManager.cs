using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberImageManager : MonoBehaviour {

	[SerializeField]private List<UnityEngine.UI.Image> _numberObjectList = new List<UnityEngine.UI.Image>( ) ;
	[SerializeField]private List<Sprite> _numberSpriteList = new List<Sprite>( ) ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateNumber(int num)
	{
		int nextNum, currentDigitNum ;
		int imageNo ;

		nextNum = num;
		currentDigitNum = _numberObjectList.Count;
		while( 0 < currentDigitNum ){
			imageNo = nextNum / ((int)Mathf.Pow(10, currentDigitNum) / 10);
			_numberObjectList[currentDigitNum - 1].sprite = _numberSpriteList[imageNo];
			nextNum %= ((int)Mathf.Pow(10, currentDigitNum) / 10);
			currentDigitNum --;
		}

		return;
	}

}
