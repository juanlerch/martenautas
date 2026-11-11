using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsInfoXNivel : MonoBehaviour {

	public GameObject panelArchivos;

	public GameObject toText;
	public GameObject toTitle;

	public LabsInfoXNivelData[] data;

	public void setNivel(int nivel){

		if(toText!=null) toText.GetComponent <Text>().text = data [nivel-1].text;
		if(toTitle!=null) toTitle.GetComponent<Text>().text = data [nivel-1].title;

		if (panelArchivos != null) {
			panelArchivos.GetComponent<LabsLeido> ().nombre = data [nivel - 1].nombre;
		}

		foreach (LabsInfoXNivelData d in data) {
			d.showPanel.SetActive (false); //desactivar el panel
			if(d.showPanels!=null) //si hay mas de un panel para desactivar 
				foreach(GameObject g in d.showPanels){
					g.SetActive (false);
				}
		}

		if (data[nivel - 1].showPanel != null) {
			data [nivel - 1].showPanel.SetActive (true); 
			if(data [nivel - 1].showPanels!=null)
				foreach(GameObject g in data [nivel - 1].showPanels){
				g.SetActive (true);
			}

		}
	}

	void Start () {
		

	}

}
