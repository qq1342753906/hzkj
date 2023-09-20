using UnityEngine;
using System.Collections.Generic;

public class BezierCurveGizmo : MonoBehaviour
{
  public List<Transform> controlPoints = new List<Transform>();
  public int resolution = 10;

  public List<Vector3> curvePoints = new List<Vector3>();
  Transform cp;
  private void Start()
  {
    cp = new GameObject("CubeParent").transform;

  }
  private void OnDrawGizmos()
  {
    curvePoints.Clear();

    Gizmos.color = Color.white;
    if (controlPoints.Count >= 2)
    {
      for (int i = 0; i <= resolution; i++)
      {
        float t = i / (float)resolution;
        Vector3 point = CalculateBezierPoint(t, controlPoints.ToArray());

        curvePoints.Add(point);

        if (i > 0)
        {
          Vector3 prevPoint = CalculateBezierPoint((i - 1) / (float)resolution, controlPoints.ToArray());
          Gizmos.DrawLine(prevPoint, point);
        }
      }
    }
  }

  private void Update()
  {
    // Press "C" key to create cubes at the curve points
    if (Input.GetKeyDown(KeyCode.C))
    {
      AddControlPoint();
    }
    // Press "C" key to create cubes at the curve points
    if (Input.GetKeyDown(KeyCode.Z))
    {
      CreateCubes();
    }

    // Press "X" key to remove the last control point
    if (Input.GetKeyDown(KeyCode.X))
    {
      RemoveControlPoint();
    }
  }

  private Vector3 CalculateBezierPoint(float t, Transform[] points)
  {
    int n = points.Length - 1;
    Vector3 p = Vector3.zero;

    for (int i = 0; i < points.Length; i++)
    {
      float blend = BinomialCoefficient(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
      p += points[i].position * blend;
    }

    return p;
  }

  private int BinomialCoefficient(int n, int k)
  {
    int result = 1;
    for (int i = 1; i <= k; i++)
    {
      result *= (n - i + 1);
      result /= i;
    }
    return result;
  }

  private void AddControlPoint()
  {
    GameObject newControlPoint = new GameObject("ControlPoint");
    newControlPoint.transform.position = Vector3.zero;
    controlPoints.Add(newControlPoint.transform);

  }
  private void CreateCubes()
  {
    foreach (Vector3 point in curvePoints)
    {
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.position = point;
      cube.transform.parent = cp;
    }
  }
  private void RemoveControlPoint()
  {
    if (controlPoints.Count > 0)
    {
      Transform lastControlPoint = controlPoints[controlPoints.Count - 1];
      DestroyImmediate(lastControlPoint.gameObject);
      controlPoints.RemoveAt(controlPoints.Count - 1);
    }
  }
}
