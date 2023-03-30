using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;
    public float boundX = .15f;
    public float boundY = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero; // frame differeces.

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
            delta.x = (transform.position.x < lookAt.position.x) ? deltaX - boundX : deltaX + boundX;


        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
            delta.y = (transform.position.y < lookAt.position.y) ? deltaY - boundY : deltaX + boundY;

        transform.position += new Vector3(deltaX, deltaY, 0);
    }

    
}
