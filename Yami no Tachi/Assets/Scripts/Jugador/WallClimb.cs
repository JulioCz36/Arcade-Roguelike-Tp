using UnityEngine;

public class WallClimb : MonoBehaviour
{

    [Header("Detección de pared")]
    [SerializeField] private float radioDeteccion = 0.2f;
    [SerializeField] private LayerMask capaPared;

    private Rigidbody2D rb;
    private Jugador jugador;
    //private Animator animator;

    private bool canWallJump = true;
    private int wallSide = 1;

    private float wallCoyoteTime = 0.15f;
    private float wallCoyoteTimer = 0f;

    private void Start()
    {
        jugador = GetComponentInParent<Jugador>();
        rb = jugador.GetComponent<Rigidbody2D>();
        //animator = jugador.GetComponent<Animator>();
    }
    private void Update()
    {

        Collider2D paredDetectada = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaPared);

        jugador.Datos.estaEnPared = paredDetectada;

        if (paredDetectada)
            wallSide = (transform.position.x < paredDetectada.transform.position.x) ? 1 : -1;

        bool intentandoHaciaPared = Mathf.Sign(Input.GetAxisRaw("Horizontal")) == wallSide;

        //animator.SetBool("isWallClimbing", jugador.Datos.estaEnPared && SistemaProgresion.Instancia.puedePegarPared);

        if (SistemaProgresion.Instancia.puedePegarPared && paredDetectada && intentandoHaciaPared && !jugador.Datos.enSuelo)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0, -jugador.Datos.velocidadDesliz);
            wallCoyoteTimer = wallCoyoteTime;
        }
        else
        {
            rb.gravityScale = jugador.Datos.gravedadNormal;
            wallCoyoteTimer -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && canWallJump && !jugador.Datos.enSuelo && (paredDetectada || wallCoyoteTimer > 0) && SistemaProgresion.Instancia.puedePegarPared)
        {
            canWallJump = false;
            jugador.Datos.estaEnPared = false;
            rb.gravityScale = jugador.Datos.gravedadNormal;

            Vector2 direccionSalto = new Vector2(-wallSide, 1f).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direccionSalto * jugador.Datos.fuerzaSaltoPared, ForceMode2D.Impulse);

            Invoke(nameof(ResetWallJump), 0.25f);
        }
    }

    private void ResetWallJump()
    {
        canWallJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
