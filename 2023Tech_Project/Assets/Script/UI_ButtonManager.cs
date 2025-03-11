using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class UI_ButtonManager : MonoBehaviour
{
    public Button UI_btn_Jump;
    private P_Commend p_commend;
    bool SettingFlag = false;
    private int maxJumpCount = 1;
    private int jumpCount = 0;

    public GameObject Setting_Canvas;
    public GameObject Main_Canvas;
    public GameObject Start_Canvas;
    public FloatingJoystick F_PuseJoy;

    public Button Setting_button;
    public Button MenuExit_button;
    public Button Restart_button;
    public Button GameExit_button;
    public Button Start_Canvas_button;
    
    void Start()
    {   
        BeforeStartSetting();
        P_Anime_Commend.textFade();
        if(P_Commend._isGrounded == true){
            UI_btn_Jump.onClick.AddListener(Jump_btn_Function);
            p_commend = FindObjectOfType<P_Commend>();
        }
            Start_Canvas_button.onClick.AddListener(AfterStartSetting);
            Setting_button.onClick.AddListener(Setting_ButtonOnClick);
            MenuExit_button.onClick.AddListener(MenuExit_ButtonClick);
            Restart_button.onClick.AddListener(Restart_ButtonClick);
            GameExit_button.onClick.AddListener(GameExit_ButtonClick);
    }
    //게임 시작 전,후 활성화 기능
    void BeforeStartSetting()//게임 시작전 버튼 비활성화
    {
            S_SoundManager.instance.PlayBgm(true);
            Main_Canvas.SetActive(false);
            Time.timeScale = 1;
    }
    void AfterStartSetting(){//게임 시작후 버튼 활성화
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.StartFalling);
        Debug.Log("게임시작");
        Time.timeScale = 1;
        F_PlayerCamera.followSpeed = 123f;
        Main_Canvas.SetActive(true);
        Start_Canvas.SetActive(false);
        Setting_Canvas.SetActive(false);

    }
    void Jump_btn_Function()//점프버튼
    {
        if(P_Commend._isGrounded == true){
            P_Commend.Rb.AddForce(Vector3.up * 15f, ForceMode.VelocityChange);
            P_Anime_Commend.playerJump();
            S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Jump1);
            jumpCount=0;
        }
        else{
            if(jumpCount < maxJumpCount){
                P_Commend.Rb.AddForce(Vector3.up * 8f, ForceMode.VelocityChange);
                P_Anime_Commend.playerJump();
                S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Jump2);
                jumpCount++;
            }
        }
    }
    
    
    //___________________________________________________________
    //메뉴버튼
    void Setting_ButtonOnClick()
    {
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.MenuOpen);
        Setting_Canvas.SetActive(true);
        Setting_button.gameObject.SetActive(false);
        UI_btn_Jump.gameObject.SetActive(false);
        F_PuseJoy.gameObject.SetActive(false);
        //게임 일시정지
        Time.timeScale = 0;
    }
    //메뉴나가기버튼
    public void MenuExit_ButtonClick()
    {
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.MenuExit);
        Setting_Canvas.SetActive(false);
        Setting_button.gameObject.SetActive(true);
        UI_btn_Jump.gameObject.SetActive(true);
        F_PuseJoy.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    //리스타트버튼
    public void Restart_ButtonClick()
    {
        F_PlayerCamera.followSpeed = 0f;
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.BtnClick);
        StartCoroutine(RestartWithDelay(0.2f));
        Time.timeScale = 1;
    }
    //리스타트버튼 딜레이
    private IEnumerator RestartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);//delay초만큼 대기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//현재 씬을 다시 로드
    }
    
    //게임종료버튼
    public void GameExit_ButtonClick()
    {
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.BtnClick);
        Application.Quit();
     }

}
