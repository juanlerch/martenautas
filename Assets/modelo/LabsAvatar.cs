using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsAvatar : MonoBehaviour {

	int id=-1; //-1 sin avatar 

	public Sprite[] avatars;

	public GameObject[] toGameObjects;
	public GameObject fromGameObjects;

	void actualizar(){

		foreach(GameObject o in toGameObjects){ 
			if (id>=0) o.GetComponent<Image> ().sprite = avatars[id];
		}

		int i = 1;
		 
		foreach(Transform f in fromGameObjects.transform){ 
			if (id == i) {
				f.Find ("check").gameObject.GetComponent<Image> ().enabled = true;

		//		Debug.Log ("actualizar ON : " + id +" : " + i + ":" + f.name);

			} else {
				Image img = f.Find ("check").gameObject.GetComponent<Image> ();
				img.enabled = false;
		//		Debug.Log ("actualizar OFF : " + id +" : " + i + ":" + f.name);
			}
			i++;
		}

	}

	// Use this for initialization
	void Start () {
		Principal.inicializar ();
		id= Principal.avance.avanceJugadorActual ().avatarID;
		actualizar ();
	}
	
	public void setAvatarFrom(int pid){ 
		id  = pid;
		Principal.avance.avanceJugadorActual ().avatarID =pid;
		actualizar ();
	}
	 

}
