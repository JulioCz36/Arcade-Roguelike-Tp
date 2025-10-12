using UnityEngine;

public class EspadaJugador : MonoBehaviour
{
    private scr_jugador jugador;

    private void Start()
    {
        jugador = GetComponentInParent<scr_jugador>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.SendMessageUpwards("RecibirDanio", jugador.Datos.dano);
        }
    }
}
