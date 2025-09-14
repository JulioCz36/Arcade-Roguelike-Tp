using UnityEngine;

public class scr_mover : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] float velocidad = 5f;

    private float moverHorizontal;
    private Rigidbody2D mi_rb2d;
    private Animator mi_animator;

    private void OnEnable()
    {
        mi_rb2d = GetComponent<Rigidbody2D>();
        mi_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moverHorizontal = Input.GetAxisRaw("Horizontal");

        if (moverHorizontal != 0)
            transform.localScale = new Vector3(Mathf.Sign(moverHorizontal), 1, 1);

        mi_animator.SetBool("isIdle", Mathf.Abs(moverHorizontal) < 0.1f);
    }

    private void FixedUpdate()
    {
        Vector2 nuevaVel = mi_rb2d.linearVelocity;
        nuevaVel.x = moverHorizontal * velocidad;
        mi_rb2d.linearVelocity = nuevaVel;
    }
}
