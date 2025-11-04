using UnityEngine;

[CreateAssetMenu(menuName = "Data/Entity Data")]
public class EntityData : ScriptableObject
{
    public enum EntityType { Player, Enemy }

    [Header("General Settings")]

    public EntityType Type;
    public int MaxHealth;
    public LayerMask TargetLayer;


}
