using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour {
	public GameObject nave;
	Vector3 posicion;

	void Start () {
		posicion = new Vector3 (0, 10f, -7f);
		Camera.main.orthographicSize  = 15f;
		GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
	}

	void LateUpdate () 
	{
		/*if (nave.transform.position.y < 20) {
			transform.position = new Vector3(nave.transform.position.x, nave.transform.position.y, -7.0f);
			Camera.main.orthographicSize  = nave.transform.position.y+3.0f;
		}
		else{
			transform.position = new Vector3(nave.transform.position.x, 20, -7.0f);
			if (nave.transform.position.y < 12) {
				Camera.main.orthographicSize  = nave.transform.position.y + 3;

			}
		}*/
		transform.position = new Vector3(nave.transform.position.x, nave.transform.position.y, -7.0f);
		Camera.main.orthographicSize  = nave.transform.position.y+3.0f;
	//	transform.position = new Vector3(nave.transform.position.x, 20, -7.0f);
	}



}
