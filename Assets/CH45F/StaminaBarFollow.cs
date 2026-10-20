using UnityEngine;

public class StaminaBarFollow : MonoBehaviour
{
    public Transform targetAnchor;
    public Camera worldCamera;

    void LateUpdate()
    {
        if (targetAnchor == null || worldCamera == null)
            return;

        Vector3 screenPosition =
            worldCamera.WorldToScreenPoint(targetAnchor.position);

        if (screenPosition.z <= 0)
            return;

        transform.position = screenPosition;
    }
}