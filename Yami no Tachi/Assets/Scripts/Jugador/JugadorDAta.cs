using UnityEngine;

[CreateAssetMenu(fileName = "JugadorData", menuName = "Datos/Jugador")]
public class JugadorDAta : ScriptableObject
{
    public bool estaAtacando = false;
    public bool enSuelo = true;
    public bool estaEnPared = false;
    public bool puedeSaltar => enSuelo || estaEnPared;

    [Header("Vida")]
    public int corazones = 3;

    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 5f;

    [Header("Ataque")]
    public int dano = 1;
    public GameObject slashPrefab;

    [Header("Escalar Paredes")]
    public float velocidadDesliz = 1.5f;
    public float fuerzaSaltoPared = 7f;
    public float impulsoHorizontalPared = 5f;
    public float tiempoAgarrePared = 0.2f;
    public float gravedadNormal = 3f;
}
