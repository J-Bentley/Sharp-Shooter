using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour {

    [SerializeField] int damage;

    const string PLAYER_STRING = "Player";

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
        ExplodingBarrel explodingBarrel = other.GetComponent<ExplodingBarrel>();
        DestroyableObject destroyableObject = other.GetComponent<DestroyableObject>();
    
        enemyHealth?.TakeDamage(damage);
        playerHealth?.TakeDamage(damage);
        explodingBarrel?.TakeDamage(damage);
        destroyableObject?.TakeDamage(damage);
    }
}
