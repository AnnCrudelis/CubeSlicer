using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    float x, y;
    Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        this.transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.GetComponent<RayfireGun>().Shoot();
        }
    }
}
