using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnqC.EndLessGame;

// xử lý các dữ liệu

public enum GameTag // khai báo các tag ở bên ngoài, ta phải đặt trùng tên với các tag bên ngoài
{
    Player,
    Block,
    DeadZone
}

public enum GameLayer
{ 
    Player,
    Block,
    Dead
}

public enum ChacAnim // lưu lại tham số của phần animator 
{
    Idle,
    Jump,
    Land,
    Dead
} 

public enum GamePref // lưu trữ các key để lưu xuống máy người dùng
{
    BestScore,
    LevelUnlocked,
    CurLevelId, // chỉ của con nhân vật ở level nào mà người chơi đã sở hữu
    IsMusicOn,
    IsSoundOn // để lưu lại trạng thái của music vs sound là đã bật hay chưa trong phần setting
}


public enum GameScene
{
    // lưu lại tên các scene

    MainMenu,
    GamePlay

}

public enum MoveDirection // hướng di chuyển
{
    Left,
    Right
}

public enum GameState
{
    // lưu lại trạng thái của game

    Starting,
    Playing,
    Gameover
}

// hiện thị ra ngoài 
[System.Serializable]
public class LevelItem
{
    public int scoreRequire; // điểm số yêu cầu để unlock level mới
    public Sprite unlockThumb;
    public Sprite lockThumb;
    public Sprite levelBG;
    public Sprite chacPreviewImg;

    public Player playerPb;
    public Block blockPb;
    public float spawnTime;
    public float baseSpeed; // tốc độ di chuyển căn bản của block
    public float maxSpeed; 



}