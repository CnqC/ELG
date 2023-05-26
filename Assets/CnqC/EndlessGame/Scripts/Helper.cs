using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public static class Helper
{
    public static void ClearChilds(Transform root)
    {
        if (root)
        {
            int childs = root.childCount;

            if (childs > 0)
            {
                for (int i = 0; i < childs; i++)
                {
                    var child = root.GetChild(i);

                    if (child)
                        MonoBehaviour.Destroy(child.gameObject);
                }
            }
        }
    }

    public static Vector2 Get2DCamSize()
    {
        return new Vector2(2f * Camera.main.aspect * Camera.main.orthographicSize, 2f * Camera.main.orthographicSize);
    }

    public static void FitSpriteToScreen(SpriteRenderer sp, bool resetScale = true, bool fitX = true, bool fixY = true, float offsetX = 0, float offsetY = 0)
    {
        if (resetScale)
            sp.transform.localScale = Vector3.one;

        var width = sp.sprite.bounds.size.x;
        var height = sp.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        double scaleX = worldScreenWidth / width;
        double scaleY = worldScreenHeight / height;

        if (fitX)
            sp.transform.localScale = new Vector3((float)scaleX + offsetX, sp.transform.localScale.y + offsetY, sp.transform.localScale.z);

        if (fixY)
            sp.transform.localScale = new Vector3(sp.transform.localScale.x + offsetX, (float)scaleY + offsetY, sp.transform.localScale.z);
    }
}
