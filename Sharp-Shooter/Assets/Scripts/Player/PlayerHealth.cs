using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [Range(1, 10)]
    [SerializeField] public int startingHealth;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    public int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    AudioSource[] audioSources;


    void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
        audioSources = GetComponents<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustShieldUI();
        audioSources[1].pitch = Random.Range(0.8f, 1.2f);
        audioSources[1].Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void GetHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        AdjustShieldUI();
    }

    void Death()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        starterAssetsInputs.SetCursorState(false);
        Destroy(this.gameObject);
    }

    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
                shieldBars[i].color = GetHealthColor(currentHealth);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    Color GetHealthColor(int healthAmount)
    {
        float halfHealth = startingHealth / 2f;

        if (healthAmount > halfHealth)
        {
            float t = (startingHealth - healthAmount) / halfHealth;
            t = Mathf.Pow(t, 0.5f);
            return Color.Lerp(Color.green, Color.yellow, t);
        }
        else
        {
            float t = healthAmount / halfHealth;
            t = Mathf.Pow(t, 2f); 
            return Color.Lerp(Color.red, Color.yellow, t);
        }
    }
}