using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsCopy : MonoBehaviour {
	
	public string nombre;

	public GameObject panel;
	public GameObject toText;
	public GameObject toTitle;

	public Color color=Color.gray;



	public string title;
	[TextArea(30, 10)][SerializeField] public string text;

	public void doCopy (){
	

		toText.GetComponent<Text> ().text = text.ToString();
		toTitle.GetComponent<Text>().text = title;
		toTitle.GetComponent<Text> ().color = color;
		panel.SetActive(true);
		if (nombre != null)
			Principal.avance.avanceJugadorActual ().agregarLeido(nombre);
	}

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
