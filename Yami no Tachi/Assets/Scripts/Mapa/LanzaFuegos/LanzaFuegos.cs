using System.Collections;
using UnityEngine;

public class LanzaFuegos : MonoBehaviour
{
    [Header("Configuración del disparo")]
    public ObjectPool objectPool;
    public Transform puntoSalida;
    public float tiempoEntreDisparos = 5f;

    private bool puedeDisparar = true;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (puedeDisparar)
            StartCoroutine(ControlarDisparo());
    }

    private IEnumerator ControlarDisparo()
    {
        puedeDisparar = false;

        anim.SetTrigger("lanzar");

        yield return new WaitForSeconds(tiempoEntreDisparos);

        puedeDisparar = true;
    }
    public void Disparar()
    {
        GameObject fuego = objectPool.GetPooledObject();

        if (fuego != null)
        {
            fuego.transform.position = puntoSalida.position;
            fuego.transform.rotation = puntoSalida.rotation;
            fuego.SetActive(true);
        }
    }
}
