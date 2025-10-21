using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Jump jumpScript;
    private Jugador jugador;

    private void Start()
    {
        jumpScript = GetComponentInParent<Jump>();
        jugador = GetComponentInParent<Jugador>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            jumpScript.SetPuedeSaltar(true);
            jugador.Datos.enSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            jumpScript.SetPuedeSaltar(false);
            jugador.Datos.enSuelo = false;
        }
    }
}
