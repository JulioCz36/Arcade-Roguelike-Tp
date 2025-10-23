using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CartelInteractivo : MonoBehaviour
{
    [SerializeField] private string[] lineasDialogo; 
    [SerializeField] private GameObject uiDialogo;
    [SerializeField] private Text textoDialogo;

    public UnityEvent onAbrirDialogo;
    public UnityEvent onCerrarDialogo;

    private bool jugadorCerca = false;
    private bool mostrandoDialogo = false;
    private int indiceDialogo = 0;

    private void Start()
    {
        uiDialogo.SetActive(false);
        textoDialogo.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            CerrarDialogo();
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetButtonDown("Vertical"))
        {
            if (!mostrandoDialogo)
            {
                MostrarDialogo();
            }
        }
        if (mostrandoDialogo && Input.GetButtonDown("Fire1"))
        {
            AvanzarDialogo();
        }
    }

    private void MostrarDialogo()
    {
        if (uiDialogo == null || textoDialogo == null) return;

        mostrandoDialogo = true;
        indiceDialogo = 0;

        uiDialogo.SetActive(true);
        textoDialogo.text = lineasDialogo[indiceDialogo];

        onAbrirDialogo?.Invoke();
    }

    private void AvanzarDialogo()
    {
        indiceDialogo++;
        if (indiceDialogo >= lineasDialogo.Length)
        {
            CerrarDialogo();
            return;
        }

        textoDialogo.text = lineasDialogo[indiceDialogo];
    }

    private void CerrarDialogo()
    {
        mostrandoDialogo = false;
        textoDialogo.gameObject.SetActive(false);

        onCerrarDialogo?.Invoke();
    }
    public void MostrarTextoDialogo()
    {
         textoDialogo.gameObject.SetActive(true);
    }

    public void DesactivarUI()
    {
       uiDialogo.SetActive(false);
    }

}
