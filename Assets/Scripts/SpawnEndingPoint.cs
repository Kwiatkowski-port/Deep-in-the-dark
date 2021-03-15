using UnityEngine;

public class SpawnEndingPoint : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] end;

    int randomSpawnpoint;
    int endingObject;

    void Start()
    {
        randomSpawnpoint = Random.Range(0, spawnPoints.Length);
        endingObject = Random.Range(0, end.Length);
        Instantiate(end[endingObject], spawnPoints[randomSpawnpoint].position, Quaternion.identity);
    }
}

