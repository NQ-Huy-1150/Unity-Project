using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnDrawGizmos()
    {
        // Vẽ hình cầu trong Scene view để dễ nhìn vị trí spawn
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}