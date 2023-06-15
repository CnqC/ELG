using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;
using UnityEngine.SceneManagement;

public static class Pref 
{
    public static bool hasBestScore;

    public static int bestScore
    {
        set
        {
            int oldScore = PlayerPrefs.GetInt(GamePref.BestScore.ToString(), 0);

            if( oldScore < value) // nếu mà điểm số đã lưu nhỏ ơn điểm số hiện tại 
            {
                hasBestScore = true; // đã đạt điểm số cao nhất

                PlayerPrefs.SetInt(GamePref.BestScore.ToString(),value); // biết đề lên điểm số cũ


            }
            else // nếu mà điểm số mới thấp hơn điểm số cũ thì --> không làm gì cả
            {
                hasBestScore = false; // chưa đạt điểm số cao nhất
         
            }
        }

        get => PlayerPrefs.GetInt(GamePref.BestScore.ToString());
    }

    public static int CurLevelId // lưu lại Id của các level
    {
        set => PlayerPrefs.SetInt(GamePref.CurLevelId.ToString(), value);
        get => PlayerPrefs.GetInt(GamePref.CurLevelId.ToString(), 0);
    }

    public static void SetLevelUnlock( int levelId, bool unlocked) // xem là trạng thái của level đã mở chưa
    {    
        // vd: levelId =1 thì key --> levelUnlock1
        // dùng để phân chia và lưu lại các key khác nhau ứng với lại các Id khác nhau
        // ktra cái nào mở khóa cái nào chưa
        SetBool(GamePref.LevelUnlocked.ToString() + levelId, unlocked);
    }

    public static bool IslevelUnlocked (int levelId) // ktr dữ liệu lưu xuống máy người dùng có hay chưa
    {
        return GetBool(GamePref.LevelUnlocked.ToString() + levelId);
    }
     



   // tạo ra 2 phương thức Set/getBool

    public static void SetBool(string key, bool value)
    {
      
            PlayerPrefs.SetInt(key, value ? 1 :0);
                                    // tóan tử 3 ngôi
                                    // value true--> 1, false -> 0

    }
   
    public static bool GetBool(string key, bool defaultvalue = false)
    {                                           // giá tri mặc định

        // ktra xem dữ liệu đã lưu xuống máy người dùng có sẵn key chưa
        return PlayerPrefs.HasKey(key) ? 
            PlayerPrefs.GetInt(key) == 1 ? true : false : defaultvalue; 
           // nếu mà dữ liệu ng dùng lưu xuống = 1 --> true
           // dữ liệu mà != 1 --> false 
           // dữ liệu mà chưa lưu thì xuất defaultvalue

    }

    
}
