using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public new string name;
    [HideInInspector] public float currentHealth;
    public float defaultHealth;
    public float moveSpeed;
    public float defaultDamage;
    public Sprite sprite;
    public AnimatorOverrideController animatorOverrideController;
}