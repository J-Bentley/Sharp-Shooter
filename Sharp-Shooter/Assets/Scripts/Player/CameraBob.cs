using StarterAssets;
using UnityEngine;

public class CameraBob : MonoBehaviour {

    // Moves camera in parabolic motion (U shaped curve)

    [Header("Settings")]
    [SerializeField] float walkingBobbingSpeed; // speed of bob
    [SerializeField] float runningBobbingSpeed;
    [SerializeField] float bobbingAmount; // size of the U shape
    [SerializeField] float moveThreshold; // how much does the player have to move for camera to bob

    [Header("References")]
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    float defaultYPos;
    float timer;

    void Awake() {
        defaultYPos = transform.localPosition.y;
    }

    void Update() {
        if (firstPersonController.Grounded && starterAssetsInputs.move.sqrMagnitude > moveThreshold) {

            // Uses differant variable if sprinting
            float speed = starterAssetsInputs.sprint ? runningBobbingSpeed : walkingBobbingSpeed;

            timer += Time.deltaTime * speed;

            Vector3 pos = transform.localPosition;
            pos.y = defaultYPos + Mathf.Sin(timer) * bobbingAmount;
            transform.localPosition = pos;
        }
        else {

            // smoothly reset camera to default pos when not moving

            timer = Mathf.Lerp(timer, 0f, Time.deltaTime * 5f);

            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, defaultYPos, Time.deltaTime * 10f);
            transform.localPosition = pos;
        }
    }
}