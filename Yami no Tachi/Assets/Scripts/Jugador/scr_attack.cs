using UnityEngine;

public class scr_attack : MonoBehaviour
{

    private Animator mi_animator;
    private bool isAttacking = false;

    [Header("Efecto de corte")]
    public GameObject slashPrefab;  
    public Transform attackPoint;

    private void OnEnable(){
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

        GameObject slash = Instantiate(slashPrefab, attackPoint.position, attackPoint.rotation, attackPoint);

        float duracion = mi_animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(slash, duracion);
    }

    public void TerminarAtaque()
    {
        isAttacking = false;
    }

}
