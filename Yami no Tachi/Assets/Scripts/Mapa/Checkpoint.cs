using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.Instance.SetCheckpoint(transform);
            Debug.Log("Checkpoint activado en: " + transform.position);
        }
    }
}
