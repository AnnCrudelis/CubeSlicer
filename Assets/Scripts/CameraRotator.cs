using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{

    public void RotateCamera(float speed)
    {
        this.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
