using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;


public class registro : MonoBehaviour {
	string urlBase = Principal.urlBase;
					  
	public InputField gUsuario;
	public InputField gPassword;
	public InputField gEmail;

	public GameObject errorUsuario;
	public GameObject errorMail;
	public GameObject errorGenerico;

	public bool aditiva=false;
	bool mostrarPassword = false;

	//public GameObject popupError;

	public void registrarse(){
		StartCoroutine (metodoRegistrarse());
	}

	IEnumerator metodoRegistrarse(){

		Registro datosAcceso = new Registro ();
		datosAcceso.usuario  = gUsuario.text;
		datosAcceso.password = gPassword.text;
		datosAcceso.email    = gEmail.text;

		string jsonAcceso = JsonUtility.ToJson (datosAcceso);

		string url = 	urlBase + "/crear-usuario";

		var www = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonAcceso);
		www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		www.SetRequestHeader("Content-Type", "application/json");

		yield return www.Send();

		//Debug.Log("Status Code: " + www.responseCode);

		if (www.isNetworkError || www.isHttpError) {
			//Debug.Log (www.error);
			//Debug.Log ("Code");
			//Debug.Log (www.responseCode);

		} else {
			if (www.isDone) {

				string jsonResult = www.downloadHandler.text;
				//Debug.Log(jsonResult);
				//RetornoLogin ret = new RetornoLogin();

				RetornoRegistro ret = JsonUtility.FromJson<RetornoRegistro> (jsonResult);

				if (ret.estado == 1) {

					if (!aditiva) {
						Principal p = new Principal ();

						p.cambiarUsuarioPorNombre (ret.usuario);

						// Guardar el token
						Principal.avance.avanceJugadorActual ().token = ret.token;
					} else {
						Principal.avance.renombrar (Principal.user, ret.usuario);
						Principal.avance.avanceJugadorActual ().token = ret.token;
						Principal p = FindObjectsOfType<Principal> () [0];
						LabsLogros logros = FindObjectsOfType<LabsLogros> () [0];
						logros.subirLogrosAInternet ();
						ranking rank = FindObjectOfType<ranking> ();
						if(rank) rank.getRanking ();
						p.cerrarScenaAditiva ("RegistroAditivo");
					}

				} else {
					Debug.Log ("MSG: " + ret.mensaje);
					if (ret.mensaje == "usuario") {
						errorUsuario.SetActive (true);
						Debug.Log ("Usuario");

					}	
					else if (ret.mensaje == "mail") {
						errorMail.SetActive(true);

					}	
					else{
						errorGenerico.SetActive (true);

					}
			
				}


			}
		}
	}

	public void mostrarOcultarPassword(){
		mostrarPassword = !mostrarPassword;
		if (mostrarPassword) {
			gPassword.contentType = InputField.ContentType.Standard;
		} else {
			gPassword.contentType = InputField.ContentType.Password;
		}
		gPassword.ActivateInputField ();


	}
}
