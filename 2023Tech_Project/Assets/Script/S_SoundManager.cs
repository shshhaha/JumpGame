using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SoundManager : MonoBehaviour
{
    public static S_SoundManager instance;
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource []sfxPlayers;
    int channelIndex;

    public enum Sfx {MenuExit = 0,MenuOpen = 1, BtnClick = 2, Walk1 = 3, Walk2 = 4, Jump1 = 5, Jump2 = 6, StartFalling = 7, Falling = 8, FallingToHigh = 9, Victory = 10}

    void Awake(){
        instance = this;
        Init();
    }

    void Init(){
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++){
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
         
    }

    public void PlayBgm(bool isPlay){
        if(isPlay){
            bgmPlayer.Play();
        }
        else{
            bgmPlayer.Stop();
        }
    }

    public void PlaySFX(Sfx sfx){
        for(int i = 0; i < sfxPlayers.Length; i++){
            int loopIndex = (i + channelIndex)%sfxPlayers.Length;
            
            if(sfxPlayers[loopIndex].isPlaying)
                continue;

                channelIndex = loopIndex;
                sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
                sfxPlayers[loopIndex].Play();
                break;
        }        
    }
    public void StopSFX(Sfx sfx){
        for(int i = 0; i < sfxPlayers.Length; i++){
            if(sfxPlayers[i].clip == sfxClip[(int)sfx] && sfxPlayers[i].isPlaying){
                sfxPlayers[i].Stop();
                break;
            }
        }
    }
}
