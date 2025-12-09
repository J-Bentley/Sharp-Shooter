using UnityEngine;

public abstract class Pickup : MonoBehaviour {

    [SerializeField] float rotationSpeed = 100f;

    AudioSource audioSource;

    const string PLAYER_STRING = "Player";

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // TODO: do this betterer

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(PLAYER_STRING)) {
            
            // Stops Trigger from being ran more than once
            this.GetComponent<Collider>().enabled = false;

            // Destroy the model of the pickup but keeps the parent alive where audio source and script are
            Transform firstChild = transform.GetChild(0);
            Destroy(firstChild.gameObject);

            // Gives ammo or gun
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);

            // Play audio source and destroy parent when audio is done
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }

    void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
