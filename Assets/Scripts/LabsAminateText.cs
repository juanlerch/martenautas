    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsAminateText : MonoBehaviour {

	GameObject audioTipeo;

	string strComplete;
	void Start(){
		audioTipeo = GameObject.Find ("AudioTipeo");
		strComplete = gameObject.GetComponent<Text> ().text; 
		//Debug.Log ("1:"+strComplete);
		gameObject.GetComponent<Text> ().text=""; 
		StartCoroutine( AnimateText() );
	}

    public void animar() {
        Debug.Log ("animar()" + gameObject + " " + gameObject.activeSelf);

            Debug.Log("animando()");
            gameObject.SetActive(true);
            strComplete = gameObject.GetComponent<Text>().text;
            gameObject.GetComponent<Text>().text = "";
            StartCoroutine(AnimateText());
       
    }

	IEnumerator AnimateText(){
		int i = 0;
		string str = "";
		//Debug.Log ("2:"+strComplete);
		if (audioTipeo != null) audioTipeo.GetComponent<AudioSource> ().Play ();
		while( i < strComplete.Length ){
			if (audioTipeo != null && !audioTipeo.GetComponent<AudioSource>().isPlaying ) 
				audioTipeo.GetComponent<AudioSource> ().Play ();
			str += strComplete[i++];
			gameObject.GetComponent<Text> ().text = str;
			yield return new WaitForSeconds(0.05F);

		}
		if (audioTipeo != null) {
			Debug.Log ("stopping:" + strComplete);
			audioTipeo.GetComponent<AudioSource> ().Stop ();

		}
	}

    void OnDisable()
    {
        gameObject.GetComponent<Text>().text = strComplete;
		if (audioTipeo != null) {
			Debug.Log ("stopping:" + strComplete);
			audioTipeo.GetComponent<AudioSource> ().Stop ();

		}
    }


}
