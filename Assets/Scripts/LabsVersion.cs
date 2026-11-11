using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class LabsVersion : MonoBehaviour {
	string urlBase = Principal.urlBase;
	Version v;

	static bool mostrado=false;

	public GameObject panelNuevaVersion;
	public GameObject botonNuevaVersion;

	 string version = "1.0.11";
	 int    id_version = 11;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text> ().text = "v"+version;
		getVersion ();
	}
	
	public void getVersion(){
		StartCoroutine (obtenerVersion());

	}

	IEnumerator obtenerVersion(){

		string url = urlBase + "/version";
		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("Content-Type", "application/json");


		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;

				v = JsonUtility.FromJson<Version>(jsonResult);

				if (v.idVersion > id_version) {
					if (botonNuevaVersion != null)
						botonNuevaVersion.SetActive(true);
					if (panelNuevaVersion && !mostrado) {
						mostrado = true;
						panelNuevaVersion.SetActive (true); 
					}
				}

			}
		}
	}
}
