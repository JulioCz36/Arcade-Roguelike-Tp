using UnityEngine;
using UnityEngine.Events;

public class scr_jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int corazones = 3;
    [SerializeField] private UnityEvent<int> OnLivesChanged;
    [SerializeField] private MenuCondicion menuGameOver;

    private void Start()
    {
        OnLivesChanged.Invoke(corazones);

    }
    public void modificarCorazones(int dano)
    {
        corazones += dano;
        if (corazones <= 0)
        {
            menuGameOver.Activar();
            //Destroy(gameObject);
        }
        OnLivesChanged.Invoke(corazones);
    }
}