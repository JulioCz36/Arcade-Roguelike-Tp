using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    private Jugador jugador;
    private Animator mi_animator;

    public Transform attackPoint;

    private void OnEnable()
    {
        jugador = GetComponentInParent<Jugador>();
        mi_animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (SistemaProgresion.Instancia.puedeAtacar && Input.GetButtonDown("Fire1") && !jugador.Datos.estaAtacando)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        jugador.Datos.estaAtacando = true;
        mi_animator.SetTrigger("attack");

        GameObject slash = Instantiate(jugador.Datos.slashPrefab, attackPoint.position, attackPoint.rotation, attackPoint);
        float duracion = mi_animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(slash, duracion);
    }
    public void TerminarAtaque()
    {
        jugador.Datos.estaAtacando = false;
    }
}
