using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverDiaLog : Dialog
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI bestScoreTxt;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (scoreTxt)
            scoreTxt.text = GameManager.Ins.Score.ToString(); // cập nhập score

        if (Pref.hasBestScore ) // nếu end game rồi mà thấy có bestcore mới -> biến hasbestcore = true
        {
            if (bestScoreTxt)
                bestScoreTxt.text = $"NEW BEST: {Pref.bestScore}";

            AudioController.Ins.PlaySound(AudioController.Ins.bestScore); // phát âm thanh khi đạt bestScore
        }
        else
        {
            // ng chơi k có điểm số cao hơn bestcore cũ
            // cập nhập lại bestcore cũ:

            if (bestScoreTxt)
                bestScoreTxt.text = $"TOP SCORE: { Pref.bestScore}";
        }
            
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
