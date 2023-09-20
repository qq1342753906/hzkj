using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;

using System;

using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Example
{
    public class UIMousePanelData : UIPanelData
	{
	}
	public partial class UIMousePanel : UIPanel
	{
        public Camera mainCamera; // 主摄像机
        private Button hitButton; // 被射线击中的按钮
        private float stayTime = 0f; // 鼠标停留在按钮上的时间
        private bool 使用鼠标来控制鼠标;
        private bool 直接使用手掌来控制鼠标;
        public float 屏幕宽度;
        public float 屏幕高度;
        UnityEngine.Rect rect;
        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIMousePanelData ?? new UIMousePanelData();
            mainCamera = GameObject.FindObjectOfType<UIRoot>().UICamera;
            屏幕宽度 = GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.x / 2;
            屏幕高度 = GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y / 2;
            hitButton = transform.Find("w").GetComponent<Button>();
            hitButton.onClick.AddListener(() => { Debug.Log("触发了1"); });
            hitButton.onClick.AddListener(() => { Debug.Log("触发了2"); });
            hitButton.onClick.AddListener(() => { Debug.Log("触发了3"); });
            使用鼠标来控制鼠标 = false;
            直接使用手掌来控制鼠标 = true;
            rect = GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect;
            initEvent();
        }
        private void initEvent()
        {
            //InvokeRepeating("UpdateTimer", 1.0f, 1.0f); // 每秒更新一次
            TypeEventSystem.Global.Register<姿势识别数据解析>(玩家移动).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void 玩家移动(姿势识别数据解析 zs)
        {
            Debug.Log("玩家移动解析:");
            Debug.Log("当前时间"+DateTime.UtcNow);
            ThreadToMain.ToMain(1, () =>
            {
                var landmark = zs.nl.Landmark;
                if (直接使用手掌来控制鼠标)
                {
                    var 右手掌 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[20].X, landmark[20].Y, 0, RotationAngle.Rotation0, false);
                    人体识别鼠标位置 = CanvasToScreenCoordinateConverter.Instance.屏幕转Canvas位置(右手掌*new Vector2(-1,1));
                }
                else
                {
                    //使用新的算法来控制鼠标，
                    var 鼻子 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                    Vector3 pos = Vector3.zero;
                    //TextDisplayer.DisplayText("v1Y", v1.y);

                    //if (v1.y >RangeY)
                    //{
                    var 右肩膀 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[12].X, landmark[12].Y, 0, RotationAngle.Rotation0, false);
                    var 右手腕 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[16].X, landmark[16].Y, 0, RotationAngle.Rotation0, false);
                    float 比例X1 = 鼻子.x - 右肩膀.x;
                    float 比例距离X = (鼻子.x - 右手腕.x) / 比例X1;
                    float 比例y1 = 鼻子.y - 右肩膀.y;
                    float 比例距离y = (右手腕.y-鼻子.y ) / 比例y1;
                    人体识别鼠标位置= CanvasToScreenCoordinateConverter.Instance.比例转Canvas位置(new Vector2(比例距离X, 比例距离y));
                }

            }
            );
        }
        Vector2 人体识别鼠标位置;
        public LayerMask uiLayer;
        public float dic = 10;
        void FixedUpdate()
        {
#if UNITY_EDITOR
            // 设置鼠标Image的位置以跟随电脑鼠标
            if (使用鼠标来控制鼠标)
            {
                鼠标.rectTransform.anchoredPosition = CanvasToScreenCoordinateConverter.Instance.屏幕转Canvas位置(Input.mousePosition) - new Vector3(屏幕宽度, 屏幕高度, 0);
            }
            else
#endif
            {
                //if (Vector3.Distance(鼠标.rectTransform.anchoredPosition, 人体识别鼠标位置) > dic)
                //{
                鼠标.rectTransform.anchoredPosition = 人体识别鼠标位置;// Vector3.MoveTowards(鼠标.rectTransform.anchoredPosition, 人体识别鼠标位置, 50);
                //}
            }

            // 获取鼠标在世界坐标中的位置
            Vector2 worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //TextDisplayer.DisplayText("鼠标位置:", Input.mousePosition);
            //TextDisplayer.DisplayText("鼠标Imageans位置:", 鼠标.rectTransform.anchoredPosition);
            //TextDisplayer.DisplayText("鼠标Image位置:", 鼠标.rectTransform.position);
            //TextDisplayer.DisplayText("鼠标Image位置2:", 鼠标.transform.position);
            //TextDisplayer.DisplayText("鼠标Image位置2:", mainCamera.WorldToScreenPoint(鼠标.transform.position));
            //TextDisplayer.DisplayText("鼠标在世界坐标中的位置:", worldMousePos);

            // 从摄像机发射一条射线到鼠标的位置
            Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(鼠标.transform.position));

            if (Physics.Raycast(ray, out RaycastHit hit, 100, uiLayer))
            {
                // 检查射线是否击中了带有Collider组件的对象
                if (hit.collider != null)
                {
                    // 获取被击中对象的Button组件
                    Button hitButtonTemp = hit.collider.gameObject.GetComponent<Button>();

                    // 检查被击中的对象是否是一个按钮
                    if (hitButtonTemp != null)
                    {
                        // 检查是否是新的按钮
                        if (hitButton != hitButtonTemp)
                        {
                            stayTime = 0f;
                            hitButton = hitButtonTemp;
                        }

                        stayTime += Time.deltaTime; // 累计停留时间

                        // 如果停留时间超过1秒
                        if (stayTime >= 1f)
                        {
                            // 触发按钮上的所有事件
                            hitButton.onClick.Invoke();
                            stayTime = 0f; // 重置停留时间
                        }
                    }
                }
                else
                {
                    stayTime = 0f; // 如果射线没有击中任何对象，则重置停留时间
                    hitButton = null;
                }

            }
            else
            {
                stayTime = 0f; // 如果射线没有击中任何对象，则重置停留时间
                hitButton = null;
            }
            // 设置鼠标检测轮廓的fillAmount属性
            鼠标检测轮廓.fillAmount = stayTime;
        }
        protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
