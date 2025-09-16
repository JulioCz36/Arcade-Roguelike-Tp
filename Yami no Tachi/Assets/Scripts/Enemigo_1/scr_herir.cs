using UnityEngine;

public class scr_herir : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("modificarCorazones", -dano);
        }
    }
}

