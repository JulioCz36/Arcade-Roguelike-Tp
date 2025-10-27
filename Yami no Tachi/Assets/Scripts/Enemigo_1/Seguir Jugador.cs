using System.Collections;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip detectoSFX;
    [SerializeField] private AudioSource movimietoAudioSource;
    private bool sonidoDetectadoReproducido = false;

    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;

    public float velocidadMovimiento;
    public float ditanciaMaxima;
    public Vector3 puntoInicial;

    public bool mirandoDerecha;

    public float tiempoOcupado = 0.5f;
    private float contadorOcupado;

    [Header("Ataque")]
    public float radioAtaque = 1.5f;
    public float tiempoEntreAtaques = 1.2f;
    private bool puedeAtacar = true;

    private Rigidbody2D rb2d;
    private Animator animator;

    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
        Ocupado,
        Recuperandose,
    }
    private void Start()
    {
        puntoInicial = transform.position;
        estadoActual = EstadosMovimiento.Esperando;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;
            case EstadosMovimiento.Ocupado:
                EstadoOcupado();
                break;

        }
    }

    private void EstadoEsperando()
    {
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider)
        {
            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        if (!sonidoDetectadoReproducido)
        {
            movimietoAudioSource.PlayOneShot(detectoSFX);
            sonidoDetectadoReproducido = true;
        }

        animator.SetBool("walk", true);
        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        float distanciaJugador = Vector2.Distance(transform.position, transformJugador.position);

        if (distanciaJugador <= radioAtaque && puedeAtacar)
        {
            IniciarAtaque();
            return; 
        }


        if (distanciaJugador > radioAtaque)
        {
            if (transform.position.x < transformJugador.position.x)
                rb2d.linearVelocity = new Vector2(velocidadMovimiento, rb2d.linearVelocity.y);
            else
                rb2d.linearVelocity = new Vector2(-velocidadMovimiento, rb2d.linearVelocity.y);
        }

        GirarAObjetivo(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > ditanciaMaxima || Vector2.Distance(transform.position, transformJugador.position) > ditanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
            sonidoDetectadoReproducido = false;
        }
    }

    private void IniciarAtaque()
    {
        rb2d.linearVelocity = Vector2.zero;
        animator.SetTrigger("attack"); 
        puedeAtacar = false;

        StartCoroutine(ReiniciarAtaque());
    }

    private IEnumerator ReiniciarAtaque()
    {
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    private void EstadoVolviendo()
    {
        if (transform.position.x < puntoInicial.x)
        {
            rb2d.linearVelocity = new Vector2(velocidadMovimiento, rb2d.linearVelocity.y);
        }
        else
        {
            rb2d.linearVelocity = new Vector2(-velocidadMovimiento, rb2d.linearVelocity.y);
        }

        GirarAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 0.1f)
        {
            rb2d.linearVelocity = Vector2.zero;
            animator.SetBool("walk", false);
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void EstadoOcupado()
    {
        animator.SetBool("walk", false);

        contadorOcupado -= Time.deltaTime;
        if (contadorOcupado <= 0)
        {
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    public void ActivarOcupado()
    {
        estadoActual = EstadosMovimiento.Ocupado;
        contadorOcupado = tiempoOcupado;
    }

    private void GirarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(puntoInicial, ditanciaMaxima);
    }
}
