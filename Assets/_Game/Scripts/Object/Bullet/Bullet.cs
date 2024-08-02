using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Bullet : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    private Character character;
    private Vector3 direction = Vector3.zero;
    
    [SerializeField] protected LayerMask wallBorder;

    protected bool stopped = false;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!stopped)
        {
            if (direction != Vector3.zero)
            {
                if (Physics.Raycast(TF.position, Vector3.down, out RaycastHit hit, 4f, wallBorder))
                {
                    OnDespawn();
                }
                else
                {
                    TF.position = speed * Time.deltaTime * direction.normalized + TF.position;
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character enemy = other.GetComponent<Anim>().GetCharacter();
            if (enemy != character)
            {
                enemy.Died();
                OnDespawn();
            }
        }

        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Character enemy = other.GetComponent<Anim>().GetCharacter();
            if (enemy != character)
            {
                Player player = (Player)enemy;
                player.Died();
                OnDespawn();
            }
        }    
    }

    public void OnDespawn()
    {
        character.SetActiveWeapon(false);
        SimplePool.Despawn(this);
    }

    public virtual void OnInit(Vector3 d, Character c)
    {
        direction = d;
        character = c;
        character.SetActiveWeapon(true);
    }
}
