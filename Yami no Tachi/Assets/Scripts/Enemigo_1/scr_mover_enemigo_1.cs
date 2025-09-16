using UnityEngine;

public class scr_mover_enemigo_1 : MonoBehaviour
{
    [Header("Movimiento Patrulla")]
    [SerializeField] float velocidad = 2f;
    [SerializeField] float tiempoCaminar = 2f;
    [SerializeField] float tiempoIdle = 1.5f;

    [Header("Referencias")]
    [SerializeField] Animator animator;

    private Rigidbody2D rb;
    private bool mirandoDerecha = true;

    private Transform jugador;
    private bool persiguiendo = false;
    private Coroutine patrullaCoroutine;
    private enemigo_1 scriptAtaque;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrullaCoroutine = StartCoroutine(Patrullar());
        scriptAtaque = GetComponent<enemigo_1>();
    }

    private void FixedUpdate()
    {
        if (persiguiendo && jugador != null)
        {
            animator.SetBool("idle", false);
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direccion.x * velocidad, rb.linearVelocity.y);

            if ((direccion.x > 0 && !mirandoDerecha) || (direccion.x < 0 && mirandoDerecha))
            {
                mirandoDerecha = !mirandoDerecha;
                transform.localScale = new Vector3(mirandoDerecha ? 3 : -3, 3, 3);
            }
        }
    }

    public void SeguirJugador(Transform target)
    {
        jugador = target;
        persiguiendo = true;

        scriptAtaque.SetJugador(target);

        if (patrullaCoroutine != null)
        {
            StopCoroutine(patrullaCoroutine);
            patrullaCoroutine = null;
        }
    }

    public void DejarDeSeguir()
    {
        jugador = null;
        persiguiendo = false;
        scriptAtaque.SetJugador(null);

        if (gameObject.activeInHierarchy && patrullaCoroutine == null)
        {
            patrullaCoroutine = StartCoroutine(Patrullar());
        }
    }

    private System.Collections.IEnumerator Patrullar()
    {
        while (true)
        {
            // CAMINAR
            animator.SetBool("idle", false);
            float t = 0f;
            while (t < tiempoCaminar)
            {
                float dir = mirandoDerecha ? 1f : -1f;
                rb.linearVelocity = new Vector2(dir * velocidad, rb.linearVelocity.y);
                t += Time.deltaTime;
                yield return null;
            }

            rb.linearVelocity = Vector2.zero;

            animator.SetBool("idle", true);
            yield return new WaitForSeconds(tiempoIdle);

            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector3(mirandoDerecha ? 3 : -3, 3, 3);
        }
    }
}
