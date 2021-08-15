using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] cubes;
    private GameObject rayfireMan;
    private GameObject currentCube;
    [SerializeField]
    private GameObject blade;
    [SerializeField]
    private GameObject gun;
    private bool isFreeModeOn = false;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    float speed = 0;
    public Toggle[] toggles;
    public CameraRotator cameraRotator;

    private void Start()
    {
        rayfireMan = FindObjectOfType<RayfireMan>().gameObject;
        ResetCube();
    }

    public void ResetCube()
    {
        blade.SetActive(false);
        gun.SetActive(false);
        DestroyAllRigids();
        
        for (var i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                currentCube = Instantiate(cubes[i], new Vector3(1.5f, 1.5f, 0), Quaternion.identity);
                
            }
                
        }
        SetActiveWeapon();
    }

    void SetActiveWeapon()
    {
        foreach (var toggle in toggles)
        {
            if (toggle.isOn && !isFreeModeOn)
            {
                if (toggle.name == "Slice")
                {
                    blade.SetActive(true);
                    gun.SetActive(false);
                }
                else
                {
                    gun.SetActive(true);
                    blade.SetActive(false);
                }
            }
        }

    }

    public void DestroyAllRigids()
    {
        RayfireRigid[] rigids = rayfireMan.GetComponentsInChildren<RayfireRigid>();
        if (currentCube != null)
        {
            Destroy(currentCube);
        }
        foreach (var rigid in rigids)
        {
            Destroy(rigid.gameObject);
        }
    }

    public void SetFreeModeOnOff()
    {
        if (isFreeModeOn)
        {
            isFreeModeOn = false;
            SetActiveWeapon();
        }
        else
        {
            isFreeModeOn = true;
            blade.SetActive(false);
            gun.SetActive(false);
        }
    }

    private void FreeMode()
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
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (plane.Raycast(ray, out distance))
                fingerUp = ray.GetPoint(distance);
            speed = fingerUp.x;
        }
        if (speed > 0)
            speed -= Time.deltaTime;
        cameraRotator.RotateCamera(speed);
    }

    private void Update()
    {
        if (isFreeModeOn)
        {
            FreeMode();
        }
    }
}
