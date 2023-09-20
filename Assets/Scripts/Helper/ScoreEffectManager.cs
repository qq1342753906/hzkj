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
        scoreText.DOFade(0, 1.5f); // ��������Ч�𽥵���
        EffectBg.DOFade(0, 1f);
        EffectBg.gameObject.transform.localScale = Vector3.zero;
        EffectBg.gameObject.transform.DOScale(Vector3.one, 1f);
        float posY = 100;

        if (spawnPosition.y >= GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y/2-100)
        {
            posY = 0;
        }
        scoreText.GetComponent<RectTransform>().DOAnchorPos3DY(posY, 1.5f).OnComplete(() => Destroy(gameObject)); // ��������Ч�����ƶ�������ɺ�����
    }

    public void ShowAttackEffect(int AttackValue, Vector3 spawnPosition)
    {
        GetComponent<RectTransform>().anchoredPosition3D = spawnPosition;
        scoreText.text = "-" + AttackValue.ToString();
        scoreText.color = Color.red;
        scoreText.DOFade(0, 1.5f); // ��������Ч�𽥵���
        EffectBg.DOFade(0, 1f);
        EffectBg.color = Color.red;
        EffectBg.gameObject.transform.localScale = Vector3.zero;
        EffectBg.gameObject.transform.DOScale(Vector3.one, 1f);
        float posY = 100;

        if (spawnPosition.y >= GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y / 2 - 100)
        {
            posY = 0;
        }
        scoreText.GetComponent<RectTransform>().DOAnchorPos3DY(posY, 1.5f).OnComplete(() => Destroy(gameObject)); // ��������Ч�����ƶ�������ɺ�����
    }

    public void ͼƬչʾЧ��(Image originalImage,Vector3 pos,Action ac=null)
    {
        // ����һ���µ�Image����
        Image newImage = Instantiate(originalImage, originalImage.transform.parent);

        // ������Image�����������ԭʼImage������ͬ
        newImage.rectTransform.anchoredPosition = pos;
        newImage.rectTransform.sizeDelta = originalImage.rectTransform.sizeDelta;
        newImage.rectTransform.rotation = originalImage.rectTransform.rotation;
        newImage.gameObject.SetActive(true);


        // ����һ������������ƶ��͵�������
        Sequence sequence = DOTween.Sequence();

        sequence.Append(newImage.rectTransform.DOAnchorPos3DY(pos.y+GameObject.FindObjectOfType<UIRoot>().GetComponent<CanvasScaler>().referenceResolution.y/10.0f,1f));
        // ��ӵ�������������
        sequence.Append(newImage.DOFade(0f, 1f));

        // ����һ���ص����ڶ�����ɺ����ٶ���
        sequence.OnComplete(() => {
            if (ac != null)
            {
                ac.Invoke();
            }
            Destroy(newImage.gameObject); });
    }
}
