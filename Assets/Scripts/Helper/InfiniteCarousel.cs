using DG.Tweening;

using QFramework;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class 图片滚动效果 : MonoBehaviour
{
  public Image[] 图片数组;
  public float 间距 = 100f;
  [HideInInspector]  // 默认隐藏这个变量
  public bool 手动设置缩放因子 = true;

  public float 缩放因子;
  public float 滑动时间 = 0.5f;  // 滑动动画的时长

  private int 中心索引 = 0;
  private Vector2 _中心位置;
  public List<Sprite> 精灵数组 = new List<Sprite>();

  private void Start()
  {
    是奇数 = (图片数组.Length % 2) == 1;
    if (是奇数)
    {
      图片分组总数 = (图片数组.Length + 1) / 2;

    }
    else
    {
      图片分组总数 = 图片数组.Length / 2;
    }
    _中心位置 = 图片数组[0].rectTransform.anchoredPosition;
    if (!手动设置缩放因子)
    {
      缩放因子 = 1.0f / 图片分组总数;
    }
    排列图片();

    TypeEventSystem.Global.Register<手掌左右滑动状态>(e =>
    {
      if (DataMgr.手掌滑动时间间隔 > 1)
      {

        ThreadToMain.ToMain<手掌左右滑动状态>(e, (s) =>
            {

              var f = (手掌左右滑动状态)s;
              if (f.HandType == 1)
              {
                中心索引 = (中心索引 - 1 + 图片数组.Length) % 图片数组.Length;
                排列图片(true);
              }
              else
              {
                中心索引 = (中心索引 + 1) % 图片数组.Length;
                排列图片(true);
              }
              ////TextDisplayer.DisplayText("当前主界面选择索引:", 中心索引);
              DataMgr.ResertTime();
              AudioKit.PlaySound("Resources://change");
            }
            );
      }
    }).UnRegisterWhenGameObjectDestroyed(gameObject);

    TypeEventSystem.Global.Register<握拳选择>(e =>
    {
      if (DataMgr.握拳选择间隔 > 2f && DataMgr.手掌滑动时间间隔 > 1)
      {
        DataMgr.ResertTime();
        ////TextDisplayer.DisplayText("当前最终主界面选择索引:", 中心索引);
        Debug.Log("当前选择的场景:" + 中心索引);

      }
    }).UnRegisterWhenGameObjectDestroyed(gameObject);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      中心索引 = (中心索引 - 1 + 图片数组.Length) % 图片数组.Length;
      排列图片(true);
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      中心索引 = (中心索引 + 1) % 图片数组.Length;
      排列图片(true);
    }
  }
  private int 图片分组总数;
  private bool 是奇数;
  private void 排列图片(bool 动画 = false)
  {

    //需要设置图片下的文字颜色
    for (var i = 0; i < 图片数组.Length; i++)
    {
      Vector2 目标位置;
      float 缩放比例;

      if (i == 中心索引) // 中心图片
      {
        目标位置 = _中心位置;
        缩放比例 = 1f;
        // Debug.Log("图片分组总数:索引:" + i + "图片名称:" + 图片数组[i].gameObject.name);
        Debug.Log("图片分组总数:索引:" + 图片分组总数 + ",图片名称:" + 图片数组[i].gameObject.name + ",目标位置:" + 目标位置);
        图片数组[i].sprite = 精灵数组[0];
        图片数组[i].transform.Find("Text").GetComponent<Text>().color = Color.white;
      }
      else
      {
        var 相对索引 = (i - 中心索引 + 图片数组.Length) % 图片数组.Length;
        图片数组[i].sprite = 精灵数组[1];
        图片数组[i].transform.Find("Text").GetComponent<Text>().color = new Color(0.6901961f, 0.682353f, 0.6784314f, 1);
        if (相对索引 < 图片分组总数)
        {
          var 偏移量 = 相对索引;
          目标位置 = _中心位置 + new Vector2(间距 * 偏移量, 0);
          Debug.Log("间距:" + 间距 + ",偏移量:" + 偏移量);
          if (目标位置 == _中心位置)
          {
            缩放比例 = 0.1f;
          }
          else
          {
            缩放比例 = Mathf.Abs(1 - Mathf.Abs(_中心位置.x - 目标位置.x) / 间距 * 缩放因子);
          }
        }
        else
        {
          var 偏移量 = 相对索引 - 图片数组.Length;
          Debug.Log("图片分组总数:间距:" + 间距 + ",偏移量:" + 偏移量);
          目标位置 = _中心位置 + new Vector2(间距 * 偏移量, 0);
          if (目标位置 == _中心位置)
          {
            缩放比例 = 0.1f;
          }
          else
          {
            缩放比例 = Mathf.Abs(1 - (Mathf.Abs(_中心位置.x - 目标位置.x) / 间距) * 缩放因子);
          }
        }


        Debug.Log("图片分组总数:" + 图片分组总数 + ",相对索引:" + 相对索引 + ",目标位置:" + 目标位置 + ",缩放比例" + 缩放比例 + ",图片名称:" + 图片数组[i].gameObject.name + ",与中心的间距:" + Mathf.Abs(_中心位置.x - 目标位置.x) + ",缩放因子:" + 缩放因子 + ",位置比例:" + (Mathf.Abs(_中心位置.x - 目标位置.x) / 间距));

      }

      if (动画)
      {
        图片数组[i].rectTransform.DOAnchorPos(目标位置, 滑动时间);
        图片数组[i].rectTransform.DOScale(new Vector2(缩放比例, 缩放比例), 滑动时间);
      }
      else
      {
        图片数组[i].rectTransform.anchoredPosition = 目标位置;
        图片数组[i].rectTransform.localScale = new Vector2(缩放比例, 缩放比例);
      }
    }

    // 设置中心图片的层级为最高
    图片数组[中心索引].transform.SetAsLastSibling();
  }

}
