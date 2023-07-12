using UnityEngine;

namespace ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Player")]
    public class PlayerSO : ScriptableObject
    {
        public new string name;
        public float health;
        public float moveSpeed;
        public float defaultWeaponDamage;
    }
}
