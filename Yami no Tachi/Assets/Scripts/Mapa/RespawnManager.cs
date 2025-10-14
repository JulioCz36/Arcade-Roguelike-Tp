using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instancia { get; private set; }

    private Transform ultimoCheckpoint;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ActualizarCheckpoint(Transform nuevoCheckpoint)
    {
        ultimoCheckpoint = nuevoCheckpoint;
    }

    public void ReaparecerJugador(GameObject jugador)
    {
        if (ultimoCheckpoint != null)
        {
            jugador.transform.position = ultimoCheckpoint.position;
        }
    }
}
