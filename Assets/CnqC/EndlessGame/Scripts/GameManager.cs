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

        StartCoroutine(SpawnBlockCo());
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
        m_blockIdx = 1; // làm cho nó không trùng với block đầu tiên trong mảng các block

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

    // tạo ra 1 courotine cho việc spawn ra block

    IEnumerator SpawnBlockCo()
    {
        if (IsConponentnull() || m_curLevel == null || m_curBlock == null) yield return null;

        var blockPrebfab = m_curLevel.blockPb;
        

        if (blockPrebfab)
            yield return null;

            while (state != GameState.Gameover) // kiểm tra trạng thái có phải G.OVer k --> nếu phải thì thôi
        {
            m_blockSpawnPosY += m_curBlock.blockGrap; // nếu mà block sau spawn sẽ tăng thêm 1 theo trục Y bằng 1 khoảng blockGrap

            // cứ mỗi vòng lập while thì block mới được tạo sẽ tăng lên 1 tốc độ += speedup
            m_blockSpeed += speedUp;

            // giới hạn blockspeed
            m_blockSpeed = Mathf.Clamp(m_blockSpeed, m_curLevel.baseSpeed, m_curLevel.maxSpeed);


            float checking = Random.Range(0f, 1f);

            SpriteRenderer PrevBlockSp = m_curBlock.Sp; // lấy ra spriteRender trong block hiện tại

            // trước khi game bắt đầu thì sẽ có  thông báo cho người dùng biết được là sắp tạo ra block
            GameObject WarningSignClone = null;

            if(checking <= 0.5f)
            {
                // x = nữa chiều rộng của Camara - 0,3f  
                Vector3 spawnPos = new Vector3(m_camSize.x/2  + 0.3f, m_blockSpawnPosY, 0f);

                WarningSignClone = Instantiate(warningSignPb, spawnPos, Quaternion.identity);

                // vì hình mặc định khi mà tạo ra warningsign thì nó hình hướng sang phải , ta sẽ chỉnh phần scale x của nó thành âm thì sẽ quay lại sang trái 
                WarningSignClone.transform.localScale = new Vector3(
                    WarningSignClone.transform.localScale.x * (-1)
                    , WarningSignClone.transform.localScale.y
                    , WarningSignClone.transform.localScale.z);
            }
            else
            {
                Vector3 spawnPos = new Vector3(-(m_camSize.x /2 + 0.3f), m_blockSpawnPosY, 0f);

                WarningSignClone = Instantiate(warningSignPb, spawnPos, Quaternion.identity);
            }


                yield return new WaitForSeconds(m_curLevel.spawnTime); // tạo ra spawnTime để sinh ra các block 
            
                if(checking <= 0.5f)
                {
                    // tạo ra vị trị spawn của các block
                    // vị trí của X của block sẽ = 1 nữa phải trục X của Camara +0.6f ( để block nằm ngoài và di chuyển từ từ vào)
                    Vector3 spawnPos = new Vector3((m_camSize.x / 2 - 0.8f), m_blockSpawnPosY, 0f);

                    // tạo ra block
                    m_curBlock = Instantiate(blockPrebfab, spawnPos, Quaternion.identity);

                // cho block di chuyển sang bên trái vì block được tạo bên phải

                m_curBlock.moveDirection = MoveDirection.Right;
                }
                else
                {
                    // vị trí của X của block sẽ = 1 nữa trái  trục X của Camara +0.6f ( để block nằm ngoài và di chuyển từ từ vào)
                    Vector3 spawnPos = new Vector3(-(m_camSize.x / 2 - 0.8f), m_blockSpawnPosY, 0f);

                    // tạo ra block
                    m_curBlock = Instantiate(blockPrebfab, spawnPos, Quaternion.identity);


                    // cho block di chuyển sang bên phải vì block được tạo bên trái

                    m_curBlock.moveDirection = MoveDirection.Left;
            }

            // thay đổi lần lượt các sprite ở trong block
            m_curBlock.ChangeSprite(ref m_blockIdx);

            m_curBlock.SpriteOrderUp(PrevBlockSp); // tăng lên sprites của block sau lên 1 đơn orderinlayer

            m_curBlock.canMove = true; // cho di chuyển


            m_curBlock.moveSpeed = m_blockSpeed; // gán giá trị của blockspeed cho vào tốc độ của block.

            if (WarningSignClone) // nếu mà warningsign được tạo ra khác rỗng thì xóa nó
                Destroy(WarningSignClone);
        }
    }

    public bool IsConponentnull()
    {
        bool checking = LevelManager.Ins == null ;

        if (checking) // if checking = true;
            Debug.Log("Some component is null !! Please check.");

        return checking;
    }

    // xử lý gameLogic
    public void PlayGame()
    {
        if (IsConponentnull() ) return;

        // chuyển state thành game
        state = GameState.Playing;

        StartCoroutine(SpawnBlockCo());
    }


    public void AddScore(int Score)
    {
        if (IsConponentnull() || state != GameState.Playing) return; //add score lúc đang choi

        m_score += Score;
        Pref.bestScore = m_score;
    }

    public void GameOver()
    {
        if (IsConponentnull()) return;
        state = GameState.Gameover;
        Debug.Log("GameOver!!!");
    }
}
