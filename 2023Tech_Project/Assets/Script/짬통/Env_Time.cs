using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Time : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    int tensPlaceMinutes = 0;
    float sunAngle = 0;
    int minute;
    int hour;
    int date = 0;
    bool dateFlag = true;
    [SerializeField] private float fogDensityCalc;//증감량 비율
    [SerializeField] private float nightFogDensity;//밤 상태의 안개 밀도
    [SerializeField] private float dayFogDensity;//낮 상태의 안개 밀도
    private float currentFogDensity;//현재 안개 밀도
    private bool isNight=false ;

    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }
    void FixedUpdate()
    {
        sunAngle += Time.deltaTime * rotationSpeed;
        hour = Mathf.FloorToInt((sunAngle / 15) + 6);
        minute = Mathf.FloorToInt((sunAngle * 4) % 60);

        tensPlaceMinutes = (minute / 10) * 10;
        if (tensPlaceMinutes >= 60)
        {
            hour += 1;
            tensPlaceMinutes = 0;
        }
        hour = hour % 24;

        transform.rotation = Quaternion.Euler(sunAngle, 45f, 30f);
        
        if(hour==23)
        {
            dateFlag=true;
        }
        if (dateFlag==true && hour==0)
        {
            date += 1;
            dateFlag=false;
        }
        DayFogDensity();
        Debug.Log(currentFogDensity);
    }

    void DayFogDensity()
    {
        isNight=(hour >= 18 || hour <= 6) ? true : false;
        currentFogDensity = (isNight) ? nightFogDensity : dayFogDensity;
        if(isNight){
            if(currentFogDensity <= dayFogDensity){
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else{
            if(currentFogDensity >= nightFogDensity){
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
