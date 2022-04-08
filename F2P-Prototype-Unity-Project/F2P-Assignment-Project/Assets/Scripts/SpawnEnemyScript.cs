using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{

    public GameObject BaseEnemy;
    public GameObject BossEnemy;
    public GameObject GoldEnemy;

    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSecondsRealtime(1f);

        int SpawnType = Random.Range(1, 100);

        if(SpawnType <= 111)
        {
            int Number = Random.Range(2, 4);
            for(int i = 0; i < Number; i++)
            {
                Vector3 Pos = new Vector2(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + Random.Range(-4, 4));
                Instantiate(BaseEnemy, Pos, Quaternion.identity);
            }
        }
        else if(SpawnType > 25 && SpawnType <= 50)
        {
            int Number = Random.Range(1, 2);
            for (int i = 0; i < Number; i++)
            {
                Vector3 Pos = new Vector2(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + Random.Range(-4, 4));
                Instantiate(BossEnemy, Pos, Quaternion.identity);
            }
        }
        else if(SpawnType > 50 && SpawnType <= 90)
        {
            int Number = Random.Range(2, 4);
            for (int i = 0; i < Number; i++)
            {
                Vector3 Posi = new Vector2(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + Random.Range(-4, 4));
                Instantiate(BaseEnemy, Posi, Quaternion.identity);
            }

            Vector3 Pos = new Vector2(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + Random.Range(-4, 4));
            Instantiate(BossEnemy, Pos, Quaternion.identity);
        }
        else if(SpawnType > 90)
        {
            Vector3 Pos = new Vector2(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + Random.Range(-4, 4));
            Instantiate(GoldEnemy, Pos, Quaternion.identity);
        }

        Instantiate(BaseEnemy, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
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
