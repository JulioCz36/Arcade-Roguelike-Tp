using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class scr_jugador : MonoBehaviour
{
    [Header("Datos del jugador")]
    [SerializeField] private JugadorDAta data;
    [SerializeField] private UnityEvent<int> OnLivesChanged;
    public JugadorDAta Datos => data;
    
    private int corazones;
    private Animator animator;
    private scr_mover movimiento;
    private void Start()
    {
        OnLivesChanged.Invoke(data.corazones);
        corazones = data.corazones;
        movimiento = GetComponent<scr_mover>();
        animator = GetComponent<Animator>();

    }
    public void modificarCorazones(int dano)
    {
        corazones += dano;
        if (corazones <= 0)
        {
            SistemaProgresion.Instancia.MarcarDerrota();
        }
        OnLivesChanged.Invoke(corazones);
    }

    public void modificarCorazones(int dano, Vector2 direccion)
    {
        corazones += dano;
        animator.SetTrigger("hit");
        StartCoroutine(PerderControl());
        StartCoroutine(DesactivarCollision());
        movimiento.Rebotar(direccion);

        if (corazones <= 0)
            SistemaProgresion.Instancia.MarcarDerrota();

        OnLivesChanged.Invoke(corazones);
    }

    private IEnumerator DesactivarCollision()
    {
        Physics2D.IgnoreLayerCollision(9,10,true);
        yield return new WaitForSeconds(data.tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    private IEnumerator PerderControl()
    {
        data.sePuedeMover = false;
        yield return new WaitForSeconds(data.tiempoPerdidaControl);
        data.sePuedeMover = true;
    }
}