using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MaterialObject
{
    [SerializeField] private WeaponType weaponType = WeaponType.Arrow;
    
    [SerializeField] protected Transform attackPosition;
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject animObject;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected GameObject attackChoosed;

    protected HashSet<GameObject> enemies = new HashSet<GameObject>();
    protected bool stopped = false;
    protected bool isRunning = false;
    protected bool isActiveWeapon = false;
    protected string currentAnim;
    protected GameObject eTarget = null;

    private void Update()
    {
        Move();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ATTACKRANGE))
        {
            AttackArea attackArea = other.GetComponent<AttackArea>();
            attackArea.RemoveEnemy(this.gameObject);
        }
    }

    private void AttackEnemy(Vector3 enemyPos, PoolType typeAttack = PoolType.Arrow)
    {
        transform.LookAt(enemyPos);
        Vector3 direction = new Vector3(enemyPos.x - TF.position.x, 0, enemyPos.z - TF.position.z);
        Bullet b = SimplePool.Spawn<Arrow>(typeAttack, attackPosition.position, Quaternion.identity);
        b.OnInit(direction, this);
    }

    protected virtual void Move()
    {

    }

    protected Vector3 NextMove(Vector3 newPosition)
    {
        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit hit, 2f, groundLayer))
        {
            return hit.point;
        }

        return TF.position;
    }

    protected void SetAttackTarget(GameObject e = null)
    {
        eTarget = e;
    }

    public void SetActiveChoosed(bool status)
    {
        attackChoosed.SetActive(status);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                anim.ResetTrigger(currentAnim);
            }

            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeWeapon(WeaponType type)
    {
        this.weaponType = type;
    }

    public void Attack()
    {
        if (!isRunning)
        {
            if (eTarget != null && !isActiveWeapon)
            {
                AttackEnemy(eTarget.transform.position);
            }
        }    
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public GameObject GetAnimObject()
    {
        return this.animObject;
    }    

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        enemies.Remove(enemy.GetComponent<Character>().GetAnimObject());
    }

    public void SetActiveWeapon(bool status)
    {
        isActiveWeapon = status;
    }    

    public virtual void Died()
    {
        ChangeAnim(Constant.ANIM_DIE);

        if (this is Bot)
        {
            SimplePool.Despawn(this);
        }
    }    
}
