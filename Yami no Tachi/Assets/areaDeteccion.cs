using UnityEngine;

public class areaDeteccion : MonoBehaviour
{
    public scr_mover_enemigo_1 enemigo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemigo.SeguirJugador(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemigo.DejarDeSeguir();
        }
    }
}
