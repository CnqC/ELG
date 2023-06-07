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
        
    }

    private void Init() 
    {
        if (levels == null || levels.Length <= 0) return; // ktra mảng levels

        for (int i = 0; i < levels.Length; i++)
        {
            var level = levels[i]; // duyệt từng phần tử trong mảng levels trên và gàn tuần tự vào biến var level.

            if (level == null) continue; // nếu mà level == null thì sẽ ngắt vòng lập và chuyển sang vòng lập khác

            if(i == 0 ) // mở khóa level đầu
            {

            }
            else
            {
                // nếu dữ liệu chưa được lưu xuống máy người chơi thì sẽ lưu dữ liệu.
                // tức là khóa các level khác lại.


            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
