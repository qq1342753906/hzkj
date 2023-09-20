using Mediapipe.Unity;
using Mediapipe;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Mediapipe.Unity.CoordinateSystem;

public class 姿势识别
{
  public 姿势识别()
  {
    new 姿势识别手掌滑动检测();
  }
  // Start is called before the first frame update
  public void OnPoseLandmarksOutput(object stream, OutputEventArgs<NormalizedLandmarkList> eventArgs)
  {
    if (eventArgs.value == null)
    {
#if UNITY_EDITOR
      Debug.LogError("当前eventargsvalue为空");

#endif
            DataMgr.没有检测到人 = true;
      return;
    }
        DataMgr.没有检测到人 = false;

        姿势识别数据解析变量.nl = eventArgs.value;
    TypeEventSystem.Global.Send<姿势识别数据解析>(姿势识别数据解析变量);
    解析数据(eventArgs.value);
  }
  姿势识别数据解析 姿势识别数据解析变量 = new 姿势识别数据解析();
  public void 解析数据(NormalizedLandmarkList poseLandmarkList)
  {
        //0 - 鼻子
        //1 - 左眼（内部）
        //2 - 左眼
        //3 - 左眼（外部）
        //4 - 右眼（内部）
        //5 - 右眼
        //6 - 右眼（外部）
        //7 - 左耳
        //8 - 右耳
        //9 - 嘴巴（左侧）
        //10 - 嘴巴（右侧）
        //11 - 左肩
        //12 - 右肩
        //13 - 左肘
        //14 - 右肘
        //15 - 左腕
        //16 - 右腕
        //17 - 左小指
        //18 - 右小指
        //19 - 左食指
        //20 - 右食指
        //21 - 左拇指
        //22 - 右拇指
        //23 - 左臀部
        //24 - 右臀部
        //25 - 左膝盖
        //26 - 右膝盖
        //27 - 左脚踝
        //28 - 右脚踝
        //29 - 左脚后跟
        //30 - 右脚后跟
        //31 - 左脚趾
        //32 - 右脚趾
        //Debug.Log("当期的坐标位置2:"+ poseLandmarkList.Landmark[0]);
    //ThreadToMain.ToMain(1, () => {
    //  var v = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, poseLandmarkList.Landmark[0].X, poseLandmarkList.Landmark[0].Y, poseLandmarkList.Landmark[0].Z, RotationAngle.Rotation0, false);
    //  //Debug.Log("当期的坐标位置:" + v);
    //});

    /*
    //0 - nose
    NormalizedLandmark nose = poseLandmarkList.Landmark[0];


    //12 - right shoulder
    NormalizedLandmark rightShoulder = poseLandmarkList.Landmark[12];
    //16 - right wrist
    NormalizedLandmark rightWrist = poseLandmarkList.Landmark[16];
    // 24 - right hip
    NormalizedLandmark rightHip = poseLandmarkList.Landmark[24];
    //26 - right knee
    NormalizedLandmark rightKnee = poseLandmarkList.Landmark[26];
    //28 - right ankle
    NormalizedLandmark rightAnkle = poseLandmarkList.Landmark[28];
    //30 - right heel
    NormalizedLandmark rightHeel = poseLandmarkList.Landmark[30];
    //32 -  right foot index
    NormalizedLandmark rightFootIndex = poseLandmarkList.Landmark[32];


    //11 - left shoulder
    NormalizedLandmark leftShoulder = poseLandmarkList.Landmark[11];
    //15 - left wrist
    NormalizedLandmark leftWrist = poseLandmarkList.Landmark[15];
    //23 - left hip
    NormalizedLandmark leftHip = poseLandmarkList.Landmark[23];
    //25 - left knee
    NormalizedLandmark leftKnee = poseLandmarkList.Landmark[25];
    //27 - left ankle
    NormalizedLandmark leftAnkle = poseLandmarkList.Landmark[27];
    //29 - left heel
    NormalizedLandmark leftHeel = poseLandmarkList.Landmark[29];
    //31 - left foot index
    NormalizedLandmark leftFootIndex = poseLandmarkList.Landmark[31];
    */
  }
}
