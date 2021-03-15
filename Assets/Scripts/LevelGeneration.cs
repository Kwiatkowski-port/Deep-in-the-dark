using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Tablica zawierająca możliwe pozycję rozpoczęcia generacji poziomu
    public Transform[] startingPoints;
    public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT, index 4 --> LR down, index 5 --> LR top, index 6 --> LRB down, index 7 --> LRB top, index 8 --> LRT down, index 9 --> LRT top, index 10 --> LRTB down, index 11 --> LRTB TOP

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom;

    public float minX;
    public float maxX;
    public float minY;
    
    public bool stopGeneration;

    public LayerMask room;

    private int downCounter;

    public GameObject LoadingScreen;
    public GameObject Points;

    public AudioClip backgroundSound;

    AudioSource source;

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPoints.Length);
        transform.position = startingPoints[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        source = GetComponent<AudioSource>();
        source.clip = backgroundSound;
        source.Play();
        direction = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopGeneration == false)
        {
            Move();
        }

        /*if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }*/
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) //Ruch w prawo
        {

           if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Ruch w lewo
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) //Ruch w dół 
        {
            downCounter++;
            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().DeleteRoom();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().DeleteRoom();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;  //stop
            }
            if (stopGeneration == true)
            {
                Invoke("Loading", 2);
            }
        }
    }

    public void Loading()
    {
        LoadingScreen.SetActive(false);
        Points.SetActive(true);
    }
}
