//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using Mediapipe.Unity.HandTracking;
//using Mediapipe.Unity;
//using Mediapipe;


//    public class GestureDetector : MonoBehaviour
//    {
//        [SerializeField] private HandTrackingSolution handTrackingSolution;

//        private Action<object, OutputEventArgs<List<NormalizedLandmarkList>>> handLandmarksOutputDelegate;

//        private void OnEnable()
//        {
//            if (handTrackingSolution != null)
//            {
//                // �� OnHandLandmarksOutput ������װ��ί����
//                handLandmarksOutputDelegate = (sender, eventArgs) => OnHandLandmarksOutput(sender, eventArgs);
//                handTrackingSolution.graphRunner += OnHandLandmarksOutput;
//            }
//            else
//            {
//                Debug.LogError("Hand Tracking Solution is not assigned!");
//            }
//        }

//        private void OnDisable()
//        {
//            if (handTrackingSolution != null)
//            {
//                handTrackingSolution.OnHandLandmarksOutput -= handLandmarksOutputDelegate;
//            }
//        }

//        private void OnHandLandmarksOutput(object stream, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
//        {
//            // �����ﴦ���ֲ��ؼ����������
//        }
//    }

