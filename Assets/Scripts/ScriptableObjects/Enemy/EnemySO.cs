using UnityEngine;

namespace ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemySO : ScriptableObject
    {
        public new string name;
        public float attack;
        public float health;
        public float moveSpeed;

        public Sprite enemySprite;
    }
}
