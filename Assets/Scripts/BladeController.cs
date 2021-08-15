using RayFire;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private bool detectSwipe = false;
    private Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Plane plane = new Plane(new Vector3(0, 0, -4), 0);
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (plane.Raycast(ray, out distance))
            {
                fingerDown = ray.GetPoint(distance);
                fingerUp = ray.GetPoint(distance);
                this.transform.position = fingerDown;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (plane.Raycast(ray, out distance))
                fingerUp = ray.GetPoint(distance);
            detectSwipe = true;
            float AngleRad = Mathf.Atan2(fingerUp.y - this.transform.position.y, fingerUp.x - this.transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - this.transform.rotation.x - 1);            
        }

        if (detectSwipe)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, fingerUp, Time.deltaTime*10);
        }
        if (this.transform.position.x == fingerUp.x)
        {
            detectSwipe = false;
        }
    }

}
