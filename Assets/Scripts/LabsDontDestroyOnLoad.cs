using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabsDontDestroyOnLoad : MonoBehaviour {
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    // Update is called once per frame
    void Start () {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Start: " + this.gameObject);
        }

    }
}
