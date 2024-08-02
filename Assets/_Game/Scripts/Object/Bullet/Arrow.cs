using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    public override void OnInit(Vector3 direction, Character c)
    {
        base.OnInit(direction, c);
        transform.rotation = Quaternion.Euler(90, 0, Mathf.Atan2(direction.x, -direction.z) * Mathf.Rad2Deg);
    }
}
