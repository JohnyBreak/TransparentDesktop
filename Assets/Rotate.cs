using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 45 * Time.deltaTime, Space.Self);
    }
}
