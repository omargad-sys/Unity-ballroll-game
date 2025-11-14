using UnityEngine;

public class sun_rotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      transform.Rotate(0,1.2f/15,0);   
    }
}
