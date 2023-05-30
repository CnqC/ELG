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
    private int m_blockId; // lưu lại block Id
    private bool m_isDead;

    public bool IsConponentnull()
    {
        return m_rb == null || m_anim == null;
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
        
    }

    private bool IsOnBlock() // kiểm tra xem nhân vật có dang đứng tại trên block không
    {
        m_CenterPos = new Vector3(transform.position.x
            , transform.position.y - blockCheckingOffset, transform.position.z);

        // tranform.position.x hay y hay z là vị trí của nhân vật đang đứng

        // OverLapCircle là sẽ nhận lại vị trí trung tâm của đường tròn ảo
        Collider2D col = Physics2D.OverlapCircle(m_CenterPos, blockCheckingRadius,blockLayer);

        return col! == null; // nó sẽ 1 đường tròn mà đối tượng game đó bị vẽ chồng lấp lên 1 đường tròn khác thì sẽ return.
        // trả về true khi col có giá trị.
        // trả về false khi col kh có giá trị

    }


    private void OnDrawGizmos() // vẽ ra 1 hình tròn
    {

        m_CenterPos = new Vector3(transform.position.x
           , transform.position.y - blockCheckingOffset, transform.position.z);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(m_CenterPos,blockCheckingRadius);
    }

}
