using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Manager : MonoBehaviour
{
    // New array of game objects made up of roadPrefabs
    public GameObject[] roadPrefabs;

    private List<GameObject> activeRoads = new List<GameObject>();

    // The z position of the roadPrefabs
    public float zSpawnLoc = 0;
    public float roadLength = 30;
    // Number of Roads
    public int numberOfRoads = 5;
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
        if (playerTransform.position.z - 35 > zSpawnLoc-(numberOfRoads * roadLength))
        {
            SpawnTile(Random.Range(0, roadPrefabs.Length));
            DeleteRoad();
        }
    }

    public void SpawnTile(int roadIdex)
    {
        GameObject gameObj = Instantiate(roadPrefabs[roadIdex], transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(gameObj);
        zSpawnLoc += roadLength;
    }

    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

}
