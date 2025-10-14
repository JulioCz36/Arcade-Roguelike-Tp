using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activado)
        {
            activado = true;
            RespawnManager.Instancia.ActualizarCheckpoint(transform);
            Debug.Log("Checkpoint activado en: " + transform.position);
        }
    }
}
