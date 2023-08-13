using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int health = 10;
    public float speed = 3f;
    public EnemyAttackScriptableObject enemyAttackType;
}
