using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    private Transform ultimoCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        ultimoCheckpoint = checkpoint;
    }

    public void RespawnearJugador(Jugador jugador)
    {
        if (ultimoCheckpoint != null)
        {
            jugador.transform.position = ultimoCheckpoint.position;

            Animator animator = jugador.GetComponent<Animator>();
            if (animator != null) animator.SetTrigger("respawn");
        }

    }
}
