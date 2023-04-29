using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Elemental Color")]
public class ElementalColor : ScriptableObject
{
    public Material ballMaterial;
    public string displayName;
    public Color color;
}
