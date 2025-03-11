using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class F_TimerAndClear : MonoBehaviour
{
    private UI_ButtonManager UI_ButtonManager;
    private float startTime;
    private int minutes;
    private int clearTime;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI clearText;

    public GameObject Main_Canvas;
    public GameObject clearCanvas;
    public GameObject Gold;
    public GameObject Silver;
    public GameObject Bronze;
    public Button Restart_button;



    // Start is called before the first frame update
    void Start()
    {
        UI_ButtonManager = FindObjectOfType<UI_ButtonManager>();
        startTime = Time.time;
        Restart_button.onClick.AddListener(UI_ButtonManager.Restart_ButtonClick);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(P_Commend._isGole == false){
            timer();
        }
        else{
            S_SoundManager.instance.PlaySFX(S_SoundManager.Sfx.Victory);
            Destroy(GameObject.Find("Goal"));
            Main_Canvas.SetActive(false);
            clearTime = minutes;
            clearCanvas.SetActive(true);
            clearText.text = "!!CLEAR!!\n\n"+"Clear Time\n" + clearTime + "Min\n"+ "Your Rank";
            if(clearTime <= 30){
                Gold.SetActive(true);
            }
            else if(clearTime <= 60){
                Silver.SetActive(true);
            }
            else{
                Bronze.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }
    void timer(){
        float elapsedTime = Time.time - startTime;
        minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);
        int milliseconds = (int)((elapsedTime * 1000f) % 1000f);

        string timerString = string.Format("{0:000}:{1:00}:{2:00}", minutes, seconds, milliseconds / 10);
        timerText.text = timerString;
    }
}
