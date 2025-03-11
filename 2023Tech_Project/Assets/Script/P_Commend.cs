using System.Collections;
using UnityEngine;

public class P_Commend : MonoBehaviour
{   
    public float speed;
    public float RunBonus=1.3f;
    public FloatingJoystick Forcejoy;
    public FloatingJoystick RotationJoy;
    public static Rigidbody Rb;
    
    public GameObject player;
    private float BoundaryValueM=0;
    public static bool _isGrounded = true;
    public static bool _isGole = false;
    F_PlayerCamera F_PlayerCamera;
    private float timer = 0f;
    private float timerLand = 0f;
    private float interval = 0.35f;
    private int walkIndex = 0;


    public void PlayerRaycast()
    {
        RaycastHit rayhit;
        Physics.Raycast(Rb.transform.position, Vector3.down, out rayhit, 0.5f, LayerMask.GetMask("Ground"));
        if(rayhit.collider != null){
            //Debug.Log(rayhit.collider.gameObject.name);
            _isGrounded = true;

        }
        else{
            _isGrounded = false;
        }
    }
    public void IsGole(){
        RaycastHit rayhit;
        Physics.Raycast(Rb.transform.position, Vector3.down, out rayhit, 0.5f, LayerMask.GetMask("Gole"));
        if(rayhit.collider != null){
            _isGole = true;
            Debug.Log("골");
        }
        else{
            _isGole = false;
        }

    }
    void Awake()
    {
        // Rb 변수를 Awake() 메소드에서 할당
        Rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }
    public void PlayerRotationControl(){
        //플레이어의 로테이션을 조정하는 함수
        if(RotationJoy.Vertical != 0 || RotationJoy.Horizontal !=0)
        {
            Vector3 direction = Quaternion.Euler(0f, F_PlayerCamera.rotY, 0f) * new Vector3(RotationJoy.Horizontal, 0f, RotationJoy.Vertical).normalized;
            if(direction.magnitude > 0){
            player.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    public void PlayerLocationControl(){
        BoundaryValueM = Mathf.Abs(Forcejoy.Horizontal) + Mathf.Abs(Forcejoy.Vertical);
        Vector3 direction = Quaternion.Euler(0f,F_PlayerCamera.rotY, 0f) * new Vector3(Forcejoy.Horizontal, 0f, Forcejoy.Vertical).normalized;
        //Debug.Log(direction);
        if(BoundaryValueM >= 0.36f && _isGrounded == true){
            Rb.AddForce(direction * (speed*RunBonus) * Time.fixedDeltaTime, ForceMode.VelocityChange);
            P_Anime_Commend.playerRun();
            timer += Time.deltaTime;
            if (timer >= interval){WalkSound();}
        }
        else if(BoundaryValueM > 0.36f && _isGrounded == false){
            Rb.AddForce(direction * ((speed+80f)*RunBonus) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if(BoundaryValueM < 0.36 && _isGrounded == true){
            P_Anime_Commend.playerIdle();
        }
        if(_isGrounded == false){
            P_Anime_Commend.playerFalling();
            timer += Time.deltaTime;
            timerLand += Time.deltaTime;
            if (timer >= 4){PlayerFallingSoind();}
        }
        else if(_isGrounded == true){
            S_SoundManager.instance.StopSFX(S_SoundManager.Sfx.Falling);
            if(timerLand >= 6f){PlayerFallingHighSoind();}
            timerLand = 0f;
        }
    }

    public void WalkSound(){
        if(walkIndex == 0){
            S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Walk1);
            walkIndex++;
        }
        else if(walkIndex == 1){
            S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Walk2);
            walkIndex = 0;
        }
        timer = 0f;
    }
    public void PlayerFallingSoind(){
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Falling);
        timer = 0f;
    }
    public void PlayerFallingHighSoind(){
        S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.FallingToHigh);
        
        S_SoundManager.instance.PlayBgm(false);
        timerLand = 0f;
        StartCoroutine(PlayDelayedSound());
    }

    private IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(40f);
        Debug.Log("BGM재생");
        S_SoundManager.instance.PlayBgm(true);
    }
}
