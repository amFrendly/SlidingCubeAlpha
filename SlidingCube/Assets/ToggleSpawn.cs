using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpawn : MonoBehaviour
{
    [SerializeField] Spawer spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == spawner.platformsAmount.ToString())
        {
            spawner.CreatePlatform(10, 10);
            spawner.CreateBridge();
        }
    }
}
