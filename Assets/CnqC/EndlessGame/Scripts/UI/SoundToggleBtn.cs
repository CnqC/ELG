using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.UI;


public class SoundToggleBtn : ToggleBtn
{

    // Start is called before the first frame update
    protected override void Start() // ghi đè lại phương thức ảo của lớp cha 
    {
        base.Start();
        // khi mà dữ liệu chưa lưu xuống máy ng dùng thì sẽ ra true -> audio phát ra âm thanh
        m_IsOn = Pref.GetBool(GamePref.IsSoundOn.ToString(), true);

        UpdateSprite(); // khi game chạy thì sẽ UpDate hình
    }

    public override void ClickEvent()
    {
        m_IsOn = !m_IsOn; // mỗi khi mà ng dùng click vào sẽ ra ngược lại với nó
                              // vd toggle đang true, click -> false


        Pref.SetBool(GamePref.IsSoundOn.ToString(), m_IsOn);// xuống máy ng dùng với giá trị bool của toggle.

        if (AudioController.Ins == null) return;

                                //bool
        AudioController.Ins.sfxAus.mute = !m_IsOn; 
        // nếu mà Ison = true -> phát, còn IsOn = false thì tắt đi
        // nhưng mà trong AudioController thì ngược lại,
        // khi mà ô vuông của mute = true ( được tích lên) thì nó sẽ tắt, và false ( ẩn tích) thì nó sẽ tắt
        
    
    
    }
}
