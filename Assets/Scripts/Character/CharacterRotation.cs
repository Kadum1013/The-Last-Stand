using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] Camera camera;

    public void UpdateCharacterRotation()
    {
        Vector3 camForward = camera.transform.forward;
        camForward.y = 0f;

        
        if (camForward.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(camForward.normalized);
        }
    }
}
