using UnityEngine;

public class PetBee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public float followSpeed = 4f;
    public Transform playerTransform;
    public float stoppingDistance = 2f;
                
    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform)
            return;
            
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stoppingDistance)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
                transform.LookAt(playerTransform);

    }
}
