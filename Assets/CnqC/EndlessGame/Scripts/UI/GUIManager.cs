using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;
using TMPro;
using System;

public class GUIManager : MonoBehaviour
{
    // tạo ra singleTon
    public static GUIManager Ins;
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Dialog gameOverDiaLog;
    public TextMeshProUGUI scoreCountingText;
    public Image gameOverImgTxt;


    private void Awake()
    {
        Ins = this;
        
    }

    public void ShowGameGUI(bool isShow)
    {
        // chỉ có 1 cái được hiện là homeGUI hay GameGUI nên ta tạo 1 phương thức để xử lý việc này.
        // nếu mà gameGUi != null --> sẽ được hiện thị theo biến isShow 
        if (gameGUI)
        
        gameGUI.SetActive(isShow);
        
        

        // nếu mà homeGUi != null --> sẽ được hiện thị ngược lại theo biến isShow 
        if (homeGUI)
            homeGUI.SetActive(!isShow);
    }


    // cập nhập các UI
    public void UpdateScore( int score)
    {
      
        if (scoreCountingText) 
        scoreCountingText.text = score.ToString();
    }


    private IEnumerator ShowGameOverTextCo()
    {
        // ktr nếu mà biến GameOverImgTxt mà != null, thì chúng ta sẽ hiện cái GameOverText lên
        if (gameOverImgTxt)
            gameOverImgTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); // thời gian sau 2s thì sẽ thực hiện phương thức

        if (gameOverImgTxt)
            gameOverImgTxt.gameObject.SetActive(false); // tắt đi cái GameOverText;

        if (gameOverDiaLog)
            gameOverDiaLog.Show(true); // xuất cái GameOverDiaLog lên màn hình
        scoreCountingText.enabled = false;

    }

    public void ShowGameOverImgTxt()
    {
        StartCoroutine(ShowGameOverTextCo());
    }

   
}
