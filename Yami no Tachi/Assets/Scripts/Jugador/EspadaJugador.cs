using UnityEngine;

public class EspadaJugador : MonoBehaviour
{
    private Jugador jugador;

    private void Start()
    {
        jugador = GetComponentInParent<Jugador>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            Enemigo_1 enemigo = collision.GetComponentInParent<Enemigo_1>();
            if (enemigo != null)
            {
  
                enemigo.RecibirDanio(jugador.Datos.dano, jugador.transform);
            }
        }
    }
}
