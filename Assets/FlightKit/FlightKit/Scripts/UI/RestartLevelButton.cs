using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlightKit
{
    public class RestartLevelButton : MonoBehaviour {

        public virtual void Restart() {
            Time.timeScale = 1;
			LabsMostrarOcultarPorSegundos.inmediato = true;//cancela el tips
            // Restart scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
