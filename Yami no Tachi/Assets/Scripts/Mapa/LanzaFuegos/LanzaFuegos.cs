using System.Collections;
using UnityEngine;

public class LanzaFuegos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioClip disparoSFX;
    [SerializeField] private AudioSource accionesAudioSource;

    [Header("Configuración del disparo")]
    public ObjectPool objectPool;
    public Transform puntoSalida;
    public float tiempoEntreDisparos = 5f;

    private Animator anim;
    private Coroutine rutinaDisparo;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        rutinaDisparo = StartCoroutine(ControlarDisparo());
        accionesAudioSource.volume = 0.3f;
    }
    private void OnDisable()
    {
        if (rutinaDisparo != null)
            StopCoroutine(rutinaDisparo);
    }

    private IEnumerator ControlarDisparo()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreDisparos);
            anim.SetTrigger("lanzar");
            accionesAudioSource.PlayOneShot(disparoSFX);
        }
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
