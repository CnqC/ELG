using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class DestroyDelay : MonoBehaviour
{
    public float timeToDestroy;
    public bool isAutoDestroy; // có tự động hủy khi tạo ra hay không

    private void Awake()
    {
        if (isAutoDestroy)
            DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
