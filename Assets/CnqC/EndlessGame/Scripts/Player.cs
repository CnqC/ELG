using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;


public class Player : MonoBehaviour, IComponentChecking
{
    public float jumpForece; // điều khiển được nhảy
    public LayerMask blockLayer;
    public float blockCheckingRadius;
    public float blockCheckingOffset;

    public GameObject lanvfx; // hiểu ứng khi mà người chơi đáp xuống bề mặt block.

    private Vector3 m_CenterPos;
     
    private Rigidbody2D m_rb;
    private Animator m_anim;

    private bool m_isOnBlock; // biến ktra xem có đang trên block hay không
    private int m_blockId; // lưu lại block Id
    private bool m_isDead;

    public bool IsConponentnull()
    {
        bool checking = m_rb == null || m_anim == null;
        if (checking) // if checking = true;
            Debug.Log("Some component is null !! Please check.");

        return checking;
    }

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();

    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDead || IsConponentnull()) return;

        Jump();

       if(m_rb.velocity.y < 0) // có nghĩa là khi mà vận tốc của trục y của nhân vật <0 sau khi jump() thì --> nhân vật ta sẽ rơi xuống
        {
            if (m_isOnBlock ) // ta sẽ ktra là biến này có true hay k, ý là có trên mặt đất hay không, nếu có chuyển từ jump -> land.
            {
                m_anim.SetBool(ChacAnim.Jump.ToString(), false) ; // phải tắt jump trước khi nhận ra là nhân vật đang ở block, nếu k tắt thì nó sẽ hiện palyer ở animation nhảy hoài luôn
                m_anim.SetBool(ChacAnim.Land.ToString(), true);


            }
            else // nếu kiểm tra thấy k có trên block thì chuyển sang vị trí on air
            {
                m_anim.SetBool(ChacAnim.Jump.ToString(), false);
            }
        }
    }

    private void FixedUpdate() // ta sẽ để các code sử lý vật lý ở trong đây
    {
        IsOnBlock(); // ktra liên tục
    }
     
    private void IsOnBlock() // kiểm tra xem nhân vật có dang đứng tại trên block không
    {
        m_CenterPos = new Vector3(transform.position.x
            , transform.position.y - blockCheckingOffset, transform.position.z);

        // tranform.position.x hay y hay z là vị trí của nhân vật đang đứng

        // OverLapCircle là sẽ nhận lại vị trí trung tâm của đường tròn ảo
        Collider2D col = Physics2D.OverlapCircle(m_CenterPos, blockCheckingRadius,blockLayer);
                                    // kiểm tra chống lấp của vòng tròn ở vị trí m_CenterPos,
                                    // bán kính của vòng tròn là blockCheckingRadius,
                                    // LayerMask của block va chạm với player là blockLayer.
                                    // nếu va chạm là 1 block nó sẽ gán giá trị thành 1 đối tượng game cho 1 đối tượng collider
                                    

                                

        m_isOnBlock= col != null ? true :false; // nó sẽ 1 đường tròn mà đối tượng game đó bị vẽ chồng lấp lên 1 đường tròn khác thì sẽ return.
        

    }

    public void Jump ()
    {     // nếu mà biến CamJump này là false --> ng chơi chưa nhấn nút nhảy
        // hoặc nhân vật này k ở trên block mà đang ơ trên không hay đang nhảy thì sẽ k làm gì cả
        if (!GamePadController.ins.CanJump   || !m_isOnBlock  || IsConponentnull()) return;

        GamePadController.ins.CanJump = false;

        m_rb.velocity = Vector3.up *  jumpForece ;
        // vector2.up có tọa độ (x =0, y =1)
        // jumpForece là lực nhảy

        m_anim.SetBool(ChacAnim.Jump.ToString(),true); // vì enum là dạng số nguyên, nên ta sẽ chuyển dạng enum thành dạng chuỗi
        m_anim.SetBool(ChacAnim.Land.ToString(), false); // vì khi đang ở trên không thì nó sẽ không chạm đất

        
    }

    public void BacktoIdle()
    {
       
        m_anim.SetBool(ChacAnim.Land.ToString(), false);

        m_anim.SetTrigger(ChacAnim.Idle.ToString());
    }
        
    private void OnDrawGizmos() // vẽ ra 1 hình tròn
    {

        m_CenterPos = new Vector3(transform.position.x
           , transform.position.y - blockCheckingOffset, transform.position.z);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_CenterPos,blockCheckingRadius);
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GameTag.Block.ToString()))
        {
            Block block = col.gameObject.GetComponent<Block>();
            if (block) // nếu block != null
                block.PlayerLand();

            Debug.Log("da va cham vs blok");
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameTag.DeadZone.ToString()))
        {
            Debug.Log(" da va cham voi deadz");
        }
    }
}
