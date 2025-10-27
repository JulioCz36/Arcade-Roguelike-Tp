using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioSource accionesAudioSource;


    private Jugador jugador;
    private Rigidbody2D miRigidbody2D;
    private Animator animator;

    private void OnEnable()
    {
        jugador = GetComponentInParent<Jugador>();
        miRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        accionesAudioSource.volume = 0.5f;
    }
    void Update()
    {

        if (Input.GetButtonDown("Jump") && jugador.Datos.enSuelo && !jugador.Datos.estaAtacando && !jugador.Datos.estaEnPared)
        {

            accionesAudioSource.PlayOneShot(jumpSFX);

            miRigidbody2D.AddForce(Vector2.up * jugador.Datos.fuerzaSalto, ForceMode2D.Impulse);
        }
        animator.SetBool("isGrounded", jugador.Datos.enSuelo);
        animator.SetFloat("verticalVelocity", miRigidbody2D.linearVelocity.y);
    }
}
