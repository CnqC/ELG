using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class LevelManager : MonoBehaviour, ISingleTon
{


    // tạo singleTon
    public static LevelManager Ins;


    public LevelItem[] levels; // mảng trong dataStruct.



    private void Awake()
    {
        MakeSingleTon();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init() 
    {
        if (levels == null || levels.Length <= 0) return; // ktra mảng levels

        for (int i = 0; i < levels.Length; i++)
        {
            var level = levels[i]; // duyệt từng phần tử trong mảng levels trên và gàn tuần tự vào biến var level.

            if (level == null) continue; // nếu mà level == null thì sẽ ngắt vòng lập và chuyển sang vòng lập khác

            string levelDataKey = GamePref.LevelUnlocked.ToString() + i;

            if(i == 0 ) // mở khóa level đầu
            {

                Pref.SetLevelUnlock(i, true); // truyền vào cho levelId là biến đếm là i, trạng thái mở khóa là true
            }
            else
            {
                // nếu dữ liệu chưa được lưu xuống máy người chơi thì sẽ lưu dữ liệu.
                // tức là khóa các level khác lại.

                if(!PlayerPrefs.HasKey(levelDataKey)) // nếu mà dữ liệu chưa được lưu xuống máy người dùng
                {
                    Pref.SetLevelUnlock(i, false);
                }

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public LevelItem GetLevel()
    {
        if(levels != null && levels.Length > 0)
        {
            // trả về mảng levels và lấy ra chỉ số bằng các chỉ số của level mà mình đã lưu dưới máy người dùng
            return levels[Pref.CurLevelId];
        }

        return null;  // null khi = 0 hay 0 có phần tử 
    }


    public void MakeSingleTon()
    {
        if(Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // nếu mà ta thấy đối tượng nào tham chiếu tới cái script LevelManager này thì --> destroy
            // vì singleTon chỉ tồn tại 1 bản thể
            Destroy(gameObject);
        }
    }
}
