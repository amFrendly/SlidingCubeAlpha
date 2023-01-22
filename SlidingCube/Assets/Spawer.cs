using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Spawer : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] public List<GameObject> platforms;
    [NonSerialized] public int platformsAmount = 0;
    Vector3 nextSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnPos = transform.position;
        CreatePlatform(10, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateBridge()
    {
        if (platforms.Count < 2) return;

        Transform platformA = platforms[platforms.Count - 2].transform;
        Transform platformB = platforms[platforms.Count - 1].transform;
        GameObject cube = Instantiate(this.cube, platformA);
        Vector3 direction = platformB.position - platformA.position;
        cube.transform.localScale = new Vector3(1 / platformA.localScale.x, 1 / platformA.localScale.y, (direction.magnitude / 2) / platformA.localScale.z);
        cube.transform.position = (platformA.position + direction / 2);
    }

    public void CreatePlatform(int x, int z)
    {
        GameObject cube = Instantiate(this.cube, transform);
        cube.transform.localScale = new Vector3(x, 1, z);
        cube.transform.position = nextSpawnPos;
        cube.name = (platformsAmount + 1).ToString();
        nextSpawnPos.z += z * 2;
        platforms.Add(cube);

        platformsAmount++;
    }

    public void RemovePlatforms()
    {
        if (platformsAmount <= 2) return;

        for (int i = 0; i < platforms.Count - 3; i++)
        {
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
        }
    }
}
