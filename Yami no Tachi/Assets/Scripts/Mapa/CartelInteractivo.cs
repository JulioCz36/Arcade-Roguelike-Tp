using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CartelInteractivo : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string idCartel;
    [SerializeField] private bool desbloqueaHabilidad = false;
    [SerializeField] private string[] lineasDialogo;

    [Header("Referencias")]
    [SerializeField] private GameObject uiDialogo;
    [SerializeField] private GameObject particula;
    [SerializeField] private Text textoDialogo;

    public UnityEvent onAbrirDialogo;
    public UnityEvent onCerrarDialogo;

    private bool jugadorCerca = false;
    private bool mostrandoDialogo = false;
    private int indiceDialogo = 0;
    private bool yaLeido = false;

    private void Start()
    {
        uiDialogo.SetActive(false);
        textoDialogo.gameObject.SetActive(false);
        particula.SetActive(false);
        
        if (SistemaProgresion.Instancia.CartelYaLeido(idCartel))
        {
            yaLeido = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!yaLeido && other.CompareTag("Player"))
        {
            jugadorCerca = true;
            particula.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            particula.SetActive(false); 
            CerrarDialogo();
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetButtonDown("Vertical"))
        {
            if (!mostrandoDialogo)
            {
                MostrarDialogo();
            }
        }
        if (mostrandoDialogo && Input.GetButtonDown("Fire1"))
        {
            AvanzarDialogo();
        }
    }

    private void MostrarDialogo()
    {
        if (uiDialogo == null || textoDialogo == null) return;

        SistemaProgresion.Instancia.puedeAtacar = false;

        particula.SetActive(false);

        mostrandoDialogo = true;
        indiceDialogo = 0;

        uiDialogo.SetActive(true);
        textoDialogo.text = lineasDialogo[indiceDialogo];

        onAbrirDialogo?.Invoke();
    }

    private void AvanzarDialogo()
    {
        indiceDialogo++;
        if (indiceDialogo >= lineasDialogo.Length)
        {
            CerrarDialogo();
            return;
        }

        textoDialogo.text = lineasDialogo[indiceDialogo];
    }

    private void CerrarDialogo()
    {
        mostrandoDialogo = false;
        textoDialogo.gameObject.SetActive(false);

        onCerrarDialogo?.Invoke();
    }
    public void MostrarTextoDialogo()
    {
         textoDialogo.gameObject.SetActive(true);
    }

    public void DesactivarUI()
    {
       uiDialogo.SetActive(false);

       //no ataque cuando pasa de dialogo
       SistemaProgresion.Instancia.puedeAtacar = true;

        SistemaProgresion.Instancia.MarcarCartelLeido(idCartel);

        if (desbloqueaHabilidad)
            SistemaProgresion.Instancia.DesbloquearHabilidad(idCartel);
    }

}
