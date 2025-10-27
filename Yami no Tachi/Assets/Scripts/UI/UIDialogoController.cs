using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogoController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Text textoDialogo;
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioSource audioSource;

    public UnityEvent onDialogoActivo;
    public UnityEvent onDialogoFinalizado;

    private string[] lineasDialogo; 
    private int indiceActual;
    private string idCartelActual;
    private bool dialogoActivo;

    private void Start()
    {
        panelDialogo.SetActive(false);
        textoDialogo.gameObject.SetActive(false);
    }

    public void MostrarDialogo(string idCartel, string[] lineas, bool desbloqueaHabilidad)
    {
        if (dialogoActivo) return;

        idCartelActual = idCartel;
        lineasDialogo = lineas;
        indiceActual = 0;
        dialogoActivo = true;

        panelDialogo.SetActive(true);
        onDialogoActivo?.Invoke();
        GameManager.Instancia.BloquearJugador(true);

        audioSource.PlayOneShot(openSFX);
        textoDialogo.text = lineasDialogo[indiceActual];
    }

    private void Update()
    {
        if (!dialogoActivo) return;

        if (Input.GetButtonDown("Fire1"))
        {
            AvanzarDialogo();
        }
    }

    private void AvanzarDialogo()
    {
        indiceActual++;

        if (indiceActual >= lineasDialogo.Length)
        {
            CerrarDialogo();
            return;
        }

        textoDialogo.text = lineasDialogo[indiceActual];
    }

    private void CerrarDialogo()
    {
        dialogoActivo = false;
        textoDialogo.gameObject.SetActive(false);
        audioSource.PlayOneShot(openSFX);
        onDialogoFinalizado?.Invoke();

        SistemaProgresion.Instancia.MarcarCartelLeido(idCartelActual);
        SistemaProgresion.Instancia.DesbloquearHabilidad(idCartelActual);
    }

    public void MostrarTextoDialogo() {
        textoDialogo.gameObject.SetActive(true);
    }
    public void DesactivarDialogoBox()
    {
        panelDialogo.SetActive(false);
        GameManager.Instancia.BloquearJugador(false);
    }
}
