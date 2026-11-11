using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour {
	public GameObject nave;
	Vector3 posicion;
	// Use this for initialization
	void Start () {
		posicion = new Vector3 (0, 10f, -7f);
		Camera.main.orthographicSize  = 10f;

	}

	// Update is called once per frame
	void Update () {
		if (nave.GetComponent<Transform> ().position.y > 10) {
			posicion.y = 10f;
		} else {
			if (posicion.y <= 1f) {
			
			} else {
		//		posicion.y = nave.GetComponent<Transform> ().position.y;
				posicion.y = nave.GetComponent<Transform> ().position.y;
				Camera.main.orthographicSize  = posicion.y;
			}


		}

		transform.position = posicion;
	}
}
