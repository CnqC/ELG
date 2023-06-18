using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;
using TMPro;

public class MainMenuMusic : MonoBehaviour 
{
    private void Start()
    {
        if (AudioController.Ins == null) return;

        AudioController.Ins.PlayMusic(AudioController.Ins.menus); // play mảng menu music ở trong script AudioController được kêu thông qua phương thức playMusic

        // chỉnh cho khi playGame thì sẽ phát nhạc bên GameManager.
    }
}
