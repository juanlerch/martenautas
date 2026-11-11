using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class grupos : MonoBehaviour {
	public InputField gGrupo;
	public GameObject gMensajeCrearGrupo;
	public GameObject gErrorCrearGrupo;

	public InputField gUsuario;
	public GameObject gMensajeAgregarUsr;

	public GameObject gSelectGrupo;
	public GameObject detalleRanking;

	int i=1;
	RankingGeneral rg;
	List<GameObject> posiciones = new List<GameObject> ();

	string urlBase = Principal.urlBase;
	Grupos gruposUsuario;

	public void obtenerGrupos(){
		StartCoroutine(GetGrupos ());

	}

	IEnumerator GetGrupos(){
		string url = urlBase + "/grupos?token=" + Principal.avance.avanceJugadorActual ().token;
		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("Content-Type", "application/json");

		Dropdown selectGrupos;

		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;

				gruposUsuario = JsonUtility.FromJson<Grupos>(jsonResult);
				selectGrupos = gSelectGrupo.GetComponent<Dropdown> ();
				selectGrupos.ClearOptions ();
				selectGrupos.AddOptions (gruposUsuario.listado);
				/*
				selectGrupos.onValueChanged.AddListener(delegate {
					DropdownValueChanged(selectGrupos);
				});
				*/
				StartCoroutine (obtenerRankingGrupo(gruposUsuario.ids[selectGrupos.value]));
				detalleRanking.SetActive (false);

			}
		}
	}
	/*
	void DropdownValueChanged(Dropdown change)
	{
		Debug.Log("Valor : " + change.value);
		Debug.Log("ID : "    + gruposUsuario.ids[change.value]);

		string idGrupo = gruposUsuario.ids [change.value];


		StartCoroutine (obtenerRankingGrupo(idGrupo));
		detalleRanking.SetActive (false);
	}
	*/
	public void elegirGrupo(Dropdown change){
		string idGrupo = gruposUsuario.ids [change.value];

		StartCoroutine (obtenerRankingGrupo(idGrupo));
		detalleRanking.SetActive (false);
	}


	IEnumerator obtenerRankingGrupo(string idGrupo){
		//limpiar las posiciones Anteriores
		foreach (GameObject anterior in posiciones) {
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		posiciones.Clear ();
		i = 1;

		AvanceJugador avance = Principal.avance.avanceJugadorActual();


		string url = urlBase + "/ranking-grupo/" + idGrupo + "?token=" + avance.token;

		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("Content-Type", "application/json");

		Debug.Log (url);

		yield return www.Send();
		/*
		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {*/
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;

				Debug.Log (jsonResult);

				string[] insignias = {"Cuestionarios","Laboratorios","Ecuaciones","Operaciones con numeros enteros","Numeros Primos"};

				rg = JsonUtility.FromJson<RankingGeneral>(jsonResult);
				foreach (Ranking r in rg.ranking) {
					GameObject d = Instantiate(detalleRanking);
					posiciones.Add (d);

					d.transform.SetParent (detalleRanking.transform.parent);
					d.SetActive(true);
					d.transform.Find ("Posición").GetComponent<Text>().text = ""+i;

					LabsAvatar avatar = FindObjectsOfType<LabsAvatar> ()[0];

					d.transform.Find ("Grupo Player/Avatar").GetComponent<Image>().sprite = avatar.avatars[r.avatar];
					d.transform.Find ("Grupo Player/Player").GetComponent<Text> ().text = r.nombreUsuario;
					d.transform.Find ("Grupo Puntos/Puntos").GetComponent<Text> ().text = r.puntaje;
					d.transform.Find ("Grupo Puntos/RANGO").GetComponent<Text> ().text = r.nivel.ToUpper();

					Color tmp = d.transform.Find ("Grupo Puntos/IcoRango"+(r.nivel).Substring(0,1).ToUpper()).GetComponent<Image>().color;
					tmp.a = 1.0f;
					d.transform.Find ("Grupo Puntos/IcoRango"+(r.nivel).Substring(0,1).ToUpper()).GetComponent<Image>().color = tmp;

					if(r.logros.Count > 0){
						foreach (AvanceLogro l in r.logros) {
							if (System.Array.IndexOf (insignias, l.nombre) >= 0) {
								tmp = d.transform.Find ("Grupo Insignias/TitInsignias/insignias/" + l.nombre).GetComponent<Image> ().color;
								tmp.a = 1.0f;
								d.transform.Find ("Grupo Insignias/TitInsignias/insignias/" + l.nombre).GetComponent<Image> ().color = tmp;
							}
						}

					}


					i++;
				}



			}
//		}
	}


	public void crearGrupo(){
		if (gGrupo.text.Length > 0) {
			StartCoroutine(postCrearGrupo ());
		}
	}

	IEnumerator postCrearGrupo(){
		string jsonGrupo = "{\"token\":\""+  Principal.avance.avanceJugadorActual ().token + "\", \"grupo\":\"" + gGrupo.text + "\"}";

			string url = 	urlBase + "/crear-grupo";
			
			var www = new UnityWebRequest(url, "POST");
			byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonGrupo);
			www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");

			yield return www.Send();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);

			} else {
				if (www.isDone) {
					string jsonResult = www.downloadHandler.text;
					Debug.Log (jsonResult);

					RetornoCrearGrupo ret = JsonUtility.FromJson<RetornoCrearGrupo> (jsonResult);
					
					if (ret.estado == 1) {
						gMensajeCrearGrupo.SetActive (true);
						obtenerGrupos ();
					}
					else{
						gErrorCrearGrupo.GetComponent<Text> ().text = ret.mensaje;
					}
				}
		}
	}

	public void agregarUsuario(){
		Dropdown selectGrupos = gSelectGrupo.GetComponent<Dropdown> ();

		if (gUsuario.text.Length > 0) {
			StartCoroutine(postGrupoAgregarMiembro (gruposUsuario.ids[selectGrupos.value]));
		}

		Debug.Log ("USR:" + gUsuario.text);
	}

	public void limpiarMensajeAgregar(){
		gMensajeAgregarUsr.GetComponent<Text>().text = "";
	}

	IEnumerator postGrupoAgregarMiembro(string idGrupo){
		string jsonGrupo = "{\"token\":\""+  Principal.avance.avanceJugadorActual ().token + "\", \"idGrupo\":\"" + idGrupo + "\", \"usuario\":\""+ gUsuario.text +"\"}";

		string url = 	urlBase + "/grupo-agregar-usuario";

		var www = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonGrupo);
		www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		www.SetRequestHeader("Content-Type", "application/json");

		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);

		} else {
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;
				RetornoAgregarMiembro ret = JsonUtility.FromJson<RetornoAgregarMiembro> (jsonResult);

				gMensajeCrearGrupo.SetActive (true);
				if (ret.estado == 1) {
					gUsuario.text = "";
					gMensajeAgregarUsr.GetComponent<Text> ().text = "¡USUARIO AGREGADO!";
				} else {
					gMensajeAgregarUsr.GetComponent<Text> ().text = ret.mensaje;
				}
			}
		}
	}





}
