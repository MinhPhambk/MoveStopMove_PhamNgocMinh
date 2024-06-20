using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private Character character;

    private void LateUpdate()
    {
        if (attackRange > 0)
        {
            transform.localScale = new Vector3(attackRange, attackRange, transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character enemy = other.gameObject.GetComponent<Anim>().GetCharacter();
            character.AddEnemy(enemy);
            character.AttackEnemy(other.gameObject.transform.position);
        }
    }

    public void RemoveEnemy(Character enemy)
    {
        character.SetActiveChoosed(false);
        character.RemoveEnemy(enemy);
    }
}