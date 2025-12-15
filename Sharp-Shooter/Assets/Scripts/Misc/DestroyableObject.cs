using System.Collections;
using UnityEngine;

public class DestroyableObject : MonoBehaviour {

    // Attach to crates and barrels

    [SerializeField] int startingHealth;
    [SerializeField] GameObject destroyedObjectPrefab;
    [SerializeField] bool isExplodingBarrel;
    [SerializeField] float explosionDelay;

    int currentHealth;
    ParticleSystem ps;

    void Awake()
    {
        currentHealth = startingHealth;
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (isExplodingBarrel)
            {
                StartCoroutine(Ignition());
            }
            else
            {
                DestroySelf();
            }
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
        Instantiate(destroyedObjectPrefab, transform.position, transform.rotation);
    }

    IEnumerator Ignition()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(explosionDelay);
        DestroySelf();
    }
}
