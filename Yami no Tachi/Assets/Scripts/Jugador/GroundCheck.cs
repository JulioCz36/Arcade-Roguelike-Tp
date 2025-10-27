using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip jumpDownSFX;
    [SerializeField] private AudioSource accionesAudioSource;

    [Header("Configuración de suelo")]
    public LayerMask capaSuelo;
    public float radioDeteccion = 0.1f;

    private Jugador jugador;
    private bool estabaEnSuelo;

    private void Start()
    {
        jugador = GetComponentInParent<Jugador>();
    }

    private void Update()
    {
        bool enSuelo = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaSuelo);

        if (!estabaEnSuelo && enSuelo)
        {
            if (accionesAudioSource.isPlaying) return;
            accionesAudioSource.PlayOneShot(jumpDownSFX);
        }

        estabaEnSuelo = enSuelo;
        jugador.Datos.enSuelo = enSuelo;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    } 
}
