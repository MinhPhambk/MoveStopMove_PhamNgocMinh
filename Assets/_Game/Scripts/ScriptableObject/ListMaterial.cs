using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ListMaterial", menuName = "ScriptableObjects/ListMaterial", order = 1)]
public class ListMaterial : ScriptableObject
{
    [SerializeField] private Material[] pantMaterials;

    public Material GetMaterial(Enum value)
    {
        return pantMaterials[(int)((PantType)value)];
    }
}