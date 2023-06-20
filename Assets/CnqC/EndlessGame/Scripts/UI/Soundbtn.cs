using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;


public class Soundbtn : MonoBehaviour
{
    private Button m_btn;



    private void Awake()
    {
        m_btn = GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (m_btn == null) return;

        m_btn.onClick.RemoveAllListeners();
        // tạo sự kiện cho btn này khi được click sẽ tạo ra hiêu ứng playsound
        m_btn.onClick.AddListener(() => PlaySound());
    }

    private void PlaySound()
    {
        if (AudioController.Ins == null) return;

        AudioController.Ins.PlaySound(AudioController.Ins.btnClick);
    }
}
