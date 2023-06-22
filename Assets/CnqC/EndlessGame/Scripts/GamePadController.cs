using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

// viết code cho những input bàn phím chuột hay pad

public class GamePadController : MonoBehaviour

{
    public bool isOnMoblie;
    public static GamePadController ins; // singleton

    private bool m_canJump; // check xem ng chơi có jump hay không

    public bool CanJump { get => m_canJump; set => m_canJump = value; }

    private void Awake()
    {
        ins = this;
    }

    private void Update()
    {
        if (!isOnMoblie) // nếu như không phải dạng moblie thì người sẽ chuyển lấy thông tin từ bàn phím ấn xuống nút space.
            m_canJump = Input.GetKeyDown(KeyCode.Space);
                                // hàm này là bool nếu như mà check ng chơi có nhấn key thì sẽ ra true, còn k sẽ ra false
                                // chỉ ktra nhấn 1 lần chứ k ktra giữ
        

    }
}
