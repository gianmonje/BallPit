using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Custom/Input Config")]
public class InputConfig : ScriptableObject {
    private static InputConfig instance;
    public static InputConfig Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<InputConfig>("Configs/InputConfig");
            }
            return instance;
        }
    }

    [MenuItem("Tools/FPS Configurations")]
    private static void PingFPSConfigurations() {
        Selection.activeObject = Instance;
        EditorGUIUtility.PingObject(Instance);
    }

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float mouseSensitivity = 2f;
    [SerializeField] public float pickupRange = 10f;
    [SerializeField] public int pickupCameraDistance = 1;
    [SerializeField] public float ballThrowForce = 10f; // Adjust this value for the throw force

    static public float MoveSpeed { get => Instance.moveSpeed; set => Instance.moveSpeed = value; }
    static public float MouseSensitivity { get => Instance.mouseSensitivity; set => Instance.mouseSensitivity = value; }
    static public float PickupRange { get => Instance.pickupRange; set => Instance.pickupRange = value; }
    static public int PickupCameraDistance { get => Instance.pickupCameraDistance; set => Instance.pickupCameraDistance = value; }
    static public float BallThrowForce { get => Instance.ballThrowForce; set => Instance.ballThrowForce = value; }
}