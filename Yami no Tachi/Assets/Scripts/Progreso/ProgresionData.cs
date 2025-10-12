using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProgresionData", menuName = "Datos/Progresion")]
public class ProgresionData : ScriptableObject
{
    [Header("Fragmentos requeridos")]
    public List<string> fragmentosNecesarios = new List<string>();
}
