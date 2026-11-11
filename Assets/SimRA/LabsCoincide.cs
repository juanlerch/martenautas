using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LabsCoincide : MonoBehaviour, ITrackableEventHandler
{
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
    // Update is called once per frame
    void Update()
    {
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
            if (SistemaSolarRA.selected == myModelPrefab.gameObject)
            {
                SistemaSolarRA.selected = null;
               // SistemaSolarRA.nada();
            }
        }
    }
    private void OnTrackingFound()
    {
        Debug.Log("Encontrado"); 
        SistemaSolarRA.selected = myModelPrefab.gameObject;
    }
}
