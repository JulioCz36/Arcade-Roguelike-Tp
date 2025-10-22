using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Jump jumpScript;
    private Jugador jugador;

    [Header("Configuración de suelo")]
    public LayerMask capaSuelo;
    public float radioDeteccion = 0.1f;

    private void Start()
    {
        jumpScript = GetComponentInParent<Jump>();
        jugador = GetComponentInParent<Jugador>();
    }

    private void Update()
    {
        bool enSuelo = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaSuelo);

        if (enSuelo)
        {
            jumpScript.SetPuedeSaltar(true);
            jugador.Datos.enSuelo = true;
        }
        else
        {
            jumpScript.SetPuedeSaltar(false);
            jugador.Datos.enSuelo = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    } 
}
