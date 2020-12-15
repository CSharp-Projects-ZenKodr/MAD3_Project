using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Manager : MonoBehaviour
{
    public static Road_Manager Instance { get; private set; }

    // New array of game objects made up of roadPrefabs
    public GameObject[] roadPrefabs;

    // Checkpoint game object
    public GameObject checkPoint;
    public GameObject startPoint;
    public GameObject powerUp;

    public List<GameObject> activeRoads = new List<GameObject>();

    // The z position of the roadPrefabs
    public float zSpawnLoc = 0;
    public float roadLength = 30;

    // Number of Roads
    public int numberOfRoads = 6;
    // Player transform position 
    public Transform playerTransform;

    public int checkpointCounter;

    // Creates a singleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        checkpointCounter = 0;
        // Creates the first few roads so there is a starting map
        for (int i = 0; i < numberOfRoads; i++)
        {
            if (i == 0)
            {
                // Sets the starting point
                StartPoint();
            }
            else
            {
                // Spawns random tiles
                SpawnTile(Random.Range(0, roadPrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpointCounter > 5)
        {
            // Gets players position z and calculates if less then the all the roads lengths
            if (playerTransform.position.z - 35 > zSpawnLoc - (numberOfRoads * roadLength))
            {
                SpawnTile(Random.Range(0, roadPrefabs.Length));
                DeleteRoad();
            }
        } else {
            // Gets players position z and calculates if less then the all the roads lengths
            if (playerTransform.position.z - 35 > zSpawnLoc - (numberOfRoads * roadLength))
            {
                // Road size is 30 so % 900 will always be a consistent messure
                if (zSpawnLoc % 900 == 0)
                {
                    SpawnCheckpoint(); // Spawns a checkpoint prefab
                    checkpointCounter++;
                    DeleteRoad(); // Deletes old roads
                }
                else if (zSpawnLoc % 1500 == 0)
                {
                    SpawnPowerUp();
                    DeleteRoad();
                }
                else
                {
                    SpawnTile(Random.Range(0, 4)); // Spawns a random road prefab from a list
                    DeleteRoad(); // Deletes old roads
                }
            }
        }
    }

    /**
     * Fucntion takes in an int and randomly spawns a tile to the activeRoads list
     */
    private void SpawnTile(int roadIdex)
    {
        GameObject gameObj = Instantiate(roadPrefabs[roadIdex], transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(gameObj);
        zSpawnLoc += roadLength;
    }

    /**
     * Fucntion spawns a Checkpoint tile to the activeRoads list
     */
    private void SpawnCheckpoint()
    {
        GameObject cp = Instantiate(checkPoint, transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(cp);
        zSpawnLoc += roadLength;
    }

    /**
     * Fucntion spawns a Starting Point tile to the activeRoads list
     */
    private void StartPoint()
    {
        GameObject sp = Instantiate(startPoint, transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(sp);
        zSpawnLoc += roadLength;
    }

    /**
     * Fucntion removes and destroys the last activeRoads tile in the list
     */
    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

    /**
     * Fucntion checks if the current activeRoads tile is a checkpoint then returns truw or false
     */
    public bool IsCheckpoint()
    {
        if (activeRoads[0].name.Contains("Tile_Checkpoint")) {
            return true;
        } else
        {
            return false;
        }
    }

    /**
     * Fucntion spawns a Power-Up tile to the activeRoads list
     */
    private void SpawnPowerUp()
    {
        GameObject pwrUp = Instantiate(powerUp, transform.forward * zSpawnLoc, transform.rotation);
        activeRoads.Add(pwrUp);
        zSpawnLoc += roadLength;
    }

}
