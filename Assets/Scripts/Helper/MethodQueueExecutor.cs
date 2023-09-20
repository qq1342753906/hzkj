using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace QFramework2
{
  public class MethodQueueExecutor
  {
    private Queue<Func<bool>> methodQueue = new Queue<Func<bool>>();
    public static MethodQueueExecutor CreadMethodQueueExecutor()
    {
      return new MethodQueueExecutor();
    }
    public void AddMethodQueue(Func<bool> fu)
    {
      methodQueue.Enqueue(fu);
    }


    public void ExecuteMethodQueue()
    {
      if (methodQueue.Count > 0)
      {
        Func<bool> method = methodQueue.Dequeue();
        new TaskM(waitEnd(method));
      }
      else
      {
        Debug.Log("All methods have been executed.");
      }
    }
    public void ExecuteMethodQueueSync()
    {
      new TaskM(waitExecuteMethodQueue());
      ExecuteMethodQueue();
    }
    IEnumerator waitExecuteMethodQueue()
    {
      yield return new WaitForSeconds(0.1f);
    }
    IEnumerator waitEnd(Func<bool> ac)
    {
      yield return new WaitUntil(() => ac.Invoke());
      ExecuteMethodQueue();
    }
  }
}