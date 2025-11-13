using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 10, -15);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // You can manually set the offset in Inspector now
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
            return;
        transform.position = player.transform.position + offset;
    }
}
