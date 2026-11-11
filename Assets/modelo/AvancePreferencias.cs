using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvancePreferencias {
    List<string> claves;
    List<string> valores;

    public void agregar(string clave) {
        if (clave == null) claves = new List<string>();
    }

    public string obtener (string clave) {
        if (clave == null) claves = new List<string>();
        if (claves.Contains(clave)) {
            int index = claves.IndexOf(clave);
            return valores[index];
        }
        else {
            return null;
        }
    }

}
