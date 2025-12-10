using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class Explosion : MonoBehaviour {

    [SerializeField] float radius;
    [SerializeField] int damage;
    
    const string PLAYER_STRING = "Player";

    CinemachineImpulseSource impulseSource;

    void Awake() {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start() {
        Explode();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode() {
        
        impulseSource.GenerateImpulse();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        HashSet<object> damaged = new HashSet<object>();

        // Enemy Explosion: Damages player and crates. Does not damage barrels.

        foreach (Collider hitCollider in hitColliders)
        {

            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            DestroyableObject destroyableObject = hitCollider.GetComponent<DestroyableObject>();

            if (playerHealth != null && !damaged.Contains(playerHealth))
            {
                playerHealth.TakeDamage(damage);
                damaged.Add(playerHealth);
            }
            
            if (destroyableObject != null && !damaged.Contains(destroyableObject))
            {
                destroyableObject.TakeDamage(damage);
                damaged.Add(destroyableObject);
            }
        }
    }
}
