using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
    public override void OnInit(Vector3 direction, Character c)
    {
        base.OnInit(direction, c);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
