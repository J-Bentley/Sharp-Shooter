using UnityEngine;

public class HealthpackPickup : MonoBehaviour {

    [SerializeField] int amount;
    [SerializeField] float rotationSpeed = 100f;

    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] AudioSource audioSource;

    const string PLAYER_STRING = "Player";

    void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }


    void OnTriggerEnter(Collider other) {
        if(other.CompareTag(PLAYER_STRING)) {
            if (playerHealth.currentHealth != playerHealth.startingHealth) {
                this.GetComponent<Collider>().enabled = false;
                playerHealth.GetHealth(amount);
                audioSource.Play();
                Destroy(gameObject, audioSource.clip.length);
            }
        }
    }
}
