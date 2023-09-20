//using System;
//using System.Collections.Generic;

//using UnityEngine;

//public static class PseudoRandomGenerator
//{
//    // ��������������طֱ��Ӧ�����׶�
//    private static List<int> phaseOnePool = new List<int>();
//    private static List<int> phaseTwoPool = new List<int>();
//    private static List<int> phaseThreePool = new List<int>();
//    private static Dictionary<string, List<int>> �׶����ñ� = new Dictionary<string, List<int>>();
//    // ��������������Ķ���
//    private static System.Random rnd = new System.Random();
//    private static List<�׶�����> l�׶������б� = new List<�׶�����>();
//    // ��̬���캯�������ڳ�ʼ�������������
//    static PseudoRandomGenerator()
//    {
//        initData();
//        InitializePhaseOnePool();
//        InitializePhaseTwoPool();
//        InitializePhaseThreePool();
//    }
//    static void initData()
//    {
//        Debug.LogError(Resources.Load<TextAsset>("DataText/�׶�").text);
//        l�׶������б�= Newtonsoft.Json.JsonConvert.DeserializeObject<List<�׶�����>>(Resources.Load<TextAsset>("DataText/�׶�").text);
//    }
//    public static bool ResertData()
//    {
//        l�׶������б� = Newtonsoft.Json.JsonConvert.DeserializeObject<List<�׶�����>>(Resources.Load<TextAsset>("DataText/�׶�").text);
//        if (l�׶������б� == null || l�׶������б�.Count == 0)
//        {
//            return false;
//        }
//        return true;
//    }
//    /// <summary>
//    /// ��ʼ����һ�׶ε��������
//    /// </summary>
//    private static void InitializePhaseOnePool()
//    {
//        // ���ݹ涨�ĸ�����������������
//        for (int i = 0; i < l�׶������б�[0].�׶���[0]; i++)
//            phaseOnePool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l�׶������б�[0].�׶���[1]; i++)
//            phaseOnePool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l�׶������б�[0].�׶���[2]; i++)
//        {
//            Debug.LogError("����һ��ѭ��?" + l�׶������б�[0].�׶���[2]);
//            phaseOnePool.Add(rnd.Next(13, 16));

//        }
//        // �Գ��е�������ϴ�ƣ���ȷ�������
//        Shuffle(phaseOnePool);
//    }

//    /// <summary>
//    /// ��ʼ���ڶ��׶ε��������
//    /// </summary>
//    private static void InitializePhaseTwoPool()
//    {
//        // ���ݹ涨�ĸ�����������������
//        for (int i = 0; i < l�׶������б�[1].�׶���[0]; i++)
//            phaseTwoPool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l�׶������б�[1].�׶���[1]; i++)
//            phaseTwoPool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l�׶������б�[1].�׶���[2]; i++)
//            phaseTwoPool.Add(rnd.Next(13, 16));

//        Shuffle(phaseTwoPool);
//    }

//    /// <summary>
//    /// ��ʼ�������׶ε��������
//    /// </summary>
//    private static void InitializePhaseThreePool()
//    {
//        // ���ݹ涨�ĸ�����������������
//        for (int i = 0; i < l�׶������б�[2].�׶���[0]; i++)
//            phaseThreePool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l�׶������б�[2].�׶���[1]; i++)
//            phaseThreePool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l�׶������б�[2].�׶���[2]; i++)
//            phaseThreePool.Add(rnd.Next(13, 16));

//        Shuffle(phaseThreePool);
//    }
//    public static int GetNowRandomFromPhase()
//    {
//        switch (DataMgr.��ǰ�׶�)
//        {
//            case 1: return GetRandomFromPhaseOne();
//            case 2: return GetRandomFromPhaseTwo();
//            case 3: return GetRandomFromPhaseThree();
//            default: return GetRandomFromPhaseThree();
//        }
//    }
//    /// <summary>
//    /// �ӵ�һ�׶εĳ���ȡ��һ�������
//    /// </summary>
//    /// <returns>����һ�������</returns>
//    public static int GetRandomFromPhaseOne()
//    {
//        if (phaseOnePool.Count == 0)
//            InitializePhaseOnePool();
//        int randomValue = phaseOnePool[0];
//        phaseOnePool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// �ӵڶ��׶εĳ���ȡ��һ�������
//    /// </summary>
//    /// <returns>����һ�������</returns>
//    public static int GetRandomFromPhaseTwo()
//    {
//        if (phaseTwoPool.Count == 0)
//            InitializePhaseTwoPool();
//        int randomValue = phaseTwoPool[0];
//        phaseTwoPool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// �ӵ����׶εĳ���ȡ��һ�������
//    /// </summary>
//    /// <returns>����һ�������</returns>
//    public static int GetRandomFromPhaseThree()
//    {
//        if (phaseThreePool.Count == 0)
//            InitializePhaseThreePool();
//        int randomValue = phaseThreePool[0];
//        phaseThreePool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// �Ը������б����ϴ��
//    /// </summary>
//    /// <param name="list">Ҫ��ϴ�Ƶ��б�</param>
//    private static void Shuffle<T>(List<T> list)
//    {
//        int n = list.Count;

//        // ϴ���㷨
//        while (n > 1)
//        {
//            n--;
//            int k = rnd.Next(n + 1);
//            T value = list[k];
//            list[k] = list[n];
//            list[n] = value;
//        }
//    }
//}
