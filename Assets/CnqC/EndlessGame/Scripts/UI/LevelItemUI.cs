using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;
using TMPro;

public class LevelItemUI : MonoBehaviour
{
    public TextMeshProUGUI scoreRequireText; // điểm số yêu cầu
    public Image lockThumb; // tham chiếu tới tới Locked trong ItemUI của LevelDiaLog
    public Image UnlockThumb;
    public Button btn;

    public void UpdateUI(LevelItem level, int levelId)
    {
        if (level == null) return;

        /// lấy ra trạng thái levelID dưới máy ng dùng

        bool isUnlocked = Pref.IslevelUnlocked(levelId);


        if (lockThumb)
            lockThumb.gameObject.SetActive(!isUnlocked); // khi mà level dc mở khóa --> image lock sẽ ẩn

        if (UnlockThumb)
            UnlockThumb.gameObject.SetActive(isUnlocked); // ngược lại là nếu như được mở khóa thì unlock image sẽ hiện

        if (isUnlocked) // dc mở khóa
        {
            if (UnlockThumb)
                UnlockThumb.sprite = level.unlockThumb; // đổi sprites trong unlockThum = level của unlockthum
        }
        else // k dc mở khóa
        {
            if (scoreRequireText) // nếu mà != null thì sẽ xét lại điểm số bắt buộc mà ng chơi đạt được để mở khóa
                scoreRequireText.text = level.scoreRequire.ToString();

            // đổi sprites cho nhân vật
            if (lockThumb)
                lockThumb.sprite = level.lockThumb;
    
        }
    }
}
