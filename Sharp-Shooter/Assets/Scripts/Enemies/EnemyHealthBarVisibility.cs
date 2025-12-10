using UnityEngine;

public class EnemyHealthBarVisibility : MonoBehaviour
{
    [SerializeField] Canvas healthBarCanvas;
    Transform playerCamera;
    float maxDetectionDistance = 50f;
    float viewAngleThreshold = 12f;

    void Start()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.enabled = false;

        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (playerCamera == null) return;

        Vector3 directionToEnemy = transform.position - playerCamera.position;
        float distance = directionToEnemy.magnitude;

        // Distance check
        if (distance > maxDetectionDistance)
        {
            healthBarCanvas.enabled = false;
            return;
        }

        // Angle check
        float angle = Vector3.Angle(playerCamera.forward, directionToEnemy.normalized);

        if (angle < viewAngleThreshold)
        {
            healthBarCanvas.enabled = true;
        }
        else
        {
            healthBarCanvas.enabled = false;
        }
    }
}