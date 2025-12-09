using StarterAssets;
using UnityEngine;

public class LookSway : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float swayMultiplier;
    [SerializeField] float swaySmoothing;

    [Header("References")]
    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    void Update()
    {
        // Mouse input
        float swayX = -starterAssetsInputs.look.y * swayMultiplier; // pitch
        float swayY = starterAssetsInputs.look.x * swayMultiplier;  // yaw
        float swayZ = -starterAssetsInputs.move.x * 0.5f * swayMultiplier; // roll from strafing

        // Combine rotations
        Quaternion targetRot = Quaternion.Euler(swayX, swayY, swayZ);

        // Smoothly interpolate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, swaySmoothing * Time.deltaTime);

    }
}
