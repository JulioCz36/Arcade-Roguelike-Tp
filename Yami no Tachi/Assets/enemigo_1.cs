using System.Collections;
using UnityEngine;

public class enemigo_1 : MonoBehaviour
{
    [Header("Ataque")]
    [SerializeField] float tiempoEntreAtaques = 3f;
    [SerializeField] float rangoAtaque = 2f;
    [SerializeField] float retrocesoDistancia = 1f;
    [SerializeField] float retrocesoVelocidad = 3f;
    [SerializeField] float avanceVelocidad = 5f;
    [SerializeField] float tiempoIdleAntes = 0.5f;

    private Animator animator;
    private Transform jugador;
    private bool puedeAtacar = true;
    private bool atacando = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (jugador == null || atacando) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque && puedeAtacar)
        {
            StartCoroutine(PatronAtaque());
        }
    }

    private IEnumerator PatronAtaque()
    {
        atacando = true;
        puedeAtacar = false;

        animator.SetBool("idle", true);
        yield return new WaitForSeconds(tiempoIdleAntes);

        Vector2 dirRetroceso = (transform.position - jugador.position).normalized;
        float moved = 0f;
        while (moved < retrocesoDistancia)
        {
            float step = retrocesoVelocidad * Time.deltaTime;
            transform.position += (Vector3)dirRetroceso * step;
            moved += step;
            yield return null;
        }

        animator.SetTrigger("attack"); 
        Vector2 dirAtaque = (jugador.position - transform.position).normalized;
        float tiempoAvance = 0.3f; 
        float elapsed = 0f;
        while (elapsed < tiempoAvance)
        {
            transform.position += (Vector3)dirAtaque * (avanceVelocidad * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        animator.SetBool("idle", true);

        yield return new WaitForSeconds(tiempoEntreAtaques);

        puedeAtacar = true;
        atacando = false;
    }

    public void SetJugador(Transform target)
    {
        jugador = target;
    }
}
