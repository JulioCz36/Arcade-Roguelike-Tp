using UnityEngine;

public class Pinche : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("modificarCorazones", -1);
            RespawnManager.Instancia.ReaparecerJugador(collision.gameObject);
        }
    }
}
