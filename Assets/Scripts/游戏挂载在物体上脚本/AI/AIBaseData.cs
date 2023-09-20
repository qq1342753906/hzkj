using QFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBaseData : BaseObjData
{
    public Vector3 endPos;
    public void AddFaoce()
    {
        // 获取UI对象的屏幕位置
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // 获取屏幕的中心点
        float screenCenterX = Screen.width / 2;
        // 计算UI对象与屏幕中心的x轴距离，并根据该距离来确定施加力的x轴范围
        float distanceFromCenter = Mathf.Abs(screenPosition.x - screenCenterX);
        float maxForceX = Mathf.Lerp(4f, 8f, distanceFromCenter / screenCenterX);

        // 根据UI对象的位置来决定施加力的方向和大小
        Vector3 force;
        if (screenPosition.x < screenCenterX)
        {
            // 如果UI对象在屏幕的左侧，施加向右上方的力
            force = new Vector3(Random.Range(0f, maxForceX), Random.Range(10f, 14f), 0);
        }
        else
        {
            // 如果UI对象在屏幕的右侧，施加向左上方的力
            force = new Vector3(Random.Range(-maxForceX, 0f), Random.Range(10f, 14f), 0);
        }

        // 使用Rigidbody的AddForce方法来施加力
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.AddForce(force, ForceMode.Impulse);
        AudioKit.PlaySound("Resources://音效/昆虫飞入画面音效");
    }
}
