using UnityEngine;

public class WallClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private Jugador jugador;

    [Header("Detección de pared")]
    [SerializeField] private float radioDeteccion = 0.2f;
    [SerializeField] private LayerMask capaPared;

    private bool isTouchingWall;
    private bool canWallJump = true;
    private int wallSide = 1;

    private float wallCoyoteTime = 0.15f;
    private float wallCoyoteTimer = 0f;

    private void Start()
    {
        jugador = GetComponentInParent<Jugador>();
        rb = jugador.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D paredDetectada = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaPared);
        isTouchingWall = paredDetectada != null;

        if (isTouchingWall && SistemaProgresion.Instancia.puedePegarPared)
            wallCoyoteTimer = wallCoyoteTime;
        else
            wallCoyoteTimer -= Time.deltaTime;

        bool intentandoHaciaPared = Mathf.Sign(Input.GetAxisRaw("Horizontal")) == wallSide;

        if (isTouchingWall && intentandoHaciaPared && !jugador.Datos.enSuelo && SistemaProgresion.Instancia.puedePegarPared)
        {
            jugador.Datos.estaEnPared = true;

            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0, -jugador.Datos.velocidadDesliz);
        }
        else
        {
            if (jugador.Datos.estaEnPared)
            {
                jugador.Datos.estaEnPared = false;
                rb.gravityScale = jugador.Datos.gravedadNormal;
            }
        }
        if (Input.GetButtonDown("Jump") && canWallJump && (jugador.Datos.estaEnPared || wallCoyoteTimer > 0))
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
        if (transform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radioDeteccion);
        }
    }
}
