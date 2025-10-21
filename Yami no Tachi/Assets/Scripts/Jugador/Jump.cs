using UnityEngine;

public class Jump : MonoBehaviour
{

    private Jugador jugador;
    private Rigidbody2D miRigidbody2D;
    private bool puedoSaltar = false;

    private void OnEnable()
    {
        jugador = GetComponentInParent<Jugador>();
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (jugador.Datos.estaEnPared) return;

        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar && !jugador.Datos.estaAtacando)
        {
            Vector2 vel = miRigidbody2D.linearVelocity;
            vel.y = jugador.Datos.fuerzaSalto;
            miRigidbody2D.linearVelocity = vel;
            puedoSaltar = false;
        }
    }

    public void SetPuedeSaltar(bool valor)
    {
        puedoSaltar = valor;
    }
}
