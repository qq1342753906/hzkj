using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace QFramework
{
    [RequireComponent(typeof(Image))]
    public class UISpriteAnimation : MonoBehaviour
    {
        // 动画状态枚举
        public enum AnimationState
        {
            NormalWalk, // 正常行走动画
            Attack      // 攻击动画
        }

        private Image mImageSource;
        private int mCurFrame = 0;
        private float mDelta = 0;

        public float FPS = 5;
        public List<Sprite> NormalWalkFrames; // 正常行走动画序列帧
        public List<Sprite> AttackFrames;     // 攻击动画序列帧
        public bool IsPlaying = false;
        public bool Forward = true;
        public bool AutoPlay = false;
        public bool Loop = false;

        public int FrameCount
        {
            get { return GetActiveFrames().Count; }
        }

        public AnimationState CurrentAnimationState { get; private set; } = AnimationState.NormalWalk;

        void Awake()
        {
            mImageSource = GetComponent<Image>();
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
            mImageSource.sprite = GetActiveFrames()[idx];
            mCurFrame = idx;
            //mImageSource.SetNativeSize();
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

            mDelta += Time.deltaTime;
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
                        if (CurrentAnimationState == AnimationState.Attack)
                        {
                            SwitchToNormalWalkAnimation(); // 攻击动画播放完毕，切换到正常行走动画
                        }
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

        // 切换到正常行走动画
        public void SwitchToNormalWalkAnimation()
        {
            CurrentAnimationState = AnimationState.NormalWalk;
            SetSprite(0);
        }

        // 切换到攻击动画
        public void SwitchToAttackAnimation()
        {
            CurrentAnimationState = AnimationState.Attack;
            SetSprite(0);
        }

        // 获取当前活动动画序列帧
        private List<Sprite> GetActiveFrames()
        {
            return CurrentAnimationState == AnimationState.NormalWalk ? NormalWalkFrames : AttackFrames;
        }
    }
}
