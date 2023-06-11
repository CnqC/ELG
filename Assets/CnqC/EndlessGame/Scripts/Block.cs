using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

public class Block : MonoBehaviour,IComponentChecking
{


    // tạo ra các biến

    public float moveSpeed;
    public MoveDirection moveDirection; // hướng di chuyển
    public bool canMove; // ktra xem có cho di chuyển hay k
    public float blockGrap; // khoảng cách giữa 2 block được tạo ở trên scene

    public Sprite[] sprites; // mảng các ảnh của block
    public int minScore;
    public int maxScore;


    // biến lưu trữ các thành phần
    private Rigidbody2D m_rb;
    private SpriteRenderer m_Sp;

    private int m_id; // id của block
    private int m_curScore; // điểm số hiện tại 

    public SpriteRenderer Sp { get => m_Sp; }
    public int Id { get => m_id;  }
    public int CurScore { get => m_curScore;  }

    public bool IsConponentnull()
    {
        bool checking = m_rb == null || m_Sp == null;
        if (checking) // if checking = true;
            Debug.Log("Some component is null !! Please check.");

        return checking;
    
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_Sp = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_id = GetInstanceID(); // cứ mỗi gameObject nào ở trên scene đều sẽ có Instance ID
                                // lệnh này dùng để lấy ra Id đó và lưu vào biến m_id

        m_curScore = Random.Range(minScore, maxScore);

    }

    // Update is called once per frame
    void Update()
    {
        // ta sẽ gọi cái BlockMoving vào đây cứ mỗi frame trôi qua là sẽ set phương thức này

        BlockMoving();
    }


    private void BlockMoving()
    {
        if (IsConponentnull() || !canMove) return; // nếu mà check component null hay biến canmove = false thì return;

        if(moveDirection == MoveDirection.Left) 
            // nếu mà check ra là sang trái thì sẽ dịch sang trái tọa độ ( -1,0) với từng frame với tốc độ = movespeed
        {
            m_rb.velocity = Vector2.left * moveSpeed; 
                                   // .left ( -1,0)
        }
        else if (moveDirection == MoveDirection.Right)
        // nếu mà check ra là sang phải thì sẽ dịch sang trái tọa độ ( -1,0) với từng frame với tốc độ = movespeed
        {
            m_rb.velocity = Vector2.right * moveSpeed;
                                   // right(1,0)
        }

        Vector3 centerPos = new Vector3(0, transform.position.y, transform.position.z);
        // khi mà block ở chính giữa màn hình thì sẽ có tọa độ x =0 , y và z là vị trí ban đầu của 2 trục đó
        // căn theo trục x


        // biến lưu lại giá trị khoảng cách tới điểm trung tâm

        float distToCenterPos = Vector2.Distance(transform.position, centerPos);
                                     // sẽ truyền vào vị trí ( vị trí hiện tại, trung tâm)


        // ktr nếu mà vị trí block này tới vị trí trung tâm == bao nhiêu thì dừng lại
        if(distToCenterPos <= 0.1f)
        {
            m_rb.velocity = Vector2.zero; // nhưng vận tốc

            // đặt lại vị trí block = vị trí trung tâm
            transform.position = centerPos;
        }
    }

    public void PlayerLand()
    {

        if (IsConponentnull() || canMove == false) return;
        // khi mà player đáp vào block thì:

        canMove = false;

        m_rb.velocity = Vector2.zero;
    }

    public void SpriteOrderUp(SpriteRenderer preBlockSp) // tăng cái order in layer lên
    {
        if (IsConponentnull()) return;

        m_Sp.sortingOrder = preBlockSp.sortingOrder + 1 ; // ở đây chỉ có sortingOrder chứ k order in layer
            // lấy cái sortingOrder của block trước cộng thêm 1 và gán cho block sau.
    }


    public void ChangeSprite( ref int idx)
    {
        if (sprites == null || sprites.Length <= 0 || IsConponentnull()) return; //ktra mảng ảnh block

        // thay đổi sprite
        m_Sp.sprite = sprites[idx]; // thay đổi hình ảnh ( sprite) của m_sp thông qua hình ảnh trong mảng sprites ở trên

        idx++; // tăng biến idx lên 

        // ktra nếu mà tham số idx >= độ dài của mảng sprites --> thì ta sẽ trả giá trị của là 0
        // chạy hết sprite sẽ quay lại sprite đầu tiên
        if (idx >= sprites.Length)
            idx = 0;

    }
}
