using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseDiaLog : Dialog
{
    
    
  
    public override void Show(bool isShow)
    {
        base.Show(isShow);

        Time.timeScale = 0f;

      
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
        Close();
        // tạo ra sự kiện cho button Replay trong GameOverDiaLog để quạy lại GameGUI
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
