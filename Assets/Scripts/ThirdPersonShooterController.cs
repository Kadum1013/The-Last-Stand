using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterRotation))]
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimVirtualCamera;
    [SerializeField] CharacterRotation characterRotation;
    [SerializeField] float normalSensitivity;
    [SerializeField] float aimSensitivity;
    [SerializeField] bool isAiming;

    private void Update()
    {
        if(isAiming)
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
