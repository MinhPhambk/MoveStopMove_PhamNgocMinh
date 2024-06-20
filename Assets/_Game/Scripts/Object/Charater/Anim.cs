using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField] private Character character;

    public Character GetCharacter() { return character; }
}
