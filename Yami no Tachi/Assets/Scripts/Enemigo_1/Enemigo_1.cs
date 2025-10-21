using UnityEngine;

public class Enemigo_1 : MonoBehaviour
{

    [Header("Vida")]
    [SerializeField] private int vida = 3;

    [Header("Retroceso")]
    [SerializeField] private Vector2 fuerzaRetroceso;

    private Rigidbody2D rb2d;
    private Animator animator;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void RecibirDanio(int dano, Transform sender)
    {
        vida -= dano;
        Retroceso(sender);

        if (vida <= 0)
            Destroy(gameObject);
    }

    private void Retroceso(Transform sender)
    {
        Vector2 direccion = (transform.position - sender.position).normalized;

        Vector2 fuerza = new(Mathf.Sign(direccion.x) * fuerzaRetroceso.x, fuerzaRetroceso.y);

        animator.SetTrigger("hit");
        rb2d.AddForce(fuerza, ForceMode2D.Impulse);
    }
}
