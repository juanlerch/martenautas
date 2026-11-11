    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabsAminateText : MonoBehaviour {



	string strComplete;
	void Start(){

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
		while( i < strComplete.Length ){
			//Debug.Log ("3:"+str );
			str += strComplete[i++];
			gameObject.GetComponent<Text> ().text = str;
			yield return new WaitForSeconds(0.05F);
		}
	}

    void OnDisable()
    {
        gameObject.GetComponent<Text>().text = strComplete;
    }


}
