using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;


    public void Activar()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Desactivar()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IniciarJuego()
    {
        ApplicationManager.Instancia.GoToNextScene();
    }

    public void Opciones()
    {
        Debug.Log("Opciones");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instancia.BloquearJugador(false);    
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        GameManager.Instancia.BloquearJugador(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverAlMenu()
    {
        GameManager.Instancia.BloquearJugador(false);
        Time.timeScale = 1f;
        ApplicationManager.Instancia.GoToPreviousScene();
    }
}
