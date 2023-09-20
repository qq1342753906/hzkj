using QFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.5f;

        //// ��ȡUI�������Ļλ��
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //// ��ȡ��Ļ�����ĵ�
        //float screenCenterX = Screen.width / 2;
        //// ����UI��������Ļ���ĵ�x����룬�����ݸþ�����ȷ��ʩ������x�᷶Χ
        //float distanceFromCenter = Mathf.Abs(screenPosition.x - screenCenterX);
        //float maxForceX = Mathf.Lerp(4f, 8f, distanceFromCenter / screenCenterX);

        //// ����UI�����λ��������ʩ�����ķ���ʹ�С
        //Vector3 force;
        //if (screenPosition.x < screenCenterX)
        //{
        //    // ���UI��������Ļ����࣬ʩ�������Ϸ�����
        //    force = new Vector3(Random.Range(0f, maxForceX), Random.Range(10f, 14f), 0);
        //}
        //else
        //{
        //    // ���UI��������Ļ���Ҳ࣬ʩ�������Ϸ�����
        //    force = new Vector3(Random.Range(-maxForceX, 0f), Random.Range(10f, 14f), 0);
        //}

        //// ʹ��Rigidbody��AddForce������ʩ����
        //GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
    public GameObject jjjsd;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            GameObject obj = GameObject.Instantiate(jjjsd);
            obj.transform.parent = transform.parent;
            obj.transform.localScale = Vector3.one;
            obj.transform.position = transform.position;
            // ��ȡUI�������Ļλ��
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(obj.transform.position);

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
            obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

}
