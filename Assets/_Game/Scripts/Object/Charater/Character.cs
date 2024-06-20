using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MaterialObject
{
    [SerializeField] private WeaponType weaponType = WeaponType.Arrow;
    
    [SerializeField] protected Transform attackPosition;
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected GameObject attackChoosed;

    protected HashSet<Character> enemies = new HashSet<Character>();
    protected bool stopped = false;
    protected bool isSpawnedWeapon = false;
    protected string currentAnim;

    private void Awake()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ATTACKRANGE))
        {
            AttackArea attackArea = other.GetComponent<AttackArea>();
            attackArea.RemoveEnemy(this);
        }
    }

    protected Vector3 NextMove(Vector3 newPosition)
    {
        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit hit, 2f, groundLayer))
        {
            return hit.point;
        }

        return TF.position;
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

    public void AttackEnemy(Vector3 enemyPos)
    {
        Vector3 direction = (enemyPos - TF.position).normalized;
        SimplePool.Spawn<Arrow>(PoolType.Arrow, attackPosition.position, Quaternion.identity).OnInit(direction);
    }

    public void AddEnemy(Character enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Character enemy)
    {
        enemies.Remove(enemy);
    }
}
