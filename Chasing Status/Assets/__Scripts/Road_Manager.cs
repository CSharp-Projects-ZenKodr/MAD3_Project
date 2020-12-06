using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Manager : MonoBehaviour
{
    // New array of game objects made up of roadPrefabs
    public GameObject[] roadPrefabs;

    // Checkpoint game object
    public GameObject checkPoint;

    private List<GameObject> activeRoads = new List<GameObject>();

    // The z position of the roadPrefabs
    public float zSpawnLoc = 0;
    public float roadLength = 30;

    // Number of Roads
    public int numberOfRoads = 4;
    // Player transform position 
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfRoads; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, roadPrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gets players position z and calculates if less then the all the roads lengths
        if (playerTransform.position.z - 35 > zSpawnLoc-(numberOfRoads * roadLength))
        {
            // Road size is 30 so % 900 will always be a consistent messure
            if (zSpawnLoc % 900 == 0)
            {
                
                SpawnCheckpoint(); // Spawns a checkpoint prefab
                DeleteRoad(); // Deletes old roads
            } else
            {
                SpawnTile(Random.Range(0, roadPrefabs.Length)); // Spawns a random road prefab from a list
                DeleteRoad(); // Deletes old roads
            }
        }
    }

    public void SpawnTile(int roadIdex)
    {
        GameObject gameObj = Instantiate(roadPrefabs[roadIdex], transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(gameObj);
        zSpawnLoc += roadLength;
    }

    public void SpawnCheckpoint()
    {
        GameObject cp = Instantiate(checkPoint, transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(cp);
        zSpawnLoc += roadLength;
    }

    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

}
