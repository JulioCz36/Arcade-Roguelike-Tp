using UnityEngine;

public class Pedestal : MonoBehaviour
{
    private Pilar pilar;

    private bool jugadorCerca = false;
    private bool puedeActivarse = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        pilar = GameObject.FindGameObjectWithTag("Pilar").GetComponent<Pilar>();

        SistemaProgresion.Instancia.OnFragmentoRecolectado += RevisarProgreso;
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
            Debug.Log("Jugador cerca del pedestal");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador se fue");
            jugadorCerca = false;
        }
    }
    private void Update()
    {
        if (jugadorCerca && puedeActivarse && Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetTrigger("interaccion");
        }
    }
    public void FinalizarActivacion()
    {
        animator.SetBool("abierto", true);
    }
    public void AbrirPuerta()
    {
        Debug.Log("Puerta abierta desde el pedestal!");
        pilar.CargarPilar(); 
    }
}
