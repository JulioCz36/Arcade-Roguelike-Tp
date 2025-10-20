using UnityEngine;

public class FuegoLanzado : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    private Rigidbody2D rb;

    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Mover();
    }

    private void Mover()
    {
        Vector2 direccion = transform.right;
        rb.linearVelocity = direccion * velocidad;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scr_jugador jugador = other.GetComponentInParent<scr_jugador>();
            if (jugador != null)
            {
                Vector2 direccionGolpe = (other.transform.position - transform.position).normalized;
                jugador.modificarCorazones(-1, direccionGolpe);
            }
        }
        anim.SetTrigger("colisiono");
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
