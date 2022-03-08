using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{

    public GameObject Enemy;

    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSecondsRealtime(1f);
        Instantiate(Enemy, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
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
