using UnityEngine;

public class scr_mover_enemigo_1 : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] float velocidad = 3f;
    [SerializeField] float distanciaDeteccion = 8f;

    [Header("Referencias")]
    [SerializeField] Transform jugador;

    private Rigidbody2D miRigidbody2D;
    private Vector2 direccion;

    private void Awake()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= distanciaDeteccion)
        {
            direccion = (jugador.position - transform.position).normalized;
            miRigidbody2D.MovePosition(miRigidbody2D.position + direccion * (velocidad * Time.fixedDeltaTime));
        }
        else
        {
            direccion = Vector2.zero;
        }
    }
}
