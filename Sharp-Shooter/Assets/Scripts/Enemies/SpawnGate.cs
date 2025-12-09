using System.Collections;
using StarterAssets;
using UnityEngine;

public class SpawnGate : MonoBehaviour {

    [SerializeField] int spawnTime;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] ParticleSystem spawnGateVFX;

    PlayerHealth player;
    AudioSource audiosource;

    void Awake(){
        audiosource = GetComponent<AudioSource>();
    }

    void Start() {
        player = FindFirstObjectByType<PlayerHealth>();
        if (player != null) {
           StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine() {
        while (player != null) {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, Quaternion.identity);
            spawnGateVFX.Play();
            audiosource.Play();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
