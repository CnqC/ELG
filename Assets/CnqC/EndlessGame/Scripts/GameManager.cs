using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class GameManager : MonoBehaviour,IComponentChecking
{

    // Tạo singleTon
    public static GameManager Ins;

    public float speedUp;
    public GameState state;
    public GameObject warningSignPb; // tín hiệu cảnh báo

    private Player m_curPlayer;
    private Block m_curBlock;
    private LevelItem m_curLevel;

    private Vector2 m_camSize;
    private int m_blockIdx;
    private float m_blockSpawnPosY;
    private float m_blockSpeed;
    private int m_score;

    public Block CurBlock { get => m_curBlock; }
    public int Score { get => m_score;  }



    private void Awake()
    {
        Ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init(); 
    }

   public void Init()
    {
        if (IsConponentnull()) return;
        state = GameState.Starting;
        m_camSize = Helper.Get2DCamSize();

        m_blockSpawnPosY = -m_camSize.y / 2 + 1f;
        // vì các block sẽ hiện từ dưới lên
        // --> sẽ cho nó spawn từ âm của 1/2 chiều dài camara + 1f 


        // lấy ra level hiện taị
        m_curLevel = LevelManager.Ins.GetLevel();
        Pref.hasBestScore = false;

        if(m_curLevel != null)
        {
            m_blockSpeed = m_curLevel.baseSpeed; // ta sẽ lấy basespeed có trong cái levelmanager gắn cho biến m_blockSpeed 

            var mapPb = m_curLevel.mapPrefab;

            if (mapPb)
                Instantiate(mapPb, Vector3.zero, Quaternion.identity);

            var blockPb = m_curLevel.blockPb;

            // tạo ra block trên scene
            if (blockPb)
                m_curBlock = Instantiate(blockPb, new Vector3(0, m_blockSpawnPosY, 0f), Quaternion.identity);
        }

        ActivePlayer();
    }

    public void ActivePlayer()
    {
        if(IsConponentnull()) return;

        if (m_curPlayer)
            Destroy(m_curPlayer.gameObject);
        if(m_curLevel != null)
        {
            var newPlayerPb = m_curLevel.playerPb;

            //tạo ra player
            if (newPlayerPb)
                m_curPlayer = Instantiate(newPlayerPb, new Vector3(0, -1f, 0f), Quaternion.identity);


        }
    }

    public bool IsConponentnull()
    {
        bool checking = LevelManager.Ins == null ;

        if (checking) // if checking = true;
            Debug.Log("Some component is null !! Please check.");

        return checking;
    }
}
