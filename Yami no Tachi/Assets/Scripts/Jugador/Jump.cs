using UnityEngine;

public class Jump : MonoBehaviour
{

    [Header("Configuracion")]
    [SerializeField] private float fuerzaSalto = 5f;

    private Rigidbody2D miRigidbody2D;
    private bool puedoSaltar = false;

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            Vector2 vel = miRigidbody2D.linearVelocity;
            vel.y = fuerzaSalto;
            miRigidbody2D.linearVelocity = vel;
            puedoSaltar = false;
        }
    }

    public void SetPuedeSaltar(bool valor)
    {
        puedoSaltar = valor;
    }
}
