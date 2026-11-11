using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsQuality : MonoBehaviour {

	int qualityLevelPrevius; 
	// Use this for initialization
	void Start () {

		qualityLevelPrevius = QualitySettings.GetQualityLevel();
		QualitySettings.SetQualityLevel(5);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
