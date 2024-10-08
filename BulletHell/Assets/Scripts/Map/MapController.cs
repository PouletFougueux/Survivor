using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float radius;
    Vector3 noMapPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    playerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCd;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
         if(pm.moveDirection.x > 0 && pm.moveDirection.y == 0) // right
         {

            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
         }
        else if (pm.moveDirection.x < 0 && pm.moveDirection.y == 0) // left
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x == 0 && pm.moveDirection.y > 0) // up
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x == 0 && pm.moveDirection.y < 0) // down
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x > 0 && pm.moveDirection.y > 0) // right up
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("UpRight").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("UpRight").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x > 0 && pm.moveDirection.y < 0) // right down
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("DownRight").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("DownRight").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x < 0 && pm.moveDirection.y > 0) // left up
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("UpLeft").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("UpLeft").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDirection.x < 0 && pm.moveDirection.y < 0) // left down
        {

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("DownLeft").position, radius, terrainMask))
            {
                noMapPosition = currentChunk.transform.Find("DownLeft").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int r = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[r], noMapPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
     void ChunkOptimizer()
    {
        // Loop to optimize every x seconds
        optimizerCd -= Time.deltaTime;
        if (optimizerCd <= 0f)
        {
            optimizerCd = cooldown;
        }
        else
            return;


        foreach (var chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
                chunk.SetActive(true);
        }
    }
}
