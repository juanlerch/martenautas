using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;


public class login : MonoBehaviour {
	string urlBase = Principal.urlBase;
					  
	public InputField gUsuario;
	public InputField gPassword;
	public GameObject gMensajeError;
	public GameObject gMensajeUsuarioNotNull;
	public void ingresar(){
		StartCoroutine (metodoIngresar());
	}

	public void metodoIngresarSimple(GameObject text){
	
		Principal p = new Principal ();
		p.cambiarUsuario(text,gMensajeUsuarioNotNull);
	}

	IEnumerator metodoIngresar(){

		Login datosAcceso = new Login ();
		datosAcceso.usuario  = gUsuario.text;
		datosAcceso.password = gPassword.text;

		string jsonAcceso = JsonUtility.ToJson (datosAcceso);

		string url = 	urlBase + "/login";

		var www = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonAcceso);
		www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		www.SetRequestHeader("Content-Type", "application/json");

		Principal p = new Principal();


		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
			AvanceJugador aj = Principal.avance.avanceJugadorActualPorNombre (datosAcceso.usuario);
				
			if (Principal.avance.avanceJugadorActual ().clave == datosAcceso.password) {
				p.cambiarUsuarioPorNombre (datosAcceso.usuario);
			}
			else{	
				gMensajeError.SetActive (true);
			}
		} else {
			if (www.isDone) {

				string jsonResult = www.downloadHandler.text;

				RetornoLogin ret = JsonUtility.FromJson<RetornoLogin> (jsonResult);

				if (ret.estado == 1) {

					if (ret.avance.puntajeTotal > Principal.avance.avanceJugadorActual ().puntajeTotal) {
						Principal.avance.sobrescribir (ret.avance);
						Principal.avance.avanceJugadorActual ().idActualizacion = ret.idActualizacion;
					
						p.guardarAvance();
					}

					Principal.avance.avanceJugadorActual ().token = ret.token;
					Principal.avance.avanceJugadorActual ().clave = datosAcceso.password;
					p.cambiarUsuarioPorNombre (datosAcceso.usuario);
				} else {
					Principal.Log ("msg:" + ret.mensaje);
					gMensajeError.SetActive (true);

				}
			}
		}
	}
/*
	IEnumerator obtenerActualizacion(string token){
		string url = urlBase + "/avance?token=" + token;

		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("Content-Type", "application/json");

		yield return www.Send();

		Debug.Log ("pasa");

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;
				Debug.Log ("Avance:");
				Debug.Log (jsonResult);

			}
		}
	}
*/
}
