using UnityEngine;

public class EspadaJugador : MonoBehaviour
{
    [Header("Configuración de daño")]
    [SerializeField] int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.SendMessageUpwards("RecibirDanio", dano);
        }
    }
}
