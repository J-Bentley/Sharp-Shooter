using StarterAssets;
using UnityEngine;

public class CameraBob : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] float walkingBobbingSpeed;
    [SerializeField] float runningBobbingSpeed;
    [SerializeField] float bobbingAmount;

    [Header("References")]
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    float defaultYPos;
    float timer;

    void Awake() {
        defaultYPos = transform.localPosition.y;
    }

    void Update() {
        if (firstPersonController.Grounded && starterAssetsInputs.move.sqrMagnitude > 0.001f) {

            float speed = starterAssetsInputs.sprint ? runningBobbingSpeed : walkingBobbingSpeed;

            timer += Time.deltaTime * speed;

            Vector3 pos = transform.localPosition;
            pos.y = defaultYPos + Mathf.Sin(timer) * bobbingAmount;
            transform.localPosition = pos;
        }
        else {
            timer = Mathf.Lerp(timer, 0f, Time.deltaTime * 5f);

            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, defaultYPos, Time.deltaTime * 10f);
            transform.localPosition = pos;
        }
    }
}