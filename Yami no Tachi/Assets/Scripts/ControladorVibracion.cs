using Unity.Cinemachine;
using UnityEngine;

public class ControladorVibracion : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CinemachineImpulseSource cinemachine;

    [Header("Vibracion Camara")]
    [SerializeField] private float vibracionX;
    [SerializeField] private float vibracionY;

    private void OnEnable()
    {
        EspadaJugador.jugadorGolpeo += GenerarMovimientoCamara;
    }

    private void OnDisable()
    {
        EspadaJugador.jugadorGolpeo -= GenerarMovimientoCamara;
    }

    private void GenerarMovimientoCamara()
    {
        float aleatorioX = Random.Range(-vibracionX,vibracionX);
        float aleatorioY = Random.Range(-vibracionY, vibracionY);

        Vector2 veclocidad = new(aleatorioX, aleatorioY);
        cinemachine.GenerateImpulse(veclocidad);
    }
}
