using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;


public class ToggleBtn : MonoBehaviour
{
    public Sprite on, off;

    private Button m_btn; // tham chieu tối componentButton của Script
    protected bool m_IsOn; // cho các script khác có thể sữ dụng


    private void Awake()
    {
        m_btn = GetComponent<Button>();

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (m_btn == null) return;

        m_btn.onClick.AddListener(() => BtnClickEvent());
    }

    private void BtnClickEvent()
    { 
        ClickEvent();
        UpdateSprite();
    }

    protected void UpdateSprite()
    {
        Image img = m_btn.GetComponent<Image>(); // lấy thành phần Img của thằng Button
        if (img)
        {
            // thay đổi sprite của Img
            img.sprite = m_IsOn ? on : off; // nếu toggle = true -> thay img on, còn false -> off

        }
    }

    public virtual void ClickEvent()
    {
       
    }
}
