using UnityEngine;

public class FuegoLanzado : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip impactoSFX;
    [SerializeField] private AudioSource accionesAudioSource;

    [Header("Movimiento")]
    public float velocidad = 5f;
    private Rigidbody2D rb;

    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        accionesAudioSource.volume = 0.1f;
    }

    private void OnEnable()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Mover();
    }

    private void Mover()
    {
        Vector2 direccion = transform.right;
        rb.linearVelocity = direccion * velocidad;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        accionesAudioSource.PlayOneShot(impactoSFX);
        if (other.CompareTag("Player"))
        {
            Jugador jugador = other.GetComponentInParent<Jugador>();
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
