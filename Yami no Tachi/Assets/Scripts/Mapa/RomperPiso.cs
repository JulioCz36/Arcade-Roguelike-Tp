using UnityEngine;

public class RomperPiso : MonoBehaviour
{
    [SerializeField] private GameObject floorToBreak;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            Destroy(floorToBreak);
            Destroy(gameObject);
        }
    }
}
