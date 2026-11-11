using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsQualitySelector : MonoBehaviour {

	Slider slider;
	// Use this for initialization
	void Start () {
		int qualityLevel = QualitySettings.GetQualityLevel();
		slider = GetComponent<Slider> ();

		if (slider)
			slider.value = qualityLevel;
	}

	void OnDisable(){
		if (slider) {
			int qualityLevel = QualitySettings.GetQualityLevel ();
			if (qualityLevel != slider.value) {
				QualitySettings.SetQualityLevel ((int) slider.value);
			} 
		}
	}
}
