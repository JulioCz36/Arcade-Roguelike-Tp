using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Jump jumpScript;

    private void Start()
    {
        jumpScript = GetComponentInParent<Jump>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            jumpScript.SetPuedeSaltar(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            jumpScript.SetPuedeSaltar(false);
        }
    }
}
