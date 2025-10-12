using System.Collections.Generic;
using UnityEngine;

public class SistemaProgresion : MonoBehaviour
{
    public static SistemaProgresion Instancia { get; private set; }

    [Header("Datos de progresión")]
    [SerializeField] private ProgresionData progresionData;

    private List<string> fragmentosRecolectados = new List<string>();

    public delegate void FragmentoRecolectadoHandler(string nombreFragmento);
    public event FragmentoRecolectadoHandler OnFragmentoRecolectado;

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

    public void AgregarFragmento(string nombreFragmento)
    {
        if (!fragmentosRecolectados.Contains(nombreFragmento))
        {
            fragmentosRecolectados.Add(nombreFragmento);
            Debug.Log($"Fragmento {nombreFragmento} recolectado!");

            OnFragmentoRecolectado?.Invoke(nombreFragmento);

            if (VerificarProgreso())
            {
                Debug.Log("Todos los fragmentos recolectados");
            }
        }
    }

    public bool VerificarProgreso()
    {
        foreach (string fragmento in progresionData.fragmentosNecesarios)
        {
            if (!fragmentosRecolectados.Contains(fragmento))
                return false;
        }
        return true;
    }
}
