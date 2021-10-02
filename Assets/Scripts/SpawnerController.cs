using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject Cube;
    public GameObject HalfCube;
    public GameObject Beam;

    private List<GameObject> SpawnList;
    private int SpawnedCount = 0;
    private int ObjectCount = 0;

    private void Start()
    {
        SpawnList = new List<GameObject> {
            Cube,
            HalfCube,
            HalfCube,
            Cube,
            Beam,
            Beam,
        };

        Spawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObjectCount--;

        if (ObjectCount == 0)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject obj = SpawnList[SpawnedCount++ % SpawnList.Count];
        Instantiate(obj, transform);
    }
}
