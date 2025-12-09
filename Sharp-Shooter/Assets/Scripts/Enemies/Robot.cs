using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour {

    FirstPersonController player;
    NavMeshAgent agent;
    bool isDead = false;
    float repathTimer = 0.2f; // required init
    
    const string PLAYER_STRING = "Player";

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {

        if (!player)
        {
            VictoryDance();
        } 
        else
        {
            SetPath();   
        }

    }

    void OnTriggerEnter(Collider other) {
        if (isDead) return;

        if (other.CompareTag(PLAYER_STRING))
        {
            isDead = true;
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }

    void VictoryDance()
    {
        // Stop and spin when player dies
        agent.isStopped = true;
        agent.ResetPath();
        transform.Rotate(0f, 100f * Time.deltaTime, 0f);
    }

    void SetPath()
    {
        // Sets agents destination 5 times a second instead of every frame
        repathTimer -= Time.deltaTime;

        if (repathTimer <= 0f)
        {
            agent.SetDestination(player.transform.position);
            repathTimer = 0.2f; // Resets timer
        }
    }
}
