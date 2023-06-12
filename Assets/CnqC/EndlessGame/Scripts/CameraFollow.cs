using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Ins;

    public Transform target; // mục tiêu của Camara di chuyển theo
    public Vector3 offset; // điều khiển khoảng cach của Camara cách Player bao nhiêu
    [Range(1, 10)]
    public float smoothFactor; // làm cho thằng Camara mượt mà

    private void Awake()
    {
        Ins = this;
    }

    private void FixedUpdate() // thiên về vật lý
    {
        Follow();
    }

    void Follow()
    {
        if (target == null) return;

        // lấy vị trí mục tiêu ( chỉ lấy tọa độ của y khi nhân vật nhảy lên) + offset ( khoảng cách của cam vs player)                       
        Vector3 targetPos = new Vector3(0, target.transform.position.y, 0f) + offset;

                                      //Lerp ( vector3 A, vector3 B, time) --> ý là sau 1 thời gian nó sẽ nhít dần từ A -> b, chứ k nhảy thẳng tới 
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
        transform.position = new Vector3( // vì cứ mỗi lần nhít là có vị trí mới nên ta đặt lại vị trí Camara lại là vị trí mới
            Mathf.Clamp(smoothedPos.x, 0, smoothedPos.x),
            Mathf.Clamp(smoothedPos.y, 0, smoothedPos.y),
            0f);
    }
}
