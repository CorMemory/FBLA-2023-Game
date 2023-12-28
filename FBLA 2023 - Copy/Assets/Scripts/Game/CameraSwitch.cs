using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject PlayerCam;
    public GameObject TableCam;
    public int Manager;


    public void ManageCamera()
    {
        switch (Manager)
        {
            case 0:
                Cam_2();
                Manager = 1;
                break;
            case 1:
                Cam_1();
                Manager = 0;
                break;
        }
    }

    public void Cam_2()
    {
        PlayerCam.SetActive(false);
        TableCam.SetActive(true);
    }

    public void Cam_1()
    {
        PlayerCam.SetActive(true);
        TableCam.SetActive(false);
    }
}
