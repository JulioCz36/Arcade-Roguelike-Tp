using UnityEngine;
using UnityEngine.Events;

public class scr_jugador : MonoBehaviour
{
    [Header("Datos del jugador")]
    [SerializeField] private JugadorDAta data;

    [SerializeField] private UnityEvent<int> OnLivesChanged;
    [SerializeField] private MenuCondicion menuGameOver;


    public JugadorDAta Datos => data;
    private void Start()
    {
        OnLivesChanged.Invoke(data.corazones);

    }
    public void modificarCorazones(int dano)
    {
        data.corazones += dano;
        if (data.corazones <= 0)
        {
            menuGameOver.Activar();
        }
        OnLivesChanged.Invoke(data.corazones);
    }
}