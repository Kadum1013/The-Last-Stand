using UnityEngine;


[CreateAssetMenu(menuName ="Combat/Attack")]
public class AttackData : ScriptableObject
{
    public AnimationClip clip;
    public float damageMultiplier = 1f;
    [Range(0, 1)] public float hitStart = 0.25f, hitEnd = 0.5f; // hitbox active
    [Range(0, 1)] public float comboOpen = 0.5f, comboClose = 0.9f; // next input accepted
    public float lungeDistance;
}
