using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsAudioPrincipal : MonoBehaviour {
	public bool on = true;
	// Use this for initialization
	void Start () {
		GameObject audioPrincipal = GameObject.Find ("AudioPrincipal");
		GameObject audioTipeo = GameObject.Find ("AudioTipeo");

		if (audioTipeo != null) {
			GameObject.DontDestroyOnLoad (audioTipeo );
			if (audioTipeo.GetComponent<AudioSource> ().isPlaying)
				audioTipeo.GetComponent<AudioSource> ().Stop ();
		}


		if (on) {
			if (audioPrincipal != null) {
				GameObject.DontDestroyOnLoad (audioPrincipal);
				AudioSource aus = audioPrincipal.GetComponent<AudioSource> ();
				if (!aus.isPlaying)
					aus.Play ();
			}
		}
		else{
			if (audioPrincipal != null) {
				AudioSource aus = audioPrincipal.GetComponent<AudioSource> ();
				if(aus.isPlaying) aus.Stop();
			}

		}

	}

}
