using System.Threading.Tasks;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public async void SpawnObjectsWithinArea(int spawnCount) {
        await WaitUntilPoolManagerDoneLoading();
        for (int i = 0; i < spawnCount; i++) {
            Vector3 spawnPosition = RandomPointInArea(transform.position, GameConfig.SpawnArea.x, GameConfig.SpawnArea.y, GameConfig.SpawnArea.z);

            GameObject ballGO = GameManager.Instance.PoolManager.GetABall();
            if (ballGO != null) {
                ballGO.transform.position = spawnPosition;
                ballGO.transform.rotation = Quaternion.identity;
                ballGO.transform.localScale = new Vector3(GameConfig.BallSize, GameConfig.BallSize, GameConfig.BallSize);
                ballGO.SetActive(true);
            }
        }
    }

    private async Task WaitUntilPoolManagerDoneLoading() {
        while (!GameManager.Instance.PoolManager.IsDoneLoading) {
            await Task.Delay(100);
        }
    }

    private Vector3 RandomPointInArea(Vector3 center, float width, float height, float depth) {
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;
        float halfDepth = depth / 2f;

        float x = Random.Range(center.x - halfWidth, center.x + halfWidth);
        float y = Random.Range(center.y - halfHeight, center.y + halfHeight);
        float z = Random.Range(center.z - halfDepth, center.z + halfDepth);

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(GameConfig.SpawnArea.x, GameConfig.SpawnArea.y, GameConfig.SpawnArea.z));
    }
}
