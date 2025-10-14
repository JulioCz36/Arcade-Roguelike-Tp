using UnityEngine;

public class WallClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private scr_jugador jugador;

    private bool isTouchingWall;
    private bool canWallJump = true;
    private float grabTimer = 0f;
    private int wallSide = 0;

    private float wallCoyoteTime = 0.15f;
    private float wallCoyoteTimer = 0f;

    private void Start()
    {
        jugador = GetComponentInParent<scr_jugador>();
        rb = jugador.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isTouchingWall)
            wallCoyoteTimer = wallCoyoteTime;
        else
            wallCoyoteTimer -= Time.deltaTime;

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

            Vector2 direccionSalto;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (vertical > 0.5f)
            {
                direccionSalto = new Vector2(0f, 1.2f).normalized;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(direccionSalto * jugador.Datos.fuerzaSaltoPared * 1.3f, ForceMode2D.Impulse);
            }
            else
            {
                direccionSalto = new Vector2(-wallSide, 1f).normalized;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(direccionSalto * jugador.Datos.fuerzaSaltoPared, ForceMode2D.Impulse);
            }
            Invoke(nameof(ResetWallJump), 0.25f);
        }

        if (grabTimer <= 0 || Mathf.Sign(Input.GetAxisRaw("Horizontal")) != wallSide)
        {
            jugador.Datos.estaEnPared = false;
            rb.gravityScale = jugador.Datos.gravedadNormal;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            isTouchingWall = true;
            wallSide = (collision.contacts[0].point.x < transform.position.x) ? -1 : 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            isTouchingWall = false;
            jugador.Datos.estaEnPared = false;
            rb.gravityScale = jugador.Datos.gravedadNormal;
        }
    }

    private void ResetWallJump()
    {
        canWallJump = true;
    }
}
