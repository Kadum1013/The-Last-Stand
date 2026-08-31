using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterRotation))]
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimVirtualCamera;
    [SerializeField] CharacterRotation characterRotation;
    [SerializeField] float normalSensitivity;
    [SerializeField] float aimSensitivity;

    private void Update()
    {
        if(PlayerInput.instance.IsAiming)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            characterRotation.SetIsAiming(aimSensitivity, true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            characterRotation.SetIsAiming(normalSensitivity, false);
        }
    }
}
