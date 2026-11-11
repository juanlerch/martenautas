using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsInfoXNivelData : MonoBehaviour {

	public string nombre;

	public string title;
	[TextArea(30, 10)][SerializeField] public string text;


	public GameObject showPanel;

	public GameObject[] showPanels;

}
