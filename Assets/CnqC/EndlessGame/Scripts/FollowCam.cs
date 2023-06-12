using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class FollowCam : MonoBehaviour // đối tượng flow theo cam
{

    private Camera m_cam;


    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main; // lấy ra camara chính và add vào cam

    }

    // Update is called once per frame
    void Update()
    {
        // cập nhập vị trí của đối tượng game add script này
        transform.position = new Vector3(

            m_cam.transform.position.x, // tọa độ x của Cam
            m_cam.transform.position.y, // Tọa độ y của Cam
            transform.position.z);
    }
}
