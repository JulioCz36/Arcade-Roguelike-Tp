using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Jugador jugador;

    [Header("Configuración de suelo")]
    public LayerMask capaSuelo;
    public float radioDeteccion = 0.1f;

    private void Start()
    {
        jugador = GetComponentInParent<Jugador>();
    }

    private void Update()
    {
        bool enSuelo = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaSuelo);

        jugador.Datos.enSuelo = enSuelo;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    } 
}
