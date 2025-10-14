using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Jump jumpScript;
    private scr_jugador jugador;

    private void Start()
    {
        jumpScript = GetComponentInParent<Jump>();
        jugador = GetComponentInParent<scr_jugador>();
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
