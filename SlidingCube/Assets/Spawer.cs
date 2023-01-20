using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawer : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] List<GameObject> platforms;
    [SerializeField] bool spawned = false;
    public int platformsAmount { get { return platforms.Count -1; } }
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
        GameObject cube = Instantiate(this.cube);
        Vector3 direction = platformB.position - platformA.position;
        cube.name = (platforms.Count - 1).ToString();
        cube.transform.localScale = new Vector3(1, 1, direction.magnitude);
        cube.transform.position = (platformB.position + platformA.position) / 2;
        platforms.Add(cube);
    }

    public void CreatePlatform(int x, int z)
    {
        GameObject cube = Instantiate(this.cube);
        cube.transform.localScale = new Vector3(x, 1, z);
        cube.transform.position = nextSpawnPos;
        nextSpawnPos.z += z * 2;
        platforms.Add(cube);
    }
}
