using StarterAssets;
using UnityEngine;

public class CameraTilt : MonoBehaviour {

    [SerializeField] Transform playerTransform;
    [SerializeField] float tiltAngle = 10f;
    [SerializeField] float tiltSpeed = 5f;
    [SerializeField] float returnSpeed = 2f;

    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    Vector3 previousPosition;

    void Awake() {
        previousPosition = playerTransform.position;
    }

    void Update() {
        Vector3 currentPosition = playerTransform.position;
        Vector3 movementDelta = currentPosition - previousPosition;
        previousPosition = currentPosition;

        bool isMoving = starterAssetsInputs.move.sqrMagnitude > 0.001f;
        Quaternion targetRotation = Quaternion.identity;

        if (isMoving && movementDelta.sqrMagnitude > 0.0001f) {
            Vector3 worldDirection = movementDelta.normalized;

            Vector3 localMovementDirection =
                playerTransform.InverseTransformDirection(worldDirection);

            float targetTiltZ = -localMovementDirection.x * tiltAngle;

            targetRotation = Quaternion.Euler(0f, 0f, targetTiltZ);
        }

        float speed = isMoving ? tiltSpeed : returnSpeed;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * speed);
    }
}