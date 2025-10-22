using Unity.VisualScripting;
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
    private float grabTimer = 0f;
    private int wallSide = 0;

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

        if (isTouchingWall)
        {
            wallSide = (transform.position.x < jugador.transform.position.x) ? -1 : 1;
            wallCoyoteTimer = wallCoyoteTime;
        }
        else
        {
            wallCoyoteTimer -= Time.deltaTime;
        }

        if ((isTouchingWall || wallCoyoteTimer > 0f) && !jugador.Datos.enSuelo)
        {
            if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) == wallSide)
            {
                if (!jugador.Datos.estaEnPared)
                {
                    jugador.Datos.estaEnPared = true;
                    grabTimer = jugador.Datos.tiempoAgarrePared;
                    rb.gravityScale = 0f;
                    rb.linearVelocity = Vector2.zero;
                }

                grabTimer -= Time.deltaTime;
                rb.linearVelocity = new Vector2(0, -jugador.Datos.velocidadDesliz);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && canWallJump && !jugador.Datos.estaAtacando && (jugador.Datos.estaEnPared || wallCoyoteTimer > 0))
        {
            canWallJump = false;
            jugador.Datos.estaEnPared = false;
            rb.gravityScale = jugador.Datos.gravedadNormal;

            Vector2 direccionSalto = new Vector2(-wallSide, 1f).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direccionSalto * jugador.Datos.fuerzaSaltoPared, ForceMode2D.Impulse);

            Invoke(nameof(ResetWallJump), 0.25f);
        }

        if (grabTimer <= 0 || Mathf.Sign(Input.GetAxisRaw("Horizontal")) != wallSide)
        {
            jugador.Datos.estaEnPared = false;
            rb.gravityScale = jugador.Datos.gravedadNormal;
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
