using UnityEngine;
using UnityEngine.Events;

public class scr_jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int corazones = 3;
    [SerializeField] private UnityEvent<int> OnLivesChanged;

    private void Start()
    {
        OnLivesChanged.Invoke(corazones);

    }
    public void modificarCorazones(int dano)
    {
        corazones += dano;
        OnLivesChanged.Invoke(corazones);
    }
}