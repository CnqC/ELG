using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseDiaLog : Dialog
{
    private Animator  m_anim;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);

        Time.timeScale = 0f;

        m_anim.SetTrigger(ChacAnim.Shake.ToString());
    }

    public override void Close()
    {
         Time.timeScale = 1f;
        base.Close();

       
    }

    public void BackHome()
    {
        Close(); //thêm sự kiện cho button Return đóng cái GameOverDiaLog này và chuyển sang MainMenu
        SceneManager.LoadScene(GameScene.MainMenu.ToString());
    }


    public void Replay()
    {
        // tạo ra sự kiện cho button Replay trong GameOverDiaLog để quạy lại GameGUI
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
