using UnityEngine;

public class scr_mover : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float velocidad = 5f;


    private float moverHorizontal;
    private float moverVertical;
    private Vector2 direccion;

    private Rigidbody2D mi_rb2d;

    private Animator mi_animator;

    // Codigo que es ejecutado cuand el objeto se activa en el nivel
    private void OnEnable()
    {
        mi_rb2d = GetComponent<Rigidbody2D>();
        mi_animator = GetComponent<Animator>();
    }

    // Codigo ejecutado en cada frame del juego
    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");
        direccion = new Vector2(moverHorizontal, moverVertical);


        // Normalizar si el jugador se mueve en diagonal
        if (direccion.magnitude > 1)
        {
            direccion.Normalize();
        }

        mi_animator.SetFloat("movimientoX", moverHorizontal);
        mi_animator.SetFloat("movimientoY", moverVertical);

        if (moverVertical != 0 || moverHorizontal != 0){
            mi_animator.SetFloat("ultimoX", moverHorizontal);
            mi_animator.SetFloat("ultimoY", moverVertical);

            mi_animator.SetBool("isIdle", false);
        }else{
            mi_animator.SetBool("isIdle", true);
        }

        if (moverHorizontal != 0)
        {
            transform.localScale = new Vector3(4 * Mathf.Sign(moverHorizontal), 4, 1);
        }
    }

    private void FixedUpdate(){
        mi_rb2d.MovePosition(mi_rb2d.position + direccion * (velocidad * Time.fixedDeltaTime));
    }

    public Vector2 GetUltimaDireccion(){
        return direccion;
    }
}
