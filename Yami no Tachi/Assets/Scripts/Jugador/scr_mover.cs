using UnityEngine;

public class scr_mover : MonoBehaviour
{
    private scr_jugador jugador;
    private scr_attack ataque;
    private Rigidbody2D mi_rb2d;
    private Animator mi_animator;
    private float moverHorizontal;

    private void OnEnable()
    {
        jugador = GetComponentInParent<scr_jugador>();
        ataque = GetComponentInParent<scr_attack>();

        mi_rb2d = GetComponent<Rigidbody2D>();
        mi_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jugador.Datos.estaAtacando)
        {
            mi_animator.SetBool("isIdle", true);
            return; 
        }

        moverHorizontal = Input.GetAxisRaw("Horizontal");

        if (moverHorizontal != 0 )
            transform.localScale = new Vector3(Mathf.Sign(moverHorizontal), 1, 1);

        mi_animator.SetBool("isIdle", Mathf.Abs(moverHorizontal) < 0.1f);
    }

    private void FixedUpdate()
    {
        Vector2 nuevaVel = mi_rb2d.linearVelocity;
        nuevaVel.x = moverHorizontal * jugador.Datos.velocidad;
        mi_rb2d.linearVelocity = nuevaVel;
    }
}
