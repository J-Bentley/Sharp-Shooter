using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSensitivity : MonoBehaviour {

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] FirstPersonController firstPersonController;

    void Start() {
        if (firstPersonController && sensitivitySlider) {
            sensitivitySlider.value = firstPersonController.RotationSpeed; //sets slider to current value of rotation speed
            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged); // uses event listener to listen for slider change and sets OnSensChanged to slider value
        }
    }

    void OnDestroy() {
        if (sensitivitySlider) {
            sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged); //remove unused listener
        }
    }

    void OnSensitivityChanged(float value) {
        if (firstPersonController) {
            firstPersonController.RotationSpeed = value;
        }
    }
}