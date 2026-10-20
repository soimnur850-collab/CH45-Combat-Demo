using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public Ch10Health targetHealth;

    public Transform targetAnchor;
    public Vector3 offset = Vector3.zero;
    public Camera worldCamera;

    void Start()
    {
        if (worldCamera == null)
        {
            worldCamera = Camera.main;
        }
    }

    void Update()
    {
        if (targetHealth != null && healthSlider != null)
        {
            healthSlider.value = targetHealth.currentHealth;
        }

        if (targetAnchor != null && worldCamera != null)
        {
            Vector3 screenPosition = worldCamera.WorldToScreenPoint(
                targetAnchor.position + offset
            );
            transform.position = screenPosition;
        }
    }
}