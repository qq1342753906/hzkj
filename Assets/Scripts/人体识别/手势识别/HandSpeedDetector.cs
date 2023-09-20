using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.HandTracking;
using Mediapipe.Unity;
using Mediapipe;
using System;

public class HandSpeedDetector
{
  public HandSpeedDetector()
  {
  }

  // �ֲ��ؼ���ص������������ֲ��ؼ���ע��
  //public static void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  //{
  //    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
  //    Debug.Log(handLandmarks==null);
  //}
  // ���ڴ洢��ǰ���ֲ��ؼ����ʱ��
  private List<NormalizedLandmarkList> previousLandmarks = new List<NormalizedLandmarkList>();
  private List<float> previousTimes = new List<float>();

  #region �ٶ�
  //// �ֲ��ؼ�������Ļص�����
  //public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  //{
  //    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
  //    if (handLandmarks == null)
  //    {
  //        return;
  //    }
  //    if (IsFistGesture(handLandmarks))
  //    {
  //        Debug.Log($"Hand 1 is in fist gesture.");
  //    }
  //    else
  //    {
  //        Debug.Log($"Hand 2 is not in fist gesture.");
  //    }
  //    // ������⵽�������ֲ�
  //    for (int i = 0; i < handLandmarks.Count; i++)
  //    {

  //        if (handLandmarks[i].Landmark.Count > 0)
  //        {
  //            Vector3 currentPosition = new Vector3(handLandmarks[i].Landmark[0].X, handLandmarks[i].Landmark[0].Y, handLandmarks[i].Landmark[0].Z);
  //            float currentTime = DateTime.Now.Millisecond;

  //            if (previousLandmarks.Count > i && previousLandmarks[i] != null)
  //            {
  //                Vector3 positionDelta = currentPosition - GetPreviousPosition(i);
  //                float timeDelta = currentTime - GetPreviousTime(i);

  //                Vector3 velocity = positionDelta / timeDelta;

  //                Debug.Log($"Hand {i + 1} Speed: {(velocity*100).magnitude}");
  //            }

  //            UpdatePreviousLandmarks(i, handLandmarks[i]);
  //            UpdatePreviousTime(i, currentTime);
  //        }
  //    }
  //}

  //// ��ȡ��ǰ��λ��
  //private Vector3 GetPreviousPosition(int index)
  //{
  //    return new Vector3(previousLandmarks[index].Landmark[0].X, previousLandmarks[index].Landmark[0].Y, previousLandmarks[index].Landmark[0].Z);
  //}

  //// ��ȡ��ǰ��ʱ��
  //private float GetPreviousTime(int index)
  //{
  //    return previousTimes[index];
  //}

  //// ������ǰ���ֲ��ؼ���
  //private void UpdatePreviousLandmarks(int index, NormalizedLandmarkList landmarks)
  //{
  //    if (previousLandmarks.Count > index)
  //    {
  //        previousLandmarks[index] = landmarks;
  //    }
  //    else
  //    {
  //        previousLandmarks.Add(landmarks);
  //    }
  //}

  //// ������ǰ��ʱ��
  //private void UpdatePreviousTime(int index, float time)
  //{
  //    if (previousTimes.Count > index)
  //    {
  //        previousTimes[index] = time;
  //    }
  //    else
  //    {
  //        previousTimes.Add(time);
  //    }
  //}

  //// �ж����Ƿ���ȭ�ķ���
  //private bool IsFistGesture(List<NormalizedLandmarkList> handLandmarks2)
  //{
  //    // �����ֲ��ؼ��������ж��Ƿ���ȭ
  //    // ������Ը����ֲ��ؽڽǶȡ���ָ�����ȵ���Ϣ���ж��Ƿ���ȭ
  //    // ����ʾ�������������ʾ��������ж��߼�������Ҫ�����ӵĴ���
  //    bool isFist = false;
  //    foreach(var handLandmarks in handLandmarks2) 
  //    // ����ֻ�жϴ�Ĵָ��ʳָ��λ��
  //    if (handLandmarks.Landmark.Count >= 2)
  //    {
  //        Vector3 thumbPosition = new Vector3(handLandmarks.Landmark[0].X, handLandmarks.Landmark[0].Y, handLandmarks.Landmark[0].Z);
  //        Vector3 indexFingerPosition = new Vector3(handLandmarks.Landmark[8].X, handLandmarks.Landmark[8].Y, handLandmarks.Landmark[8].Z);

  //        // ���赱��Ĵָ��ʳָ����ܽ�ʱ�ж�Ϊ��ȭ
  //        float distanceThreshold = 0.05f; // ������ֵ�ɸ���ʵ���������
  //        float distance = Vector3.Distance(thumbPosition, indexFingerPosition);

  //        if (distance < distanceThreshold)
  //        {
  //            isFist = true;
  //        }
  //    }

  //    return isFist;
  //}

  #endregion
  public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  {
    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
    if (handLandmarks != null)
    {
      Debug.Log("handLandmarks������:" + handLandmarks.Count);
      if (�ж���ȭ����(handLandmarks))
      {
        Debug.Log($"Hand 1 is in complex gesture.");
      }
      else
      {
        Debug.Log($"Hand 2 is not in complex gesture.");
      }
    }
    // ������⵽�������ֲ�
    //for (int i = 0; i < handLandmarks.Count; i++)
    //{

    //}
  }

  private bool �ж���ȭ����(List<NormalizedLandmarkList> �ֲ��ؼ����б�)
  {
    bool �Ƿ���ȭ = false;

    foreach (var �ֲ��ؼ��� in �ֲ��ؼ����б�)
    {
      if (�ֲ��ؼ���.Landmark.Count >= 21)
      {
        // ��ȡ��ָ�ؼ�������ƻ�׼��
        NormalizedLandmark ��Ĵָָ�� = �ֲ��ؼ���.Landmark[4];
        NormalizedLandmark ʳָָ�� = �ֲ��ؼ���.Landmark[8];
        NormalizedLandmark ��ָָ�� = �ֲ��ؼ���.Landmark[12];
        NormalizedLandmark ����ָָ�� = �ֲ��ؼ���.Landmark[16];
        NormalizedLandmark Сָָ�� = �ֲ��ؼ���.Landmark[20];
        NormalizedLandmark ���ƻ�׼�� = �ֲ��ؼ���.Landmark[0];

        // ������ָָ�����ָָ�������ƻ�׼��ľ���
        float ��Ĵָָ����� = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(��Ĵָָ��.X, ��Ĵָָ��.Y));
        float ʳָָ����� = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(ʳָָ��.X, ʳָָ��.Y));
        float ��ָָ����� = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(��ָָ��.X, ��ָָ��.Y));
        float ����ָָ����� = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(����ָָ��.X, ����ָָ��.Y));
        float Сָָ����� = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(Сָָ��.X, Сָָ��.Y));

        float ��Ĵָָ������ = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(�ֲ��ؼ���.Landmark[2].X, �ֲ��ؼ���.Landmark[2].Y));
        float ʳָָ������ = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(�ֲ��ؼ���.Landmark[6].X, �ֲ��ؼ���.Landmark[6].Y));
        float ��ָָ������ = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(�ֲ��ؼ���.Landmark[10].X, �ֲ��ؼ���.Landmark[10].Y));
        float ����ָָ������ = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(�ֲ��ؼ���.Landmark[14].X, �ֲ��ؼ���.Landmark[14].Y));
        float Сָָ������ = Vector2.Distance(new Vector2(���ƻ�׼��.X, ���ƻ�׼��.Y), new Vector2(�ֲ��ؼ���.Landmark[18].X, �ֲ��ؼ���.Landmark[18].Y));

        // �ж���ָָ�����ָָ���ĸ����ӽ�����
        bool ��Ĵָ���� = ��Ĵָָ����� < ��Ĵָָ������;
        bool ʳָ���� = ʳָָ����� < ʳָָ������;
        bool ��ָ���� = ��ָָ����� < ��ָָ������;
        bool ����ָ���� = ����ָָ����� < ����ָָ������;
        bool Сָ���� = Сָָ����� < Сָָ������;

        // �ж��Ƿ���ȭ
        int ��������ָ���� = 0;

        if (��Ĵָ����) ��������ָ����++;
        if (ʳָ����) ��������ָ����++;
        if (��ָ����) ��������ָ����++;
        if (����ָ����) ��������ָ����++;
        if (Сָ����) ��������ָ����++;

        if (��������ָ���� >= 3)
        {
          �Ƿ���ȭ = true;
          break;
        }
      }
    }

    return �Ƿ���ȭ;
  }




}
