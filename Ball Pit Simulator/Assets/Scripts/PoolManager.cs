using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolManager : MonoBehaviour {
    public Transform container;
    public bool IsDoneLoading { get => pool.Count >= GameConfig.PoolSize; }

    private Queue<GameObject> pool;
    private Queue<GameObject> activeBalls;
    private List<AsyncOperationHandle<GameObject>> ballHandles;

    public void Start() {
        pool = new Queue<GameObject>();
        activeBalls = new Queue<GameObject>();
        ballHandles = new List<AsyncOperationHandle<GameObject>>();
         
        var ballHandle = GameConfig.BallPrefabReference.LoadAssetAsync<GameObject>();
        ballHandle.Completed += OnPrefabLoaded;
        ballHandles.Add(ballHandle);
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> ballHandle) {
        if (ballHandle.Status == AsyncOperationStatus.Succeeded) {
            GameObject ballPrefab = ballHandle.Result;
            for (int i = 0; i < GameConfig.PoolSize; i++) {
                GameObject ballGO = Instantiate(ballPrefab, container);
                ballGO.name = $"Ball_{i+1}";
                ballGO.SetActive(false);

                Renderer ballRenderer = ballGO.GetComponent<MeshRenderer>();
                ballRenderer.material = GameConfig.GetRandomColor();

                pool.Enqueue(ballGO);
            }
        }
    }

    public GameObject GetABall() {
        if (pool.Count > 0) {
            GameObject ball = pool.Dequeue();
            ball.SetActive(true);
            activeBalls.Enqueue(ball);
            return ball;
        } else {
            return null;
        }
    }

    public void RemoveABall(GameObject ball) {
        ball.SetActive(false);
        pool.Enqueue(ball);
    }

    public void RemoveAllBalls() {
        List<GameObject> ballsToDeactivate = new List<GameObject>();

        while (activeBalls.Count > 0) {
            ballsToDeactivate.Add(activeBalls.Dequeue());
        }

        foreach (var ball in ballsToDeactivate) {
            ball.SetActive(false);
            pool.Enqueue(ball);
        }
    }

    private void OnDestroy() {
        foreach (var ballHandle in ballHandles) {
            Addressables.Release(ballHandle);
        }
    }
}
