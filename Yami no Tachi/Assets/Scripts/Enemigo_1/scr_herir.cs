using UnityEngine;

public class scr_herir : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scr_jugador jugador = collision.GetComponentInParent<scr_jugador>();
            if (jugador != null)
            {
                Vector2 direccionGolpe = (collision.transform.position - transform.position).normalized;
                jugador.modificarCorazones(-dano, direccionGolpe);
            }
        }
    }
}

