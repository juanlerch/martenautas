using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsRotateSkybox : MonoBehaviour {

	public float factor=1.0f;
	// Use this for initialization

	GameObject nave ;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (nave==null) nave = GameObject.Find ("Nave");
		GameObject naveClon = GameObject.Find ("Nave(Clone)");

		if (nave != null && naveClon != null) {
			float distancia = -1* factor * (nave.GetComponent<Transform> ().position.x - naveClon.GetComponent<Transform> ().position.x);
			Debug.Log ("nave:" + nave + " clone:" + naveClon + " distance:" + distancia);

			RenderSettings.skybox.SetFloat ("_Rotation", distancia); //To set the speed, just multiply the Time.time with whatever amount you want.
		} else {
			Debug.Log ("nave:" + nave + " clone:" + naveClon);
		}
	}
}
