using UnityEngine;

public class DotDirectionIndicator : MonoBehaviour
{
    public Transform player;          // Kéo Player (hoặc Main Camera) vào đây
    public float orbitRadius = 8f;    // bán kính vị trí chấm quanh tâm crosshair

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void UpdateDirection(Vector3 targetPosition)
    {
        // Hướng từ player tới vật, chỉ tính trên mặt phẳng ngang (bỏ qua độ cao Y)
        Vector3 toTarget = targetPosition - player.position;
        toTarget.y = 0;
        toTarget.Normalize();

        Vector3 forward = player.forward;
        forward.y = 0;
        forward.Normalize();

        // Góc có dấu giữa hướng nhìn và hướng tới vật (âm = trái, dương = phải)
        float angle = Vector3.SignedAngle(forward, toTarget, Vector3.up);

        // 0 độ = thẳng trước mặt -> chấm ở trên cùng (top)
        // 180 độ (hoặc -180) = sau lưng -> chấm ở dưới cùng (bottom)
        float rad = angle * Mathf.Deg2Rad;
        float x = Mathf.Sin(rad) * orbitRadius;
        float y = Mathf.Cos(rad) * orbitRadius;

        rect.anchoredPosition = new Vector2(x, y);
    }
}