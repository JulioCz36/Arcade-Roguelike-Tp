using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Jugador jugador;
    private Rigidbody2D mi_rb2d;
    private Animator mi_animator;
    private float moverHorizontal;

    private void OnEnable()
    {
        jugador = GetComponentInParent<Jugador>();

        mi_rb2d = GetComponent<Rigidbody2D>();
        mi_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!jugador.Datos.sePuedeMover || jugador.Datos.estaAtacando) return;

        moverHorizontal = Input.GetAxisRaw("Horizontal");

        if (moverHorizontal != 0)
            transform.localScale = new Vector3(Mathf.Sign(moverHorizontal), 1, 1);

        mi_animator.SetBool("isIdle", Mathf.Abs(moverHorizontal) < 0.1f);
    }

    private void FixedUpdate()
    {
        if (!jugador.Datos.sePuedeMover || jugador.Datos.estaAtacando)
        {
            Vector2 vel = mi_rb2d.linearVelocity;
            vel.x = 0;
            mi_rb2d.linearVelocity = vel;
            return;
        }

        Vector2 nuevaVel = mi_rb2d.linearVelocity;
        nuevaVel.x = moverHorizontal * jugador.Datos.velocidad;
        mi_rb2d.linearVelocity = nuevaVel;
    }

    public void EmpezarRespawn()
    {
        jugador.Datos.sePuedeMover = false;
    }

    public void TerminarRespawn()
    {
        jugador.Datos.sePuedeMover = true;
    }

    public void Rebotar(Vector2 puntoGolpe)
    {
        mi_rb2d.linearVelocity = new Vector2(jugador.Datos.velocidadRebote.x * puntoGolpe.x, jugador.Datos.velocidadRebote.y);
    }
}
