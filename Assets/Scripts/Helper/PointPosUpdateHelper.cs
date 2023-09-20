using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;

using QFramework;

using UnityEngine;
public class PointPosUpdateHelper : HierarchicalAnnotation
{
  public Vector3 获取当前骨骼点在屏幕的坐标(Landmark target, Vector3 scale, bool visualizeZ = true)
  {
    return GetScreenRect().GetPoint(target, scale, rotationAngle, isMirrored);
  }
}
