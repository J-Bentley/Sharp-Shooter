using UnityEngine;

public class Billboard : MonoBehaviour {
    private Camera mainCamera;
    [SerializeField] bool lockYAxis;

    void Start() {
        mainCamera = Camera.main;
    }

    void LateUpdate() {
        if (mainCamera != null) {
            transform.LookAt(mainCamera.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // only rotates Y axis, remove this line to have object rotate fully in 3D
        }
    }
}