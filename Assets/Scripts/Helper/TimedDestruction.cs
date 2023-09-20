using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float lifetime = 1f; // 物体的生命周期，单位为秒

    void Start()
    {
        Destroy(gameObject, lifetime); // 在lifetime秒后销毁该游戏物体
    }
}
