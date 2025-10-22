using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LayerMask capaCheckpoint;
    public float radioDeteccion = 0.5f;

    private void Update()
    {
        Collider2D checkpoint = Physics2D.OverlapCircle(transform.position, radioDeteccion, capaCheckpoint);
        if (checkpoint != null)
        {
            RespawnManager.Instance.SetCheckpoint(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
