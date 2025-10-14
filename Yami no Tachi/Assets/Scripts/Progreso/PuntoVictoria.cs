using UnityEngine;

public class PuntoVictoria : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SistemaProgresion.Instancia.MarcarVictoria();
        }
    }
}
