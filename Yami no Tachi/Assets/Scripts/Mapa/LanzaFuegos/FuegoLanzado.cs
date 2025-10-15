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
            other.SendMessageUpwards("modificarCorazones", -1);
        }
        anim.SetTrigger("colisiono");
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
