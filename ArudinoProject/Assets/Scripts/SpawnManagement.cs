using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagement : MonoBehaviour
{
    public GameObject enemy;
    public float[] spawns;
    public Transform[] destis;
    public Queue<float> spawnTimes = new Queue<float>();
    public float timer;
    public Transform spawnPoint;
    public GameManager gm;

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnTimes.Enqueue(spawns[i]);
        }
    }
    void Update()
    {
        if(spawnTimes.Count != 0)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTimes.Peek())
            {
                spawnTimes.Dequeue();
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        GameObject ene;
        ene = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        ene.GetComponent<Enemy>().gm = gm;
        for(int i = 0; i < destis.Length; i++)
        {
            ene.GetComponent<Enemy>().destinations[i] = destis[i];
        }
    }
}
