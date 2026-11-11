using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class ranking : MonoBehaviour {
	string urlBase = Principal.urlBase;
	RankingGeneral rg;
	int i = 1;
	int paginaActual = 1;
	int totalPaginas;
	List<GameObject> posiciones = new List<GameObject> ();

	public GameObject detalleRanking;
	public GameObject textPagina;
	public GameObject miPosicion;


	IEnumerator subirLogrosYObenerRankin(){
		LabsLogros logros = FindObjectOfType<LabsLogros> ();
		logros.subirLogrosAInternet ();
		yield return new WaitForSeconds(1);
		StartCoroutine (obtenerRanking ());
	}

	public void getRanking(){
		textPagina.GetComponent<Text> ().text = "";
		StartCoroutine (subirLogrosYObenerRankin());
		detalleRanking.SetActive (false);

	}
	
	IEnumerator obtenerRanking(){
		//limpiar las posiciones Anteriores
		foreach (GameObject anterior in posiciones) {
			anterior.SetActive (false);
			Object.Destroy (anterior);
		}
		posiciones.Clear ();

		miPosicion.GetComponent<Text> ().text = "";
		string url = urlBase + "/ranking-general/"+Principal.avance.avanceJugadorActual().nombre + "/"+paginaActual;
		UnityWebRequest www = UnityWebRequest.Get(url);
		www.SetRequestHeader("Content-Type", "application/json");


		yield return www.Send();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {
			if (www.isDone) {
				string jsonResult = www.downloadHandler.text;


				string[] insignias = {"Cuestionarios","Laboratorios","Ecuaciones","Operaciones con numeros enteros","Numeros Primos"};

				rg = JsonUtility.FromJson<RankingGeneral>(jsonResult);
				i = rg.posicionInicial;
				textPagina.GetComponent<Text> ().text = paginaActual + " de " + rg.cantidadPaginas;
				miPosicion.GetComponent<Text> ().text = ""+rg.posicionUsuario;

				totalPaginas = rg.cantidadPaginas;

				foreach (Ranking r in rg.ranking) {
					GameObject d = Instantiate(detalleRanking);
					posiciones.Add (d);

					d.transform.SetParent (detalleRanking.transform.parent);
					d.SetActive(true);
					//d.transform.Find ("Posición").GetComponent<Text>().text = ""+i;
					d.transform.Find ("Posicion").GetComponent<Text>().text = ""+r.posicion;

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



						//Debug.Log (r.logros[0].nombre);

					}


					i++;
				}



			}
		}
	}

	public void subirPagina(){
		if (paginaActual < totalPaginas) {
			paginaActual++;
			StartCoroutine (obtenerRanking ());
		}

	}

	public void bajarPagina(){
		if (paginaActual > 1) {
			paginaActual--;
			StartCoroutine (obtenerRanking ());
		}
	}
}
