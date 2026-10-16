using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] RectTransform ringTransform;
    [SerializeField] Image ringImage;

    [Header("Precision Settings")]
    [SerializeField] float maxScale = 5f;
    [SerializeField] float minScale = 0.1f;

    // precision 1 = Full power.  0 = no power
    [SerializeField] private float currentPower = 0;
    bool isAiming = false;
    private void Update()
    {
        ringImage.enabled = isAiming;
        if (!isAiming)
        {
            currentPower = 0;
            return;
        }


        UpdateRingVisuals();
    }

    private void UpdateRingVisuals()
    {
        float currentScale = Mathf.Lerp(maxScale, minScale, currentPower);
        ringTransform.localScale = new Vector3(currentScale, currentScale, 1f);
    }
    public void ShrinkCircle(float value)
    {
        currentPower = Mathf.Clamp01(value);
    }

    public void UpdateIsAiming(bool aiming)
    {
        isAiming = aiming;
    }

    public float GetPowerMultiplier => currentPower;     // Returns 1 when Highest Power, closer to 0 less powers
}
