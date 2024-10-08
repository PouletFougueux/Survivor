using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach(GameObject o in propSpawnPoints)
        {
            int r = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[r], o.transform.position, Quaternion.identity);
            prop.transform.parent = o.transform;
        }
    }
}
