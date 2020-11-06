using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject VideoPanel;
    public GameObject SoundOnImage;
    public GameObject SoundOfImage;
    public GameObject[] Hearth;
    public bool anylife = false;
    string itime;

    DateTime NowTime;
    DateTime endTime;
    System.TimeSpan diff1;
    


    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return GameManager.instance;
        }
    }

   
    void Start()
    {
        
        if (PlayerPrefs.GetInt("FirstPlay") == 0)             //oyuna ilk defa giriyorsa 3 can hakkı veriyoruz.
        {
            HearthAdd();
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
       
        if (PlayerPrefs.GetInt("Life")<=0)           //sahneye geldiğimizde hakkımız yoksa geri sayım için playerprefbsten saatimizi çekiyoruz.
        {
           Debug.Log(PlayerPrefs.GetInt("Life"));
           anylife = true;
           endTime=Convert.ToDateTime(PlayerPrefs.GetString("itime"));
        
        }
        CheckSound();                                       //sesi ve canları kontrol ediyoruz. Eksik can varsa İmage kapanır.
        CheckHearth();

    }

    public void HearthAdd()
    {
        for (int i = 0; i < Hearth.Length; i++)
        {
            Hearth[i].SetActive(true);
        }
        PlayerPrefs.SetInt("Life", 3);
        anylife = false;
    }
    public void HearthDeleted()
    {
        for (int i = Hearth.Length-1; i >= 0; i--)
        {
            if (Hearth[i].activeSelf == true)
            {
                Hearth[i].SetActive(false);
                PlayerPrefs.SetInt("Life", (PlayerPrefs.GetInt("Life") - 1));
                if(PlayerPrefs.GetInt("Life")<=0)
                {
                    PlayerPrefs.SetInt("Life", 0);
                    TimeCounter();
                }
                break;
            }
        }
    }
    public void CheckHearth()
    {
        int life = PlayerPrefs.GetInt("Life");
        for (int i = 0; i < life; i++)
        {
            Hearth[i].SetActive(true);
        }
    }

    public void sound(int index)                // sesin açılması için butondan index değeri gelir. 1 açar 2 kapatır.
    {
        if (index == 1)                 
        {
            PlayerPrefs.SetInt("sound", 1);
            AudioListener.volume = 0;           // sesi kapatmanın bir kaç yolu var build alıp denediğimde androidde bu metodun doğru çalıstığını test ettim..
        }else if(index == 2)
        {
            PlayerPrefs.SetInt("sound", 2);
            AudioListener.volume = 1;
        }

    }
    public void CheckSound()                    // sahne açılısında kullanıcının son tercihini kontrol ediyoruz. Sesi kapatmışsa ve açmamışsa tekrar aynı ayarlaran başlıyoruz.
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            SoundOnImage.gameObject.SetActive(false);
            SoundOfImage.gameObject.SetActive(true);
            AudioListener.volume = 0;
        }
        if (PlayerPrefs.GetInt("sound") == 2)
        {
            SoundOnImage.gameObject.SetActive(true);
            SoundOfImage.gameObject.SetActive(false);
            AudioListener.volume = 1;
        }
    }

    public void nextScene()
    {
        if(PlayerPrefs.GetInt("Life")==0)
        {
            VideoPanel.SetActive(true);
            
        }
        else if(PlayerPrefs.GetInt("Life")>0)
        {
            HearthDeleted();
            SceneManager.LoadScene("PlayGame");
        }
    }

    public void TimeCounter()
    {
        endTime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day+1, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);

        anylife = true;
        itime = endTime.ToString();
        PlayerPrefs.SetString("itime", itime);
    }
    private void Update()
    {
        Debug.Log(anylife);
        if (anylife == true)
        {
            NowTime = System.DateTime.Now;
            diff1 = endTime - NowTime;
            int Can = DateTime.Compare(endTime, NowTime);                   // timeları compare ediyoruz 0 ve küçük olduğunda zamanımız dolmus oluyor.
            if (Can < 0)
            {
                HearthAdd();
                anylife = false;
            }
            timeTxt.text = diff1.Hours.ToString() + " " + diff1.Minutes.ToString() + " " + diff1.Seconds.ToString();
            Debug.Log(endTime+"end");
        }
    }
}

