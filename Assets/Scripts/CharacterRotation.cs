using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] Camera camera;
    

    // Face the direction the camera is facing
    private void Update()
    {
        Vector3 camForward = camera.transform.forward;

        if(camForward.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(camForward.normalized);

        }
    }
}
