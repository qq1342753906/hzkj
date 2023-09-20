//所有事件枚举
using Mediapipe;

public struct 首页选择模式
{
  public bool isOn;
}

public struct 手掌左右滑动状态
{
  /// <summary>
  /// 手掌状态,0不动，1向左滑动，2向右滑动，3向下滑动，4向上滑动
  /// </summary>
  public int HandType;
}
public struct 手掌上下滑动状态
{
  /// <summary>
  /// 手掌状态,0不动，1向左滑动，2向右滑动，3向下滑动，4向上滑动
  /// </summary>
  public int HandType;
}
public struct 握拳选择
{
}
public struct 播放教学视频
{
}
public struct 播放放松收尾阶段音频
{
}
public struct 时间闪烁效果
{
}
public struct 姿势识别数据解析
{
  public NormalizedLandmarkList nl;
}
public struct 姿势手掌轨迹重置
{
}

public struct 直接获取姿势数据
{
  public NormalizedLandmarkList nl;
}
public struct 左倾右倾
{
  /// <summary>
  /// 显示，0不显示，1左倾，2右倾
  /// </summary>
  public int showObj;
}
public struct 福利时间
{
    public bool fl;
}
public struct 更新等级
{
    public int 当前等级;
}
public struct 更新程序运行速度
{
    public float 当前运行速度;
}