using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    private Menu menuYouWin;
    private Menu menuGameOver;
    private Menu menuPausa;

    private Jugador jugador;
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        BuscarReferenciasEnEscena();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarReferenciasEnEscena();
    }

    private void BuscarReferenciasEnEscena()
    {
        jugador = FindAnyObjectByType<Jugador>();

        var pausaGO = GameObject.FindGameObjectWithTag("MenuPausa");
        var gameOverGO = GameObject.FindGameObjectWithTag("MenuGameOver");
        var youWinGO = GameObject.FindGameObjectWithTag("MenuYouWin");

        menuPausa = pausaGO ? pausaGO.GetComponent<Menu>() : null;
        menuGameOver = gameOverGO ? gameOverGO.GetComponent<Menu>() : null;
        menuYouWin = youWinGO ? youWinGO.GetComponent<Menu>() : null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0f)
                GameEvents.TriggerPause();
            else
                GameEvents.TriggerResume();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnPause += Pausar;
        GameEvents.OnResume += Reanudar;
    }

    private void OnDisable()
    {
        GameEvents.OnPause -= Pausar;
        GameEvents.OnResume -= Reanudar;
    }
    private void Pausar()
    {
        Time.timeScale = 0f;
        jugador.BloquearControl();
        menuPausa.Activar();
    }
    private void Reanudar()
    {
        menuPausa.Desactivar();
        Time.timeScale = 1f;
        jugador.DesbloquearControl();
    }

    public void MarcarVictoria()
    {
        GameManager.Instancia.BloquearJugador(true);
        menuYouWin.Activar();
    }
    public void MarcarDerrota()
    {
        GameManager.Instancia.BloquearJugador(true);
        menuGameOver.Activar();
    }

    public void BloquearJugador(bool estado)
    {
        if (estado)
        {
            jugador.BloquearControl();
        }
        else {
            jugador.DesbloquearControl();
        }
    }
}
