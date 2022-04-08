using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{

    public GameObject BaseEnemy;
    public GameObject BossEnemy;
    public GameObject MiniEnemy;
    public GameObject GoldEnemy;

    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();

    public float Delay = 2f;

    public GameObject[] SpawnPoints;

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        if (FindObjectOfType<Player_Movement_Script>().isPaused != true)
        {
            yield return new WaitForSecondsRealtime(Delay);

            int SpawnType = Random.Range(1, 100);

            if (SpawnType <= 25)
            {
                int Number = Random.Range(2, 5);
                int Where = Random.Range(0, SpawnPoints.Length);
                for (int i = 0; i < Number; i++)
                {
                    Vector3 Pos = new Vector2(SpawnPoints[Where].transform.position.x + Random.Range(-4, 4), SpawnPoints[Where].transform.position.y + Random.Range(-4, 4));
                    Instantiate(BaseEnemy, Pos, Quaternion.identity);
                }
            }
            else if (SpawnType > 25 && SpawnType <= 50)
            {
                int Number = Random.Range(1, 2);
                int Where = Random.Range(0, SpawnPoints.Length);
                for (int i = 0; i < Number; i++)
                {
                    Vector3 Pos = new Vector2(SpawnPoints[Where].transform.position.x + Random.Range(-4, 4), SpawnPoints[Where].transform.position.y + Random.Range(-4, 4));
                    Instantiate(BossEnemy, Pos, Quaternion.identity);
                }
            }
            else if (SpawnType > 50 && SpawnType <= 90)
            {
                int Number = Random.Range(3, 7);
                int Where = Random.Range(0, SpawnPoints.Length);
                for (int i = 0; i < Number; i++)
                {
                    Vector3 Posi = new Vector2(SpawnPoints[Where].transform.position.x + Random.Range(-4, 4), SpawnPoints[Where].transform.position.y + Random.Range(-4, 4));
                    Instantiate(MiniEnemy, Posi, Quaternion.identity);
                }
            }
            else if (SpawnType > 90)
            {
                int Where = Random.Range(0, SpawnPoints.Length);
                Vector3 Pos = new Vector2(SpawnPoints[Where].transform.position.x + Random.Range(-4, 4), SpawnPoints[Where].transform.position.y + Random.Range(-4, 4));
                Instantiate(GoldEnemy, Pos, Quaternion.identity);
            }
        }
        AttackQueue.Enqueue(SpawnEnemy());
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (AttackQueue.Count > 0)
                yield return StartCoroutine(AttackQueue.Dequeue());
            yield return null;
        }
    }
}
