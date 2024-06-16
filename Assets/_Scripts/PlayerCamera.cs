using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    float cameraSpeed = 7f;


    void Start()
    {
            
    }


    void Update()
    {
        FollowPlayer();
        LookAtPlayer();
        
    }


    public bool ChangePlayer(Transform newTarget)
    {
        if (newTarget == playerTransform) return false;
        
        playerTransform = newTarget;
        return true;
    }


    void LookAtPlayer()
    {
        // ToDo:
        // Add gurad if player position has not changed

        float speed = cameraSpeed * Time.deltaTime;
        Vector3 relativePos = playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);
        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.zero - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
    }

    void FollowPlayer()
    {
        // ToDo:
        // Add gurad if player position has not changed


        Vector3 target = playerTransform.position - playerTransform.right;
        float speed = cameraSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, speed);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay((transform.position + Vector3.up), transform.forward * 5);
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
