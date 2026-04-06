using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z), 0.005f);
        transform.position = newPos;
    }
}
