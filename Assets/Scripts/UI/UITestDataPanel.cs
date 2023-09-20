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
        string strValue = "0.01"; // 举例的字符串值
        float floatValue;
        int intValue;
        protected override void OnInit(IUIData uiData = null)
		{
			
			mData = uiData as UITestDataPanelData ?? new UITestDataPanelData();
			// please add init code here
			鱼出现频率输入框.onEndEdit.AddListener((str) => {
                if (float.TryParse(str, out floatValue))
                {
                    if (floatValue < 0.02f)
                    {
						鱼出现频率进度条.value = floatValue;
                        DataMgr.AI产生频率 = floatValue;
                    }
                    else
                    {
                        WaringText.text=("数值不小于0.02");
						鱼出现频率输入框.text = 鱼出现频率进度条.value.ToString();
                    }
                }
                else
                {
                    WaringText.text = ("字符串不是有效的浮点数");
                    鱼出现频率输入框.text = 鱼出现频率进度条.value.ToString();

                }
            });
            Y区间输入框.onEndEdit.AddListener((str) => {
                if (int.TryParse(str, out intValue))
                {
                    if (intValue < 500f&& intValue>0)
                    {
                        Y区间进度条.value = floatValue;
                        DataMgr.鱼生成Y范围 = floatValue;
                    }
                    else
                    {
                        WaringText.text = ("数值不小于大于500和小于1");
                        Y区间输入框.text = Y区间进度条.value.ToString();
                    }
                }
                else
                {
                    WaringText.text = ("数值不小于大于500和小于1");
                    Y区间输入框.text = Y区间进度条.value.ToString();

                }
            });
            Y区间进度条.onValueChanged.AddListener((str) => {
                if (str < 1f)
                {
                    鱼出现频率进度条.value = 1f;
                    WaringText.text = ("数值不小于大于500和小于1");

                    return;
                }
                Y区间输入框.text = str.ToString();
                DataMgr.鱼生成Y范围 = str;
            });
            鱼出现频率进度条.onValueChanged.AddListener((str) => {
                if (str < 0.02f)
                {
                    鱼出现频率进度条.value = 0.02f;
                    WaringText.text = ("数值不小于0.02");
                    return;
                }
                鱼出现频率输入框.text = str.ToString();
                DataMgr.AI产生频率 = str;
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
            //            HintManager.Instance.ShowHint("鱼类信息表格式修改错误，请重新修改");
            //        }
            //    }
            //    else
            //    {
            //        HintManager.Instance.ShowHint("鱼类阶段表格式修改错误，请重新修改");

            //    }



            //});
            鱼出现频率输入框.text = DataMgr.AI产生频率.ToString();
            鱼出现频率进度条.value = DataMgr.AI产生频率;
            Y区间输入框.text = DataMgr.鱼生成Y范围.ToString();
            Y区间进度条.value = DataMgr.鱼生成Y范围;
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
