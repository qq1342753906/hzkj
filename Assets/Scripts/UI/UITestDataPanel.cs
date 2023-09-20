using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Google.Protobuf.WellKnownTypes;

namespace QFramework.Example
{
	public class UITestDataPanelData : UIPanelData
	{
	}
	public partial class UITestDataPanel : UIPanel
	{
        string strValue = "0.01"; // �������ַ���ֵ
        float floatValue;
        int intValue;
        protected override void OnInit(IUIData uiData = null)
		{
			
			mData = uiData as UITestDataPanelData ?? new UITestDataPanelData();
			// please add init code here
			�����Ƶ�������.onEndEdit.AddListener((str) => {
                if (float.TryParse(str, out floatValue))
                {
                    if (floatValue < 0.02f)
                    {
						�����Ƶ�ʽ�����.value = floatValue;
                        DataMgr.AI����Ƶ�� = floatValue;
                    }
                    else
                    {
                        WaringText.text=("��ֵ��С��0.02");
						�����Ƶ�������.text = �����Ƶ�ʽ�����.value.ToString();
                    }
                }
                else
                {
                    WaringText.text = ("�ַ���������Ч�ĸ�����");
                    �����Ƶ�������.text = �����Ƶ�ʽ�����.value.ToString();

                }
            });
            Y���������.onEndEdit.AddListener((str) => {
                if (int.TryParse(str, out intValue))
                {
                    if (intValue < 500f&& intValue>0)
                    {
                        Y���������.value = floatValue;
                        DataMgr.������Y��Χ = floatValue;
                    }
                    else
                    {
                        WaringText.text = ("��ֵ��С�ڴ���500��С��1");
                        Y���������.text = Y���������.value.ToString();
                    }
                }
                else
                {
                    WaringText.text = ("��ֵ��С�ڴ���500��С��1");
                    Y���������.text = Y���������.value.ToString();

                }
            });
            Y���������.onValueChanged.AddListener((str) => {
                if (str < 1f)
                {
                    �����Ƶ�ʽ�����.value = 1f;
                    WaringText.text = ("��ֵ��С�ڴ���500��С��1");

                    return;
                }
                Y���������.text = str.ToString();
                DataMgr.������Y��Χ = str;
            });
            �����Ƶ�ʽ�����.onValueChanged.AddListener((str) => {
                if (str < 0.02f)
                {
                    �����Ƶ�ʽ�����.value = 0.02f;
                    WaringText.text = ("��ֵ��С��0.02");
                    return;
                }
                �����Ƶ�������.text = str.ToString();
                DataMgr.AI����Ƶ�� = str;
            });
            //Close.onClick.AddListener(() => {
            //    if (PseudoRandomGenerator.ResertData())
            //    {
            //        if (AIManager.Instance.ResetData())
            //        {
            //            CloseSelf();
            //        }
            //        else
            //        {
            //            HintManager.Instance.ShowHint("������Ϣ���ʽ�޸Ĵ����������޸�");
            //        }
            //    }
            //    else
            //    {
            //        HintManager.Instance.ShowHint("����׶α��ʽ�޸Ĵ����������޸�");

            //    }



            //});
            �����Ƶ�������.text = DataMgr.AI����Ƶ��.ToString();
            �����Ƶ�ʽ�����.value = DataMgr.AI����Ƶ��;
            Y���������.text = DataMgr.������Y��Χ.ToString();
            Y���������.value = DataMgr.������Y��Χ;
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
