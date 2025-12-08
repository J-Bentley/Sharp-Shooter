using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour {

    FirstPersonController player;
    NavMeshAgent agent;
    bool isDead = false;
    float repathTimer;

    const string PLAYER_STRING = "Player";

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start () {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update() {

        if (!player) return;

        repathTimer -=Time.deltaTime;
        
        if (repathTimer <= 0f) {
            agent.SetDestination(player.transform.position);
            repathTimer = 0.2f; // 5 times per second
        }
    }

    void OnTriggerEnter(Collider other) {
        if(isDead) return;

        if(other.CompareTag(PLAYER_STRING)) {
            isDead = true;
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
