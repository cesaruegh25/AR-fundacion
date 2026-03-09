using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PokeballData", menuName = "Scriptable Objects/PokeballData")]
public class PokeballData : ScriptableObject
{
    string name;
    int captureRate;
    Sprite sprite;
}
