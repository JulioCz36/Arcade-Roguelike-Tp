using UnityEngine;

public class scr_attack : MonoBehaviour
{

    private scr_jugador jugador;
    private Animator mi_animator;
    private bool isAttacking = false;

    public Transform attackPoint;

    private void OnEnable(){
        jugador = GetComponentInParent<scr_jugador>();
        mi_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        isAttacking = true;
        mi_animator.SetTrigger("attack");

        GameObject slash = Instantiate(jugador.Datos.slashPrefab, attackPoint.position, attackPoint.rotation, attackPoint);

        float duracion = mi_animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(slash, duracion);
    }

    public void TerminarAtaque()
    {
        isAttacking = false;
    }

}
