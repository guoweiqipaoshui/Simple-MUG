using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour
{
    float currentTrackIdx = 999;
    float currentTrackIdy = 999;

    int currentHitPos = 999;

    public Image[] tracksMgs;

    public Transform[] tracksPoses;

    public Transform line;

    public GameObject pointPre;

    public AudioSource[] bgmList;

    int currentBgm = 0;

    public AudioSource bgm;

    public Slider bgmSd;

    bool SpaceSW = false;

    bool dragSlider = false;

    //bool ClickEvent = false;

    //float cooldown = 0.1f;

    public TrackTimerLists_Dic[] trackTimerLists_Dic;

    public int currentTrackCounts = 4;


    public Button[] Arrow;

    void Start()
    {
        bgm.Pause();
    }

    private void Update()
    {
        OnClickPlay();
        bgmSlider();
        PointLine();
        bgmSW();
    }

    void OnClickPlay()//bgm控制
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(bgm.isPlaying)
            {
                bgm.Pause();
            }
            else
            {
                bgm.Play();
            }
        }
    }

    public void ButtonPlay()//手机bgm控制
    {
        if (bgm.isPlaying)
        {
            bgm.Pause();
        }
        else
        {
            bgm.Play();
        }
    }

    void bgmSlider()//bgm进度条
    {
        if(!dragSlider)
        {
            bgmSd.value = bgm.time / bgm.clip.length;
        }else
        {
            bgm.time = bgmSd.value + bgm.clip.length;
        }
    }

    void bgmSW()//切换bgm
    {
        if (Input.GetKeyDown(KeyCode.O) && bgmList[currentBgm + 1] != null)
        {
            bgm.Pause();
            currentBgm = currentBgm + 1;
            bgm = bgmList[currentBgm];
        }

        if (Input.GetKeyDown(KeyCode.I) && bgmList[currentBgm - 1] != null)
        {
            bgm.Pause();
            currentBgm = currentBgm - 1;
            bgm = bgmList[currentBgm];
        }
        
    }

    void PointLine()//音节进度条
    {
        if (bgm.isPlaying)
        {
            foreach (var point in trackTimerLists_Dic[currentBgm].trackTimerLists)
            {
                point.gameObject.transform.position = new Vector3(point.trackIdx, point.trackIdy, (bgm.time - point.timer) * 10 - 2);
            }
        }
        ///cooldown -= Time.deltaTime;


        //if(cooldown <= 0)
        //{
        //cooldown = 0.11f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SpaceSW == false)
                SpaceSW = true;
            else
                SpaceSW = false;
        }


            AddPointFromKeyCode(KeyCode.UpArrow);//生成音节
            AddPointFromKeyCode(KeyCode.DownArrow);
            AddPointFromKeyCode(KeyCode.LeftArrow);
            AddPointFromKeyCode(KeyCode.RightArrow);
            
        //}
    }

    public void OnClick(int sender)//手机输入
    {
        //bgm切换
        if (sender == 8 && bgmList[currentBgm + 1] != null)
        {
            bgm.Pause();
            currentBgm = currentBgm + 1;
            bgm = bgmList[currentBgm];
            return;
        }

        if (sender == 9 && bgmList[currentBgm - 1] != null)
        {
            bgm.Pause();
            currentBgm = currentBgm - 1;
            bgm = bgmList[currentBgm];
            return;
        }
        if (sender == 10)
        {
            ClearDic(currentBgm);
            return;
        }

        //方向键控制
        if (SpaceSW == true)
        {
            switch (sender)
            {
                case 0:
                    currentTrackIdx = tracksPoses[0].position.x;
                    currentTrackIdy = tracksPoses[0].position.y;
                    currentHitPos = 0;
                    break;
                case 1:
                    currentTrackIdx = tracksPoses[1].position.x;
                    currentTrackIdy = tracksPoses[1].position.y;
                    currentHitPos = 1;
                    break;
                case 2:
                    currentTrackIdx = tracksPoses[2].position.x;
                    currentTrackIdy = tracksPoses[2].position.y;
                    currentHitPos = 2;
                    break;
                case 3:
                    currentTrackIdx = tracksPoses[3].position.x;
                    currentTrackIdy = tracksPoses[3].position.y;
                    currentHitPos = 3;
                    break;
                case 4:
                    SpaceSW = false;
                    break;
            }
        }
        else
        {
            switch (sender)
            {
                case 0:
                    currentTrackIdx = tracksPoses[4].position.x;
                    currentTrackIdy = tracksPoses[4].position.y;
                    currentHitPos = 4;
                    break;
                case 1:
                    currentTrackIdx = tracksPoses[5].position.x;
                    currentTrackIdy = tracksPoses[5].position.y;
                    currentHitPos = 5;
                    break;
                case 2:
                    currentTrackIdx = tracksPoses[6].position.x;
                    currentTrackIdy = tracksPoses[6].position.y;
                    currentHitPos = 6;
                    break;
                case 3:
                    currentTrackIdx = tracksPoses[7].position.x;
                    currentTrackIdy = tracksPoses[7].position.y;
                    currentHitPos = 7;
                    break;
                case 4:
                    SpaceSW = true;
                    break;
            }
        }
         AddPoint(currentTrackIdx, currentTrackIdy, bgm.time,currentHitPos);     
    }

    public void AddPointFromKeyCode(KeyCode keycode) //键盘输入
    {
        
        if(Input.GetKeyDown(keycode))
        {
            if (SpaceSW == true)
            {
                switch (keycode)
                {
                    case KeyCode.UpArrow:
                        currentTrackIdx = tracksPoses[0].position.x;
                        currentTrackIdy = tracksPoses[0].position.y;
                        currentHitPos = 0;
                        break;
                    case KeyCode.DownArrow:
                        currentTrackIdx = tracksPoses[1].position.x;
                        currentTrackIdy = tracksPoses[1].position.y;
                        currentHitPos = 1;
                        break;
                    case KeyCode.LeftArrow:
                        currentTrackIdx = tracksPoses[2].position.x;
                        currentTrackIdy = tracksPoses[2].position.y;
                        currentHitPos = 2;
                        break;
                    case KeyCode.RightArrow:
                        currentTrackIdx = tracksPoses[3].position.x;
                        currentTrackIdy = tracksPoses[3].position.y;
                        currentHitPos = 3;
                        break;
                }
            }
            else
            {
                switch (keycode)
                {
                    case KeyCode.UpArrow:
                        currentTrackIdx = tracksPoses[4].position.x;
                        currentTrackIdy = tracksPoses[4].position.y;
                        currentHitPos = 4;
                        break;
                    case KeyCode.DownArrow:
                        currentTrackIdx = tracksPoses[5].position.x;
                        currentTrackIdy = tracksPoses[5].position.y;
                        currentHitPos = 5;
                        break;
                    case KeyCode.LeftArrow:
                        currentTrackIdx = tracksPoses[6].position.x;
                        currentTrackIdy = tracksPoses[6].position.y;
                        currentHitPos = 6;
                        break;
                    case KeyCode.RightArrow:
                        currentTrackIdx = tracksPoses[7].position.x;
                        currentTrackIdy = tracksPoses[7].position.y;
                        currentHitPos = 7;
                        break;
                }
            }
            
            AddPoint(currentTrackIdx, currentTrackIdy,  bgm.time, currentHitPos);
        }
    }


    public void AddPoint(float trackIdx, float trackIdy, float currentTime, int hitPos)//增加音节
    {
        PointGameObject pNode = new PointGameObject();
        pNode.timer = currentTime;
        pNode.trackIdx = trackIdx;
        pNode.trackIdy = trackIdy;
        pNode.hitPos = hitPos;
        pNode.gameObject = Instantiate(pointPre);
        //pNode.gameObject.transform.parent =line;
        trackTimerLists_Dic[currentBgm].trackTimerLists.Add(pNode);
    }

    public void ClearDic(int num)
    {
        trackTimerLists_Dic[currentBgm].trackTimerLists.Clear();
    }
}
