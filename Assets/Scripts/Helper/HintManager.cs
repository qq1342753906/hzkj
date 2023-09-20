using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using QFramework;
using System.Linq;

public class HintManager : Singleton<HintManager>
{
  private GameObject hintPrefab;
  private Transform canvas;
  private List<GameObject> activeHints = new List<GameObject>();
  private float initialYPosition = -Screen.height * 0.3f;
  private float positionOffset = 110;
  int maxVisibleHints;
  Dictionary<int, float> objDicPos = new Dictionary<int, float>();
  public void 注入(GameObject hintPrefab, Transform canvas)
  {
    if (this.hintPrefab != null && this.canvas != null)
    {
      return;
    }
    this.hintPrefab = hintPrefab;
    this.canvas = canvas;
    Debug.Log("当前屏幕高度:" + Screen.height * 0.8f + ",偏移量:" + positionOffset + ",最终结果:" + Mathf.FloorToInt(Screen.height * 0.8f / positionOffset));
    maxVisibleHints = Mathf.FloorToInt(Screen.height * 0.7f / positionOffset);
    objDicPos.Clear();
    for (int n = 0; n < maxVisibleHints; n++)
    {
      objDicPos.Add(n, initialYPosition + positionOffset * (maxVisibleHints - n));
    }
  }
  private HintManager()
  {

  }

  public void ShowHint(string message, float displayTime = 3.0f)
  {
    GameObject hint = Object.Instantiate(hintPrefab, canvas);
    Text hintText = hint.GetComponentInChildren<Text>();
    hintText.text = message;
    HintHelper hintHelper = hint.AddComponent<HintHelper>();
    RectTransform hintTransform = hint.GetComponent<RectTransform>();
    hintTransform.anchoredPosition = new Vector2(0, initialYPosition);
    hintTransform.localScale = Vector3.zero;

    Image hintImage = hint.GetComponent<Image>();
    Text childText = hintText.GetComponent<Text>();

    activeHints.Add(hint);


    float targetYPosition;
    //这边的位置计算有问题，应该直接使用字典来存储，然后每次判断当前的位置是否有人了，并且在现实了，优先现实在没有显示的地方，如果都有显示了，则优先显示在最早显示的地方，应该用list存储起来这个编号？

    if (activeHints.Count > maxVisibleHints)
    {
      Debug.Log("这边运行了？" + message);
      GameObject hintToRemove = activeHints[0];
      hintHelper.Y位置 = hintToRemove.GetComponent<HintHelper>().Y位置;
      activeHints.Remove(hintToRemove);
      Object.Destroy(hintToRemove);
    }
    else
    {
      Debug.Log("这边运行了？" + message);
      hintHelper.Y位置 = initialYPosition + positionOffset * (maxVisibleHints - activeHints.Count);
    }

    Sequence sequence = DOTween.Sequence();
    sequence.Append(hintTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack))
            .Join(hintTransform.DOAnchorPosY(hintHelper.Y位置, 1f).SetEase(Ease.OutBack))
            .AppendInterval(displayTime)
            .Append(hintImage.DOFade(0, 1.0f))
            .Join(childText.DOFade(0, 1.0f))
            .OnComplete(() =>
            {
              if (activeHints.Contains(hint))
              {
                activeHints.Remove(hint);
                Object.Destroy(hint);
              }

            });
  }
}
