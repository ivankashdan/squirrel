using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    
    void Update()
    {



        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);





    }


}
