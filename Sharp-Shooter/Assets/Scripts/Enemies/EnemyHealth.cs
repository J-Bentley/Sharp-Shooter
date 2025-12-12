using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] public int startingHealth;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject destroyedObjectPrefab;
    [SerializeField] GameObject droppedAmmoPickupPrefab;
    [SerializeField] Healthbar healthbar;
    [Range (0,1)]
    [SerializeField] float chanceOfAmmoDrop; // 0.7 = 70 percent chance of ammo dropping

    GameManager gameManager;
    int currentHealth;
    
    
    void Awake() {
        currentHealth = startingHealth;
    }

    void Start() { 
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesRemaining(1);
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;

        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0){
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Instantiate(destroyedObjectPrefab, transform.position, transform.rotation);
        gameManager.AdjustEnemiesRemaining(-1);
        ChanceOfAmmoDrop();
        Destroy(this.gameObject);
    }

    void ChanceOfAmmoDrop()
    {
        float randomFloat = Random.Range(0, 1);
        if (randomFloat < chanceOfAmmoDrop)
        {
            Instantiate(droppedAmmoPickupPrefab, transform.position, Quaternion.identity);
        }
    }
}
