using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour {

    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;

    WeaponSO CurrentWeaponSO;
    Weapon currentWeapon;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;

    float timeSinceLastShot = 0f;
    float defaultFOV;
    float storedRotationSpeed;
    int currentAmmo;
    bool isZooming = false;

    const string SHOOT_STRING = "Shoot";

    void Awake() { 
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();

        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
    }

    void Start() {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(CurrentWeaponSO.MagazineSize);
    }

    void Update() {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount) {
        currentAmmo += amount;

        if (currentAmmo > CurrentWeaponSO.MagazineSize) {
            currentAmmo = CurrentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString("D2"); // display double digits
    }

    void HandleShoot() {

        timeSinceLastShot += Time.deltaTime;
        
        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= CurrentWeaponSO.FireRate && currentAmmo > 0) {
            currentWeapon.Shoot(CurrentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1); // adding a negative number is the same thing as subtracting it, neat!
        }

        if (!CurrentWeaponSO.IsAutomatic) {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom() {
        if (!CurrentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom) {

            if (!isZooming)
            {
                // stores rot speed then scales according to the current rot speed
                isZooming = true;
                storedRotationSpeed = firstPersonController.RotationSpeed;
                firstPersonController.MultiplyRotationSpeed(CurrentWeaponSO.ZoomRotationSpeedMultiplier);
            }

            // Sniper handling
            if (CurrentWeaponSO.name.Equals("Sniper")) {
                currentWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                zoomVignette.SetActive(true);
            }

            // Set FOV and rot speed of weaponSO
            weaponCamera.fieldOfView = CurrentWeaponSO.ZoomAmount;
            playerFollowCamera.m_Lens.FieldOfView = CurrentWeaponSO.ZoomAmount;

        }
        else {
            if (isZooming)
            {
                isZooming = false;
                
                // Return FOV and rot speed when not zoomed in
                playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
                weaponCamera.fieldOfView = defaultFOV;
                firstPersonController.SetRotationSpeed(storedRotationSpeed);

                // Sniper handling
                zoomVignette.SetActive(false);
                currentWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        }
    }

    public void SwitchWeapon(WeaponSO weaponSO) {
        if (currentWeapon) {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.CurrentWeaponSO = weaponSO;

        AdjustAmmo(CurrentWeaponSO.MagazineSize);
    }
}
