using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private UIDialogoController uiController;
    public void ActivarTexto()
    {
        if (uiController != null)
            uiController.MostrarTextoDialogo(); 
    }
    public void DesactivarBox()
    {
        if (uiController != null)
            uiController.DesactivarDialogoBox();
    }
}
