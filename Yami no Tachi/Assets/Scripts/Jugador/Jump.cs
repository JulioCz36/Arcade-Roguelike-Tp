using UnityEngine;

public class Jump : MonoBehaviour
{

    private Jugador jugador;
    private Rigidbody2D miRigidbody2D;
    private Animator animator;

    private void OnEnable()
    {
        jugador = GetComponentInParent<Jugador>();
        miRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (jugador.Datos.estaEnPared) return;

        if (Input.GetButtonDown("Jump") && jugador.Datos.enSuelo && !jugador.Datos.estaAtacando)
        {
            miRigidbody2D.AddForce(Vector2.up * jugador.Datos.fuerzaSalto, ForceMode2D.Impulse);
        }
        animator.SetBool("isGrounded", jugador.Datos.enSuelo);
        animator.SetFloat("verticalVelocity", miRigidbody2D.linearVelocity.y);
    }
}
