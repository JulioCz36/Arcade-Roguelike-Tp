using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private GameObject particula;

    private Pilar pilar;

    private bool jugadorCerca = false;
    private bool puedeActivarse = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        pilar = GameObject.FindGameObjectWithTag("Pilar").GetComponent<Pilar>();

        SistemaProgresion.Instancia.OnFragmentoRecolectado += RevisarProgreso;
        particula.SetActive(false);
    }

    private void OnDisable()
    {
        SistemaProgresion.Instancia.OnFragmentoRecolectado -= RevisarProgreso;
    }

    private void RevisarProgreso(string nombre)
    {
        if (SistemaProgresion.Instancia.VerificarProgreso())
        {
            puedeActivarse = true;
            animator.SetBool("completado", true);
            pilar.CompletoBusqueda();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
        if (jugadorCerca && puedeActivarse)
        {
            particula.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
    private void Update()
    {
        if (jugadorCerca && puedeActivarse && Input.GetButtonDown("Vertical"))
        {
            particula.SetActive(false);
            animator.SetTrigger("interaccion");
            puedeActivarse = false;
        }
    }
    public void FinalizarActivacion()
    {

        animator.SetBool("abierto", true);
    }
    public void AbrirPuerta()
    {
        pilar.CargarPilar(); 
    }
}
