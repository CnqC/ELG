using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class UnFollowCam : MonoBehaviour
{

    private Vector3 m_startingPos;


    // Start is called before the first frame update
    private void Awake()
    {
        m_startingPos = transform.position; // lấy ra vị trí đầu tiên khi đối tương tạo ra trên scene
    }

    // Update is called once per frame
    void Update()
    {
        // cứ mỡi frame ta sẽ thiết lập lại vị trí hiện tại của đối tượng game này = vị trí ban đầu
        // làm cho đối tượng game k di chuyển theo cam
        transform.position = m_startingPos;
    }
}
