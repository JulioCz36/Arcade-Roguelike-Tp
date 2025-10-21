using UnityEngine;

public class Pilar : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CompletoBusqueda()
    {
        animator.SetBool("completado", true);
    }
    public void CargarPilar()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("cargar");
    }
    public void AbirPilar()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("abrir");
    }
    public void DestruirPilar()
    {
        Destroy(gameObject);
    }
}
