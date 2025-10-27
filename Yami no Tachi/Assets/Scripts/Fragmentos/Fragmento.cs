using UnityEngine;

public class Fragmento : MonoBehaviour
{
    [SerializeField] private string idFragmento;

    [Header("Referencias")]
    [SerializeField] private AudioClip upSFX;
    [SerializeField] private AudioSource accionesAudioSource;
    [SerializeField] private GameObject particula;

    private Animator animator;
    private bool recogido = false;

    private void OnEnable()
    { 
        animator = GetComponent<Animator>();
        accionesAudioSource.volume = 0.3f;
        particula.SetActive(false);
    }

    private void Update()
    {
        if (recogido) return;

        Vector3 camPos = Camera.main.transform.position;
        float distancia = Vector3.Distance(transform.position, camPos);

        if (distancia < 15f)
        {
            if (!particula.activeSelf)
                particula.SetActive(true);
        }
        else
        {
            if (particula.activeSelf)
                particula.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (recogido) return; 
        if (collision.CompareTag("Player"))
        {
            particula.SetActive(false);
            accionesAudioSource.PlayOneShot(upSFX);
            recogido = true;
            animator.SetTrigger("up");
            SistemaProgresion.Instancia.AgregarFragmento(idFragmento);
            StartCoroutine(EsperarAnimacionYDestruir());
        }
    }

    private System.Collections.IEnumerator EsperarAnimacionYDestruir()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}
