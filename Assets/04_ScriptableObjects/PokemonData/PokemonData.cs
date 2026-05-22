using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PokemonData", menuName = "Scriptable Objects/PokemonData")]
public class PokemonData : ScriptableObject
{
    string name;
    int maxHP;
    GameObject model;
    int baseCaptureRate;
}
