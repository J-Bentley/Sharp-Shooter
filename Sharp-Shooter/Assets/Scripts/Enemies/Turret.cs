using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float fireRate;
    [SerializeField] int damage;
    [SerializeField] ParticleSystem muzzleflash;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask lineOfSightMask;

    AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        if (playerTargetPoint != null)
        {
            Vector3 directionToPlayer = playerTargetPoint.position - turretHead.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            turretHead.rotation = Quaternion.Lerp(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator FireRoutine() {
        while (true) {

            if (playerTargetPoint != null) {

                yield return new WaitForSeconds(fireRate);

                // Check line of sight
                Vector3 direction = playerTargetPoint.position - projectileSpawnPoint.position;
                Ray ray = new Ray(projectileSpawnPoint.position, direction.normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, direction.magnitude, lineOfSightMask)) {
                    if (hit.transform == playerTargetPoint || hit.collider.CompareTag("Player"))
                    {
                        // Fire
                        Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
                        newProjectile.transform.LookAt(playerTargetPoint);
                        newProjectile.Init(damage);
                        muzzleflash.Play();
                        audioSource.Play();
                    }
                }
            }
        }
    }
}