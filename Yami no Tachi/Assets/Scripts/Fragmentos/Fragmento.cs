using UnityEngine;

public class Fragmento : MonoBehaviour
{
    [SerializeField] private string idFragmento;

    private Animator animator;
    private bool recogido = false;

    private void OnEnable()
    { 
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (recogido) return; 
        if (collision.CompareTag("Player"))
        {
            recogido = true;
            animator.SetTrigger("up");
            SistemaProgresion.Instancia.AgregarFragmento(idFragmento);
            StartCoroutine(EsperarAnimacionYDestruir());
        }
    }

    private System.Collections.IEnumerator EsperarAnimacionYDestruir()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}
