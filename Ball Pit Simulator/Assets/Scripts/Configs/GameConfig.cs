using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Custom/Game Config")]
public class GameConfig : ScriptableObject {
    private static GameConfig instance;
    public static GameConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<GameConfig>("Configs/GameConfig");
            }
            return instance;
        }
    }

    [MenuItem("Tools/Game Configurations")]
    private static void PingGameConfig() {
        Selection.activeObject = Instance;
        EditorGUIUtility.PingObject(Instance);
    }

    [SerializeField] private int poolSize = 200;
    [SerializeField] private float ballSize = 0.2f;
    [SerializeField] private float playerWeight = 100;
    [SerializeField] private Vector3 spawnArea;
    [SerializeField] private AssetReference ballPrefabReference;
    [SerializeField] private List<Material> ballColorMaterials;

    static public int PoolSize { get => Instance.poolSize; set => Instance.poolSize = value; }
    static public float BallSize { get => Instance.ballSize; set => Instance.ballSize = value; }
    static public float PlayerWeight { get => Instance.playerWeight; set => Instance.playerWeight = value; }
    static public Vector3 SpawnArea { get => Instance.spawnArea; set => Instance.spawnArea = value; }
    static public AssetReference BallPrefabReference { get => Instance.ballPrefabReference; set => Instance.ballPrefabReference = value; }
    static public List<Material> BallColorMaterials { get => Instance.ballColorMaterials; set => Instance.ballColorMaterials = value; }

    static public Material GetRandomColor() {
        return BallColorMaterials[UnityEngine.Random.Range((int)0, (int)BallColorMaterials.Count)];
    }
}