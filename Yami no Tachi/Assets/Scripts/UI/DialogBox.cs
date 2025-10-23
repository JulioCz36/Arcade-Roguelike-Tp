using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private CartelInteractivo cartel;
    public void ActivarTexto()
    {
        if (cartel != null)
            cartel.MostrarTextoDialogo(); 
    }
    public void DesactivarUI()
    {
        if (cartel != null)
            cartel.DesactivarUI();
    }
}
