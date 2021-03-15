using UnityEngine;

public class RandomWalls : MonoBehaviour
{
    public Sprite[] Walls;
    
    void Start()
    {
        int rand = Random.Range(0, Walls.Length);
        GetComponent<SpriteRenderer>().sprite = Walls[rand];
    }
}
