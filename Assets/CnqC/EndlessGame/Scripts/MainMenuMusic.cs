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

        bool isMusicOn = Pref.GetBool(GamePref.IsMusicOn.ToString(), true);
        bool isSoundOn = Pref.GetBool(GamePref.IsSoundOn.ToString(), true);

        if (AudioController.Ins.musicAus)
            AudioController.Ins.musicAus.mute = !isMusicOn; // nếu mà thằng musicAus != null thì sẽ kích hoạt nhờ biến isMusicOn

        if (AudioController.Ins.sfxAus)
            AudioController.Ins.sfxAus.mute = !isSoundOn; // nếu mà thằng sfx != null thì sẽ kích hoạt nhờ biến isMusicOn

        AudioController.Ins.PlayMusic(AudioController.Ins.menus); // play mảng menu music ở trong script AudioController được kêu thông qua phương thức playMusic

        // chỉnh cho khi playGame thì sẽ phát nhạc bên GameManager.
    }
}
