using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public Slider staminaSlider;

    public float maxStamina = 100f;
    public float currentStamina = 100f;

    void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaBar();
    }

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        UpdateStaminaBar();
    }

    void UpdateStaminaBar()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
}
