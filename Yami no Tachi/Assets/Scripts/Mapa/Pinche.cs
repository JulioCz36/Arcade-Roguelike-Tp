using UnityEngine;

public class Pinche : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Jugador jugador = collision.GetComponentInParent<Jugador>();
            if (jugador != null)
            {
                Vector2 direccionGolpe = new(0,0);
                jugador.modificarCorazones(-dano, direccionGolpe);
                RespawnManager.Instance.RespawnearJugador(jugador);
            }
        }
    }
}
