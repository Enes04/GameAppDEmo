using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject Floor;
    public GameObject CurretFloor;
    public Text ScoreText;
    public Image PrograssBar;

    private float Score;
   

    public Stack<GameObject> Floorpool { get; set; } = new Stack<GameObject>();

    private static PlayManager instance;
    public static PlayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayManager>();
            }
            return PlayManager.instance;
        }
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            AudioListener.volume = 0;
        }
        if (PlayerPrefs.GetInt("sound") == 2)
        {
            AudioListener.volume = 1;
        }

        for (int i = 0; i < 2; i++)
        {
            CreateFloor();
        }
        for (int i = 0; i < 4; i++)
        {
            SpawnFloor();
        }
        
    }

   
    void Update()
    {
        
    }

    public void CreateFloor()
    {
        Floorpool.Push(Instantiate(Floor));    
        Floorpool.Peek().SetActive(false);
    }

    public void SpawnFloor()                                    // Zeminimizi stack yapısında saklıyorum. 
    {
        if (Floorpool.Count == 0)
        {
            CreateFloor();
        }
      
        GameObject temp = Floorpool.Pop();           
        temp.SetActive(true);                       
        temp.transform.position = CurretFloor.transform.GetChild(0).GetChild(0).position;
        CurretFloor = temp;

        for (int i = 0; i < temp.transform.GetChild(0).GetChild(1).childCount; i++)
        {
               
         temp.transform.GetChild(0).GetChild(1).GetChild(i).GetChild(Random.Range(0,3)).gameObject.SetActive(true);
        }
    }
   
    public void ScoreValue(int point)
    {
        Score = Score + point;
        ScoreText.text = "Toplanan Obje Sayisi: " + Score.ToString();
        PrograssBar.fillAmount = Score / 100;
        if(PrograssBar.fillAmount == 1)
        {
            Pause();
        }
    }
    public void Pause()
    {
        PlayerControl.Instance.anim.GetComponent<Animator>().enabled = false;
        PlayerControl.Instance.end = true;
        PausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        PlayerControl.Instance.anim.GetComponent<Animator>().enabled = true;
        PlayerControl.Instance.end = false;
        PausePanel.SetActive(false);
    }
    public void menu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
