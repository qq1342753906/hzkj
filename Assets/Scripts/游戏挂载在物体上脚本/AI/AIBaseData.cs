using QFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBaseData : BaseObjData
{
    public Vector3 endPos;
    public void AddFaoce()
    {
        // ��ȡUI�������Ļλ��
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // ��ȡ��Ļ�����ĵ�
        float screenCenterX = Screen.width / 2;
        // ����UI��������Ļ���ĵ�x����룬�����ݸþ�����ȷ��ʩ������x�᷶Χ
        float distanceFromCenter = Mathf.Abs(screenPosition.x - screenCenterX);
        float maxForceX = Mathf.Lerp(4f, 8f, distanceFromCenter / screenCenterX);

        // ����UI�����λ��������ʩ�����ķ���ʹ�С
        Vector3 force;
        if (screenPosition.x < screenCenterX)
        {
            // ���UI��������Ļ����࣬ʩ�������Ϸ�����
            force = new Vector3(Random.Range(0f, maxForceX), Random.Range(10f, 14f), 0);
        }
        else
        {
            // ���UI��������Ļ���Ҳ࣬ʩ�������Ϸ�����
            force = new Vector3(Random.Range(-maxForceX, 0f), Random.Range(10f, 14f), 0);
        }

        // ʹ��Rigidbody��AddForce������ʩ����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.AddForce(force, ForceMode.Impulse);
        AudioKit.PlaySound("Resources://��Ч/������뻭����Ч");
    }
}
