using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using QFramework;
using System;

public class ScoreEffectManager : MonoSingleton<ScoreEffectManager>
{
    public Text scoreText;
    public Image EffectBg;
    public void ShowScoreEffect(int scoreValue, Vector3 spawnPosition)
    {
        GetComponent<RectTransform>().anchoredPosition3D = spawnPosition;
        if (scoreValue <= 0)
        {
            scoreText.text = scoreValue.ToString();

        }
        else
        {
            scoreText.text = "+" + scoreValue.ToString();

        }
        scoreText.DOFade(0, 1.5f); // 将分数特效逐渐淡出
        EffectBg.DOFade(0, 1f);
        EffectBg.gameObject.transform.localScale = Vector3.zero;
        EffectBg.gameObject.transform.DOScale(Vector3.one, 1f);
        float posY = 100;

        if (spawnPosition.y >= GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y/2-100)
        {
            posY = 0;
        }
        scoreText.GetComponent<RectTransform>().DOAnchorPos3DY(posY, 1.5f).OnComplete(() => Destroy(gameObject)); // 将分数特效向上移动并在完成后销毁
    }

    public void ShowAttackEffect(int AttackValue, Vector3 spawnPosition)
    {
        GetComponent<RectTransform>().anchoredPosition3D = spawnPosition;
        scoreText.text = "-" + AttackValue.ToString();
        scoreText.color = Color.red;
        scoreText.DOFade(0, 1.5f); // 将分数特效逐渐淡出
        EffectBg.DOFade(0, 1f);
        EffectBg.color = Color.red;
        EffectBg.gameObject.transform.localScale = Vector3.zero;
        EffectBg.gameObject.transform.DOScale(Vector3.one, 1f);
        float posY = 100;

        if (spawnPosition.y >= GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y / 2 - 100)
        {
            posY = 0;
        }
        scoreText.GetComponent<RectTransform>().DOAnchorPos3DY(posY, 1.5f).OnComplete(() => Destroy(gameObject)); // 将分数特效向上移动并在完成后销毁
    }

    public void 图片展示效果(Image originalImage,Vector3 pos,Action ac=null)
    {
        // 创建一个新的Image对象
        Image newImage = Instantiate(originalImage, originalImage.transform.parent);

        // 设置新Image对象的属性与原始Image对象相同
        newImage.rectTransform.anchoredPosition = pos;
        newImage.rectTransform.sizeDelta = originalImage.rectTransform.sizeDelta;
        newImage.rectTransform.rotation = originalImage.rectTransform.rotation;
        newImage.gameObject.SetActive(true);


        // 创建一个序列来组合移动和淡出动画
        Sequence sequence = DOTween.Sequence();

        sequence.Append(newImage.rectTransform.DOAnchorPos3DY(pos.y+GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y/10.0f,1f));
        // 添加淡出动画到序列
        sequence.Append(newImage.DOFade(0f, 1f));

        // 设置一个回调来在动画完成后销毁对象
        sequence.OnComplete(() => {
            if (ac != null)
            {
                ac.Invoke();
            }
            Destroy(newImage.gameObject); });
    }
}
