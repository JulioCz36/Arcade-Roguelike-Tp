using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
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
                Vector2 direccionGolpe = (collision.transform.position - transform.position).normalized;
                jugador.modificarCorazones(-dano, direccionGolpe);
            }
        }
    }
}
