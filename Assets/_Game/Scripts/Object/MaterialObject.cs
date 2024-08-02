using System;
using UnityEngine;

public class MaterialObject : GameUnit
{
    [SerializeField] private Renderer render;
    [SerializeField] private ListMaterial mats;
    
    public Enum material;

    public void ChangeMaterial(Enum matType)
    {
        if (matType != material)
        {
            material = matType;
            render.material = mats.GetMaterial(material);   
        }
    }
}