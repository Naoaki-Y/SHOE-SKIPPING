using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public Slider HeightSlider;

	// Use this for initialization
	void Start () {
		ChangeLocalScale(HeightSlider.value);
		ChangeText(HeightSlider.value);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton ("Vertical")){
			HeightSlider.value += 100 * Input.GetAxis ("Vertical");
		}
	}
	public void ChangeLocalScale(float HumanHeight){
		GameObject.Find("HumanImage").transform.localScale = new Vector3(HeightSlider.normalizedValue, HeightSlider.normalizedValue, 1);
	}
	public void ChangeText(float HumanHeight){
		GameObject.Find("Number").GetComponent<NumberImageManager>().UpdateNumber(Mathf.FloorToInt(HumanHeight));
	}
}
