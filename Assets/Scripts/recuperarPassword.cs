using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class recuperarPassword : MonoBehaviour {

	string urlBase = Principal.urlBase;

	public InputField gEmail;
	public GameObject gPopUp;
	public GameObject gMensaje;

	public void recuperar(){
		StartCoroutine (metodoRecuperar());
	}



	IEnumerator metodoRecuperar(){

		RecuperarPass datosAcceso = new RecuperarPass ();
		datosAcceso.email  = gEmail.text;

		string jsonAcceso = JsonUtility.ToJson (datosAcceso);

		string url = 	urlBase + "/recuperar-password";

		var www = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonAcceso);
		www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		www.SetRequestHeader("Content-Type", "application/json");

		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			//Debug.Log (www.error);
			gMensaje.GetComponent<Text>().text = "Se produjo un error. Intentá nuevamente.";
			gPopUp.SetActive(true);
		} else {
			if (www.isDone) {

				string jsonResult = www.downloadHandler.text;

				RetornoRecuperarPass ret = JsonUtility.FromJson<RetornoRecuperarPass> (jsonResult);

				//Debug.Log ("msg:" + ret.mensaje);
				gMensaje.GetComponent<Text>().text = ret.mensaje;
				gPopUp.SetActive(true);
		
			}
		}
	}

}
