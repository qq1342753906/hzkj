using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float lifetime = 1f; // ������������ڣ���λΪ��

    void Start()
    {
        Destroy(gameObject, lifetime); // ��lifetime������ٸ���Ϸ����
    }
}
