using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private Collider2D mainBounds;
    [SerializeField] private Collider2D secondBounds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (confiner.BoundingShape2D == mainBounds)
            {
                confiner.BoundingShape2D = secondBounds;
            }
            else
            {
                confiner.BoundingShape2D = mainBounds;
            }
            StartCoroutine(RebuildConfinerNextFrame());
        }
    }

    private IEnumerator RebuildConfinerNextFrame()
    {
        yield return null;
        confiner.enabled = false; 
        confiner.enabled = true;
    }
}
