using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LabsOpenFile : MonoBehaviour {

	public string fileName;
	public Text debug;

	public void openFile(){
//		string url = Application.streamingAssetsPath + "/" + fileName;

		//Application.OpenURL (url); 
		OpenPDF(fileName);	 
	}

	public void OpenPDF(string filename)
	{
		//string filename = "pdffile";
		//TextAsset pdfTemp = Resources.Load (filename, typeof(TextAsset)) as TextAsset;
		//System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + filename , pdfTemp.bytes);
		//string url = Application.persistentDataPath + "/" + filename;
		Application.OpenURL(filename );
		Debug.Log (filename);
		if (debug)
			debug.text = filename;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
