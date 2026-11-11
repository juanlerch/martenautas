using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LabsRAScanear :  MonoBehaviour, ITrackableEventHandler
{

	public GameObject panelInfo;
	public GameObject enfocar;

	private TrackableBehaviour mTrackableBehaviour;
	public Transform myModelPrefab;
	// Use this for initialization
	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		Debug.Log("Cambio de estado " + previousStatus + "->" + newStatus);

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else {
			panelInfo.SetActive (false);
			enfocar.SetActive (true);
		}
	} 
	private void OnTrackingFound()
	{
		panelInfo.SetActive (true);
		enfocar.SetActive (false);
	}
}