using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Jugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioSource accionesAudioSource;

    [Header("Datos del jugador")]
    [SerializeField] private JugadorDAta data;
    [SerializeField] private UnityEvent<int> OnLivesChanged;
    public JugadorDAta Datos => data;

    private int corazones;
    private Animator animator;
    private MovimientoJugador movimiento;
    private void Start()
    {
        corazones = data.corazones;
        movimiento = GetComponent<MovimientoJugador>();
        animator = GetComponent<Animator>();

        OnLivesChanged.Invoke(corazones);
    }

    public void modificarCorazones(int dano, Vector2 direccion)
    {
        accionesAudioSource.PlayOneShot(hitSFX);

        corazones += dano;
        OnLivesChanged.Invoke(corazones);

        animator.SetTrigger("hit");

        Datos.estaAtacando = false;

        if (corazones > 0)
            StartCoroutine(PerderControl());
        StartCoroutine(DesactivarCollision());
        movimiento.Rebotar(direccion);

        if (corazones <= 0)
            GameManager.Instancia.MarcarDerrota();
    }

    private IEnumerator DesactivarCollision()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(data.tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    private IEnumerator PerderControl()
    {
        data.sePuedeMover = false;
        yield return new WaitForSeconds(data.tiempoPerdidaControl);
        data.sePuedeMover = true;
    }

    public void BloquearControl()
    {
        Datos.sePuedeMover = false;
        Datos.estaAtacando = true;
    }

    public void DesbloquearControl()
    {
        Datos.sePuedeMover = true;
        Datos.estaAtacando = false;
    }
}
