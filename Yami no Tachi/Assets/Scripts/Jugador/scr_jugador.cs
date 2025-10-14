using UnityEngine;
using UnityEngine.Events;

public class scr_jugador : MonoBehaviour
{
    [Header("Datos del jugador")]
    [SerializeField] private JugadorDAta data;
    private int corazones;

    [SerializeField] private UnityEvent<int> OnLivesChanged;

    public JugadorDAta Datos => data;
    private void Start()
    {
        OnLivesChanged.Invoke(data.corazones);
        corazones = data.corazones;

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
}