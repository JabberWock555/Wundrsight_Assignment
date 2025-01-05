using UnityEngine;
using UnityEngine.EventSystems;

public class HandleRotation : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform handle;
    public RectTransform circleCenter;
    public float maxRotation = 90f;

    bool isDragging = false;
    float clampedAngle;

    void Start()
    {
        handle.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            circleCenter, eventData.position, eventData.pressEventCamera, out localMousePosition);

        float angle = Mathf.Atan2(localMousePosition.y, localMousePosition.x) * Mathf.Rad2Deg - 90f;

        angle = NormalizeAngle(angle);

        clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);

        handle.localRotation = Quaternion.Euler(0, 0, clampedAngle);
        //SnakeMovement.SnakeRotation?.Invoke(clampedAngle);

    }

    private void LateUpdate()
    {
        if (isDragging)
        {
            SnakeMovement.SnakeRotation?.Invoke(clampedAngle);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.localRotation = Quaternion.Euler(Vector3.zero);
        isDragging = false;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
