using UnityEngine;

public class Pilar : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioSource accionesAudioSource;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        accionesAudioSource.volume = 1.0f;
    }
    public void CompletoBusqueda()
    {
        animator.SetBool("completado", true);
    }
    public void CargarPilar()
    {
        animator.SetTrigger("cargar");
        accionesAudioSource.PlayOneShot(openSFX);
    }
    public void AbirPilar()
    {
        animator.SetTrigger("abrir");
    }
    public void DestruirPilar()
    {
        GameManager.Instancia.BloquearJugador(false);
        Destroy(gameObject);
    }
}
