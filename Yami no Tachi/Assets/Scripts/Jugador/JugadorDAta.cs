using UnityEngine;

[CreateAssetMenu(fileName = "JugadorData", menuName = "Datos/Jugador")]
public class JugadorDAta : ScriptableObject
{
    [Header("Vida")]
    public int corazones = 3;

    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 5f;

    [Header("Ataque")]
    public int dano = 1;
    public GameObject slashPrefab;
}
