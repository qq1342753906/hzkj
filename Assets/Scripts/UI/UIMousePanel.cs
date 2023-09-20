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
        public Camera mainCamera; // �������
        private Button hitButton; // �����߻��еİ�ť
        private float stayTime = 0f; // ���ͣ���ڰ�ť�ϵ�ʱ��
        private bool ʹ��������������;
        private bool ֱ��ʹ���������������;
        public float ��Ļ���;
        public float ��Ļ�߶�;
        UnityEngine.Rect rect;
        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIMousePanelData ?? new UIMousePanelData();
            mainCamera = GameObject.FindObjectOfType<UIRoot>().UICamera;
            ��Ļ��� = GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.x / 2;
            ��Ļ�߶� = GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y / 2;
            hitButton = transform.Find("w").GetComponent<Button>();
            hitButton.onClick.AddListener(() => { Debug.Log("������1"); });
            hitButton.onClick.AddListener(() => { Debug.Log("������2"); });
            hitButton.onClick.AddListener(() => { Debug.Log("������3"); });
            ʹ�������������� = false;
            ֱ��ʹ��������������� = true;
            rect = GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect;
            initEvent();
        }
        private void initEvent()
        {
            //InvokeRepeating("UpdateTimer", 1.0f, 1.0f); // ÿ�����һ��
            TypeEventSystem.Global.Register<����ʶ�����ݽ���>(����ƶ�).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void ����ƶ�(����ʶ�����ݽ��� zs)
        {
            Debug.Log("����ƶ�����:");
            Debug.Log("��ǰʱ��"+DateTime.UtcNow);
            ThreadToMain.ToMain(1, () =>
            {
                var landmark = zs.nl.Landmark;
                if (ֱ��ʹ���������������)
                {
                    var ������ = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[20].X, landmark[20].Y, 0, RotationAngle.Rotation0, false);
                    ����ʶ�����λ�� = CanvasToScreenCoordinateConverter.Instance.��ĻתCanvasλ��(������*new Vector2(-1,1));
                }
                else
                {
                    //ʹ���µ��㷨��������꣬
                    var ���� = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                    Vector3 pos = Vector3.zero;
                    //TextDisplayer.DisplayText("v1Y", v1.y);

                    //if (v1.y >RangeY)
                    //{
                    var �Ҽ�� = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[12].X, landmark[12].Y, 0, RotationAngle.Rotation0, false);
                    var ������ = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[16].X, landmark[16].Y, 0, RotationAngle.Rotation0, false);
                    float ����X1 = ����.x - �Ҽ��.x;
                    float ��������X = (����.x - ������.x) / ����X1;
                    float ����y1 = ����.y - �Ҽ��.y;
                    float ��������y = (������.y-����.y ) / ����y1;
                    ����ʶ�����λ��= CanvasToScreenCoordinateConverter.Instance.����תCanvasλ��(new Vector2(��������X, ��������y));
                }

            }
            );
        }
        Vector2 ����ʶ�����λ��;
        public LayerMask uiLayer;
        public float dic = 10;
        void FixedUpdate()
        {
#if UNITY_EDITOR
            // �������Image��λ���Ը���������
            if (ʹ��������������)
            {
                ���.rectTransform.anchoredPosition = CanvasToScreenCoordinateConverter.Instance.��ĻתCanvasλ��(Input.mousePosition) - new Vector3(��Ļ���, ��Ļ�߶�, 0);
            }
            else
#endif
            {
                //if (Vector3.Distance(���.rectTransform.anchoredPosition, ����ʶ�����λ��) > dic)
                //{
                ���.rectTransform.anchoredPosition = ����ʶ�����λ��;// Vector3.MoveTowards(���.rectTransform.anchoredPosition, ����ʶ�����λ��, 50);
                //}
            }

            // ��ȡ��������������е�λ��
            Vector2 worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //TextDisplayer.DisplayText("���λ��:", Input.mousePosition);
            //TextDisplayer.DisplayText("���Imageansλ��:", ���.rectTransform.anchoredPosition);
            //TextDisplayer.DisplayText("���Imageλ��:", ���.rectTransform.position);
            //TextDisplayer.DisplayText("���Imageλ��2:", ���.transform.position);
            //TextDisplayer.DisplayText("���Imageλ��2:", mainCamera.WorldToScreenPoint(���.transform.position));
            //TextDisplayer.DisplayText("��������������е�λ��:", worldMousePos);

            // �����������һ�����ߵ�����λ��
            Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(���.transform.position));

            if (Physics.Raycast(ray, out RaycastHit hit, 100, uiLayer))
            {
                // ��������Ƿ�����˴���Collider����Ķ���
                if (hit.collider != null)
                {
                    // ��ȡ�����ж����Button���
                    Button hitButtonTemp = hit.collider.gameObject.GetComponent<Button>();

                    // ��鱻���еĶ����Ƿ���һ����ť
                    if (hitButtonTemp != null)
                    {
                        // ����Ƿ����µİ�ť
                        if (hitButton != hitButtonTemp)
                        {
                            stayTime = 0f;
                            hitButton = hitButtonTemp;
                        }

                        stayTime += Time.deltaTime; // �ۼ�ͣ��ʱ��

                        // ���ͣ��ʱ�䳬��1��
                        if (stayTime >= 1f)
                        {
                            // ������ť�ϵ������¼�
                            hitButton.onClick.Invoke();
                            stayTime = 0f; // ����ͣ��ʱ��
                        }
                    }
                }
                else
                {
                    stayTime = 0f; // �������û�л����κζ���������ͣ��ʱ��
                    hitButton = null;
                }

            }
            else
            {
                stayTime = 0f; // �������û�л����κζ���������ͣ��ʱ��
                hitButton = null;
            }
            // ���������������fillAmount����
            ���������.fillAmount = stayTime;
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
