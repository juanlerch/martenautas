using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class molecula : MonoBehaviour {
	float fx;
	float fy;
	float fz;
	float temperatura;
	float magnitudFuerza;
	float cambioLiquidoGaseoso;
	float cambioSolidoLiquido;

	float delay;

	void Start () {
		delay = Random.value;
		cambioLiquidoGaseoso = transform.Find ("temperaturas").GetComponent<temperaturas> ().puntoEbullicion;
		cambioSolidoLiquido  = transform.Find ("temperaturas").GetComponent<temperaturas> ().puntoFusion;
		magnitudFuerza = 0f;

	}
	
	void Update () {
		temperatura = GameObject.Find ("Scripts").GetComponent<materia> ().getTemperatura ();
		//------------------------------------------------------
		// Si tiene que estar en estado sólido
		//------------------------------------------------------
		if (temperatura <= cambioSolidoLiquido) {
			magnitudFuerza = 4;

			fx = Mathf.Pow (-1f, Random.Range (1, 3)) * Random.Range(magnitudFuerza, magnitudFuerza + 0.5f);
			fy = -5f;
			fz = Mathf.Pow(-1f, Random.Range(1,3)) * Random.Range(magnitudFuerza, magnitudFuerza+ 0.5f);
		
		}
		//------------------------------------------------------
		// Si tiene que estar en estado líquido
		//------------------------------------------------------
		if (temperatura > cambioSolidoLiquido && temperatura < cambioLiquidoGaseoso) {
			// Elijo 3 para que la que la magnitud máxima sea 3
			magnitudFuerza = temperatura * 3 / cambioLiquidoGaseoso +1;

			fx = Mathf.Pow (-1f, Random.Range (1, 3)) * Random.Range(magnitudFuerza, magnitudFuerza + 0.5f);

			if (gameObject.GetComponent<Transform> ().position.y > -2f) {
				fy = -3f * magnitudFuerza;

			} else {
				fy = Mathf.Pow(-1f, Random.Range(1,3)) * Random.Range(magnitudFuerza, magnitudFuerza+ 0.5f);

			}

			fz = Mathf.Pow(-1f, Random.Range(1,3)) * Random.Range(magnitudFuerza, magnitudFuerza+ 0.5f);

		}
		//------------------------------------------------------
		// Si tiene que estar en estado gaseoso
		//------------------------------------------------------
		if (temperatura >= cambioLiquidoGaseoso) {
			magnitudFuerza = 3;//temperatura * 5 / cambioLiquidoGaseoso;
			if (magnitudFuerza > 10) {
				magnitudFuerza = 10;
			}

			fx = Mathf.Pow (-1f, Random.Range (1, 3)) * Random.Range(magnitudFuerza, magnitudFuerza + 0.5f);
			fy = Mathf.Pow(-1f, Random.Range(1,3)) * Random.Range(magnitudFuerza, magnitudFuerza+ 0.5f);
			fz = Mathf.Pow(-1f, Random.Range(1,3)) * Random.Range(magnitudFuerza, magnitudFuerza+ 0.5f);

		}

		if (delay <= 0) {
			GetComponent<Rigidbody> ().AddForce (fx, fy, fz, ForceMode.Impulse);
			delay = 0.1f;
		} else {
			delay -= Time.deltaTime;
		}
	}
}
