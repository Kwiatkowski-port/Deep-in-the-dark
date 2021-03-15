using UnityEngine;

public class Grounded : MonoBehaviour
{
    GameObject tmPlayer;

    void Start()
    {
        tmPlayer = gameObject.transform.parent.gameObject;
    }
}
