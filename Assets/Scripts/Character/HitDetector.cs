using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public void Hit(float dmg, BodyPart bodyPart)
    {
        Debug.Log($"Character was hit! Dmg:{dmg}.  Body Part: {bodyPart}");
    }
}
