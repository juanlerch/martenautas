using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsUrl : MonoBehaviour {

	public void openUrl(){
		Application.OpenURL(gameObject.GetComponent<Text>().text);
	}
}
