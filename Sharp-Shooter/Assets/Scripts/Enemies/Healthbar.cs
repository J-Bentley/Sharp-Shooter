using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Image fillImage;
    float maxHealth;

    void Start()
    {
        SetMaxHealth(enemyHealth.startingHealth);
        maxHealth = enemyHealth.startingHealth;
    }

    public void SetHealth(int amount)
    {
        slider.value = amount;
        UpdateColor(amount);
    }

    void SetMaxHealth(int amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
    }

    void UpdateColor(float amount) {
        float halfHealth = maxHealth / 2f;

        if (amount > halfHealth) {
            float t = (maxHealth - amount) / halfHealth;
            t = Mathf.Pow(t, 0.5f);
            fillImage.color = Color.Lerp(Color.green, Color.yellow, t);
        } else {
            float t = amount / halfHealth;
            t = Mathf.Pow(t, 2f); 
            fillImage.color = Color.Lerp(Color.red, Color.yellow, t);
        }
    }
}
