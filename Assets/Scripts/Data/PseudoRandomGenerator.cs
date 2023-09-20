//using System;
//using System.Collections.Generic;

//using UnityEngine;

//public static class PseudoRandomGenerator
//{
//    // 定义三个随机数池分别对应三个阶段
//    private static List<int> phaseOnePool = new List<int>();
//    private static List<int> phaseTwoPool = new List<int>();
//    private static List<int> phaseThreePool = new List<int>();
//    private static Dictionary<string, List<int>> 阶段配置表 = new Dictionary<string, List<int>>();
//    // 用于生成随机数的对象
//    private static System.Random rnd = new System.Random();
//    private static List<阶段配置> l阶段配置列表 = new List<阶段配置>();
//    // 静态构造函数，用于初始化三个随机数池
//    static PseudoRandomGenerator()
//    {
//        initData();
//        InitializePhaseOnePool();
//        InitializePhaseTwoPool();
//        InitializePhaseThreePool();
//    }
//    static void initData()
//    {
//        Debug.LogError(Resources.Load<TextAsset>("DataText/阶段").text);
//        l阶段配置列表= Newtonsoft.Json.JsonConvert.DeserializeObject<List<阶段配置>>(Resources.Load<TextAsset>("DataText/阶段").text);
//    }
//    public static bool ResertData()
//    {
//        l阶段配置列表 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<阶段配置>>(Resources.Load<TextAsset>("DataText/阶段").text);
//        if (l阶段配置列表 == null || l阶段配置列表.Count == 0)
//        {
//            return false;
//        }
//        return true;
//    }
//    /// <summary>
//    /// 初始化第一阶段的随机数池
//    /// </summary>
//    private static void InitializePhaseOnePool()
//    {
//        // 根据规定的概率添加随机数到池中
//        for (int i = 0; i < l阶段配置列表[0].阶段数[0]; i++)
//            phaseOnePool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l阶段配置列表[0].阶段数[1]; i++)
//            phaseOnePool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l阶段配置列表[0].阶段数[2]; i++)
//        {
//            Debug.LogError("进入一次循环?" + l阶段配置列表[0].阶段数[2]);
//            phaseOnePool.Add(rnd.Next(13, 16));

//        }
//        // 对池中的数进行洗牌，以确保随机性
//        Shuffle(phaseOnePool);
//    }

//    /// <summary>
//    /// 初始化第二阶段的随机数池
//    /// </summary>
//    private static void InitializePhaseTwoPool()
//    {
//        // 根据规定的概率添加随机数到池中
//        for (int i = 0; i < l阶段配置列表[1].阶段数[0]; i++)
//            phaseTwoPool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l阶段配置列表[1].阶段数[1]; i++)
//            phaseTwoPool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l阶段配置列表[1].阶段数[2]; i++)
//            phaseTwoPool.Add(rnd.Next(13, 16));

//        Shuffle(phaseTwoPool);
//    }

//    /// <summary>
//    /// 初始化第三阶段的随机数池
//    /// </summary>
//    private static void InitializePhaseThreePool()
//    {
//        // 根据规定的概率添加随机数到池中
//        for (int i = 0; i < l阶段配置列表[2].阶段数[0]; i++)
//            phaseThreePool.Add(rnd.Next(0, 9));
//        for (int i = 0; i < l阶段配置列表[2].阶段数[1]; i++)
//            phaseThreePool.Add(rnd.Next(9, 13));
//        for (int i = 0; i < l阶段配置列表[2].阶段数[2]; i++)
//            phaseThreePool.Add(rnd.Next(13, 16));

//        Shuffle(phaseThreePool);
//    }
//    public static int GetNowRandomFromPhase()
//    {
//        switch (DataMgr.当前阶段)
//        {
//            case 1: return GetRandomFromPhaseOne();
//            case 2: return GetRandomFromPhaseTwo();
//            case 3: return GetRandomFromPhaseThree();
//            default: return GetRandomFromPhaseThree();
//        }
//    }
//    /// <summary>
//    /// 从第一阶段的池中取得一个随机数
//    /// </summary>
//    /// <returns>返回一个随机数</returns>
//    public static int GetRandomFromPhaseOne()
//    {
//        if (phaseOnePool.Count == 0)
//            InitializePhaseOnePool();
//        int randomValue = phaseOnePool[0];
//        phaseOnePool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// 从第二阶段的池中取得一个随机数
//    /// </summary>
//    /// <returns>返回一个随机数</returns>
//    public static int GetRandomFromPhaseTwo()
//    {
//        if (phaseTwoPool.Count == 0)
//            InitializePhaseTwoPool();
//        int randomValue = phaseTwoPool[0];
//        phaseTwoPool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// 从第三阶段的池中取得一个随机数
//    /// </summary>
//    /// <returns>返回一个随机数</returns>
//    public static int GetRandomFromPhaseThree()
//    {
//        if (phaseThreePool.Count == 0)
//            InitializePhaseThreePool();
//        int randomValue = phaseThreePool[0];
//        phaseThreePool.RemoveAt(0);
//        return randomValue;
//    }

//    /// <summary>
//    /// 对给定的列表进行洗牌
//    /// </summary>
//    /// <param name="list">要被洗牌的列表</param>
//    private static void Shuffle<T>(List<T> list)
//    {
//        int n = list.Count;

//        // 洗牌算法
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
