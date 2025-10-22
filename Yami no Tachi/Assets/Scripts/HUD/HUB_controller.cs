using UnityEngine;
using UnityEngine.UI;

public class HUB_controller : MonoBehaviour {

    [SerializeField] GameObject iconoVida;
    [SerializeField] GameObject contenedorIconosVida;
    [SerializeField] Sprite corazonLleno;
    [SerializeField] Sprite corazonVacio;

    private void Start() {
        actualizarVidasHUD(3);
    }

    public void actualizarVidasHUD(int vidas) {
        if (EstaVacioContenedor()) {
            CargarContenedor(vidas);
            return;
        }
        if (CantidadIconosVida() > vidas) {
            for (int i = contenedorIconosVida.transform.childCount; i > vidas; i--) {
                EliminarUltimoIcono();
            }
        }
        else {
            CrearIcono();
        }
    } 

    private bool EstaVacioContenedor() {
        return contenedorIconosVida.transform.childCount == 0;
    }

    private void CargarContenedor(int vidas) {
        for (int i = 0; i < vidas; i++) {
            CrearIcono();
        }
    }

    private int CantidadIconosVida() {
        return contenedorIconosVida.transform.childCount;
    }

    private void EliminarUltimoIcono() {
        Transform contenedor = contenedorIconosVida.transform;
       GameObject.Destroy(contenedor.GetChild(contenedor.childCount - 1).gameObject);
    }
    private void CrearIcono() {
        Instantiate(iconoVida, contenedorIconosVida.transform);
    }
}
