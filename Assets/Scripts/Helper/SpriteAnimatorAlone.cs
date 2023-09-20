using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace QFramework
{
    /// <summary>
    /// �������ſؼ�
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimatorAlone : MonoBehaviour
    {
        private Image mImageSource;
        private SpriteRenderer mSpriteRenderer;
        private int mCurFrame = 0;
        private float mDelta = 0;

        public float FPS = 5;
        public List<Sprite> SpriteFrames;
        public bool IsPlaying = false;
        public bool Forward = true;
        public bool AutoPlay = false;
        public bool Loop = false;
        private bool �Ƿ���Image���;
        public int sortingOrder = 109;
        // ����һ��ö������
        public enum MyOptions
        {
            Image,
            SpriteRenderer
        }

        // ������Inspector����ʾ��ѡ���SerializedProperty
        [SerializeField]
        private MyOptions ʹ��ʲô�������������֡ = MyOptions.SpriteRenderer;
        public int FrameCount
        {
            get { return SpriteFrames.Count; }
        }

        void Awake()
        {
            switch (ʹ��ʲô�������������֡)
            {
                case MyOptions.Image:
                    mImageSource = GetComponent<Image>();
                    if (GetComponent<SpriteRenderer>() != null)
                    {
                        Destroy(GetComponent<SpriteRenderer>());
                    }
                    break;
                case MyOptions.SpriteRenderer:
                    mSpriteRenderer = GetComponent<SpriteRenderer>();
                    mSpriteRenderer.sortingOrder = sortingOrder+ GetComponent<SpriteRenderer>().sortingOrder;
                    if (GetComponent<Image>() != null)
                    {
                        Destroy(GetComponent<Image>());
                    }
                    break;
            }
        }

        void Start()
        {
            if (AutoPlay)
            {
                Play();
            }
            else
            {
                IsPlaying = false;
            }
        }

        private void SetSprite(int idx)
        {
            if (ʹ��ʲô�������������֡==MyOptions.Image)
            {
                mImageSource.sprite = SpriteFrames[idx];
                mImageSource.SetNativeSize();
            }
            else
            {
                mSpriteRenderer.sprite = SpriteFrames[idx];
            }

        }

        public void Play()
        {
            IsPlaying = true;
            Forward = true;
        }

        public void PlayReverse()
        {
            IsPlaying = true;
            Forward = false;
        }

        void Update()
        {
            if (!IsPlaying || 0 == FrameCount)
            {
                return;
            }
            mDelta += Time.unscaledDeltaTime;
            if (mDelta > 1 / FPS)
            {
                mDelta = 0;
                if (Forward)
                {
                    mCurFrame++;
                }
                else
                {
                    mCurFrame--;
                }

                if (mCurFrame >= FrameCount)
                {
                    if (Loop)
                    {
                        mCurFrame = 0;
                    }
                    else
                    {
                        IsPlaying = false;
                        return;
                    }
                }
                else if (mCurFrame < 0)
                {
                    if (Loop)
                    {
                        mCurFrame = FrameCount - 1;
                    }
                    else
                    {
                        IsPlaying = false;
                        return;
                    }
                }

                SetSprite(mCurFrame);
            }
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Resume()
        {
            if (!IsPlaying)
            {
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            IsPlaying = false;
        }

        public void Rewind()
        {
            mCurFrame = 0;
            SetSprite(mCurFrame);
            Play();
        }
    }
}