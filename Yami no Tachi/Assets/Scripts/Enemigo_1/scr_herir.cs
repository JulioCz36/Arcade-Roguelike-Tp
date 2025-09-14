using UnityEngine;

public class scr_herir : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] int dano = 1;
    [SerializeField] private float damageCooldown = 5f;
    private float lastDamageTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            scr_jugador player = collision.GetComponent<scr_jugador>();
            if (player != null)
            {
                player.modificarCorazones(-dano);
                lastDamageTime = Time.time;
            }
        }
    }
}

