using UnityEngine;

public class scr_herir : MonoBehaviour {
    [Header("Configuracion")]
    [SerializeField] int dano= 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            scr_jugador jugador = collision.gameObject.GetComponent<scr_jugador>();
            jugador.modificarCorazones(-dano);
            Debug.Log("Daño reañizado " + dano);
        }
    }
}

