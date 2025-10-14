using Unity.VisualScripting;
using UnityEngine;

public class Enemigo_1 : MonoBehaviour
{
    [Header("Detección y Ataque")]
    public Transform rayCast;
    public LayerMask layerMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;

    [Header("Patrulla")]
    [SerializeField] float patrullaVel = 2f;
    [SerializeField] float tiempoCambioDir = 3f;
    private float contadorDir;
    private bool patrullandoDerecha = true;

    [Header("Vida")]
    [SerializeField] int vidaMax = 3;
    private int vidaActual;

    private RaycastHit2D hit;
    private GameObject target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool colldown;
    private float intTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        intTimer = timer;
        contadorDir = tiempoCambioDir;
        vidaActual = vidaMax;
    }

    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, layerMask);
            RaycastDebugger();
        }

        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else
        {
            inRange = false;
            attackMode = false;
        }
        if (!inRange)
        {
            StopAttack();
            Patrullar();
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && colldown == false)
        {
            Attack();
        }

        if (colldown)
        {
            Cooldown();
            animator.SetBool("attack", false);
        }
    }

    void Move()
    {
        animator.SetBool("walk", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("atk"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    void Cooldown() { 
    
        timer -= Time.deltaTime;
        if (timer <= 0 && colldown && attackMode)
        {
            colldown = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        timer = intTimer;
        attackMode = false;

        animator.SetBool("attack", false);
    }

    void Patrullar()
    {
        animator.SetBool("walk", true);

        float dir = patrullandoDerecha ? 1f : -1f;
        transform.Translate(Vector2.right * dir * patrullaVel * Time.deltaTime);

        contadorDir -= Time.deltaTime;
        if (contadorDir <= 0f)
        {
            patrullandoDerecha = !patrullandoDerecha;
            transform.localScale = new Vector3(patrullandoDerecha ? 3 : -3, 3, 3);
            contadorDir = tiempoCambioDir;
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        animator.SetBool("walk", false);
        animator.SetBool("attack", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            inRange = true;
        }
    }

    void RaycastDebugger()
    {

        if (distance > attackDistance)
        {

            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);

        }
    }

    void TriggerCooling()
    {
        colldown = true;
    }
}
