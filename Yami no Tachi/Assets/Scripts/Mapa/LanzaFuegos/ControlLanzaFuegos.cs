using UnityEngine;

public class ControlLanzaFuegos : MonoBehaviour
{
    [SerializeField] private LanzaFuegos[] lanzafuegos;
    void Start()
    {
        for (int i = 0; i < lanzafuegos.Length; i++)
        {
            lanzafuegos[i].enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var lf in lanzafuegos)
                lf.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var lf in lanzafuegos)
                lf.enabled = false;
        }
    }
}
