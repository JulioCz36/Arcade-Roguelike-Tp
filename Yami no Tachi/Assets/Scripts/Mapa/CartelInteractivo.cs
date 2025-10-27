using UnityEngine;

public class CartelInteractivo : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string idCartel;
    [SerializeField] private bool desbloqueaHabilidad = false;
    [SerializeField] private string[] lineasDialogo;

    [Header("Referencias")]
    [SerializeField] private GameObject particula;
    [SerializeField] private UIDialogoController uiDialogo;

    private bool jugadorCerca;
    private bool yaLeido;

    private void Start()
    {
        particula.SetActive(false);

        if (SistemaProgresion.Instancia.CartelYaLeido(idCartel))
            yaLeido = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!yaLeido && other.CompareTag("Player"))
        {
            particula.SetActive(true);
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            particula.SetActive(false);
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetButtonDown("Vertical"))
        {
            uiDialogo.MostrarDialogo(idCartel, lineasDialogo, desbloqueaHabilidad);
            particula.SetActive(false);
        }
    }
}
