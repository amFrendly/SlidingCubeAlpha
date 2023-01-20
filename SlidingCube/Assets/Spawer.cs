using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawer : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] bool spawned = false;

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
        if(spawned == false)
        {
            Debug.Log("Spawned");
            platform = Instantiate(platform);
            platform.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z);
            spawned = true;
        }
    }
}
