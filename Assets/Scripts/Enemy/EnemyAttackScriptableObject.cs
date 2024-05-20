using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackScriptableObject", menuName = "ScriptableObjects/EnemyAttack")]
public class EnemyAttackScriptableObject : ScriptableObject
{
    public float attackDamage = 20f;
    public float attackRange = 10f;
    //public AudioClip attacksound;
}
