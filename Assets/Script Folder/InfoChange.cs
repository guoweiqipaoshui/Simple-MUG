using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoChange : MonoBehaviour
{
    public Text MusicInfo;
    string[] StringMusicInfo = { "A Classic Chainsaw(Playing)", "A Classic Chainsaw(Stop)", "DriveMeWild(Playing)", "DriveMeWild(Stop)" };
    int currentMusic = 0;//切换音乐时改变相关信息
    bool IsPlaying = false;

    public GameObject delDataPanel;

    public GameObject HelpPanel;
    public GameObject EscPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeMusicInfo();
    }


    public void ChangeMusicInfo()//键盘输入
    {
        if ((Input.GetKeyDown(KeyCode.O) && currentMusic + 2 <= StringMusicInfo.Length - 1))
        {
            currentMusic += 2;
            MusicInfo.text = "Next one";
        }

        if (Input.GetKeyDown(KeyCode.I) && currentMusic - 2 >= 0)
        {
            currentMusic -= 2;
            MusicInfo.text = "Last one";
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPlaying == false)
            {
                IsPlaying = true;
                MusicInfo.text = StringMusicInfo[currentMusic];
            }

            else
            {
                MusicInfo.text = StringMusicInfo[currentMusic + 1];
                IsPlaying = false;
            }
        }
    }

    public void ChangeMusicInfo(int a)//手机输入
    {
        if ((a == 1 && currentMusic + 2 <= StringMusicInfo.Length - 1))
        {
            currentMusic += 2;
            MusicInfo.text = "Next one";
        }

        if (a == 2 && currentMusic - 2 >= 0)
        {
            currentMusic -= 2;
            MusicInfo.text = "Last one";
        }

        if(a == 3)
            delDataPanel.SetActive(true);
        if (a == 4)
            delDataPanel.SetActive(false);

        if (a == 0)
        {
            if (IsPlaying == false)
            {
                MusicInfo.text = StringMusicInfo[currentMusic];
                IsPlaying = true;
                
            }

            else
            {
                MusicInfo.text = StringMusicInfo[currentMusic + 1];
                IsPlaying = false;
            }
        }
    }

    public void ScenesChange(int a)
    {
        if(a == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        if(a == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void EscPanelC(int a)
    {
        Time.timeScale = 0;
        switch (a)
        {
            case 0:
                EscPanel.SetActive(true);
                break;
            case 1:
                EscPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case 2:
                HelpPanel.SetActive(true);
                Time.timeScale = 0;
                break;
            case 3:
                HelpPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case 4:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
        }
    }
}
