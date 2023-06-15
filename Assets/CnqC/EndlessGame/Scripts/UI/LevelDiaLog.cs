using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelDiaLog : Dialog,IComponentChecking
{
    public Transform gridGroup;  // tạo ra các mảng từ thanh ở dưới
    public LevelItemUI itemUIPb;
    public TextMeshProUGUI bestScoreTxt;

   

    public bool IsConponentnull()
    {
        bool checking = LevelManager.Ins == null || gridGroup == null ||itemUIPb == null ;

        if (checking) // if checking = true;
            Debug.Log("Some component is null !! Please check.");

        return checking;
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (bestScoreTxt)
            bestScoreTxt.text = $"TOP SCORE: {Pref.bestScore}";

       

        UpdateUI(); // cril + . để tạo ra phương thức này

    }

    private void UpdateUI()
    {
        var levels = LevelManager.Ins.levels;

        if (levels == null || levels.Length <= 0 || IsConponentnull()) return;


        // xóa bỏ các phần tử cũ của gird

        Helper.ClearChilds(gridGroup);


        for (int i = 0; i < levels.Length; i++)
        {
            // tạo ra 1 biến gán giá trị của i tăng dần cho nó
            // tránh bị lỗi khi truyền trực tiếp thì sẽ chỉ nhận mỗi 1 gia trị gần nhất
            int levelId = i;

            var level = levels[i]; // lấy ra từng giá trị theo thứ tự từ 0 trong mảng levels

            if (Pref.bestScore >= level.scoreRequire) // nếu mà điểm số được lưu dưới máy ng dùng >= điểm mà level yêu cầu thì mở khóa
                Pref.SetLevelUnlock(levelId, true);

            if (level == null) continue; // nếu mà level == nul thì bỏ qua vòng lặp này và chạy vòng lặp khác

                                                     // ở giữa (0,0,0)
            var LevelUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);

            // set các transform 
            LevelUIClone.transform.SetParent(gridGroup);

            // đưa vị trí của Ui vào vị trí 000
            LevelUIClone.transform.localPosition = Vector3.zero;

            // thay đổi scale 
            LevelUIClone.transform.localScale = Vector3.one;

            //cập nhập các levelItem --> truy cập vào script LevelItemUI
            LevelUIClone.UpdateUI(level, levelId);

            // nếu mà LevelUIclone có button thì sẽ gọi tới sự kiện Onclick của nó
            if (LevelUIClone.btn)
            {
                LevelUIClone.btn.onClick.RemoveAllListeners(); // xóa đi các Listiners cũ
                LevelUIClone.btn.onClick.AddListener(() => LevelClicKEvent(level, levelId));
                
            }
        }
    }


    private void LevelClicKEvent(LevelItem level, int levelId)
    {
        if (level == null) return;

        bool isUnlocked = Pref.IslevelUnlocked(levelId);

        if (isUnlocked)
        {
            // nếu mà biến isUnlocked = true --> sẽ lưu lại ID của Game băng cách thay thế Id của hiện tại của Game = 1 giá tị = levelID
            Pref.CurLevelId = levelId;

            // vì ta đã qua scene làm UI 
            SceneManager.LoadScene(GameScene.GamePlay.ToString());
        }
    }
}

