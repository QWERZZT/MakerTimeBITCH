using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // ������ �����
    public Transform[] spawnPoints;        // ����� ������
    public float spawnInterval = 5f;       // �������� ����� ��������

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        // ����� ��������� ����� ������
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        // ����� �����
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
