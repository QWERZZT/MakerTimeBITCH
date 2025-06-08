using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // Префаб врага
    public Transform[] spawnPoints;        // Точки спавна
    public float spawnInterval = 5f;       // Интервал между спавнами

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        // Выбор случайной точки спавна
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        // Спавн врага
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
