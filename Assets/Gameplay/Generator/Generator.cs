using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{

    public zegar zegar;
    public Text Compression;
    public int LevelId;
    float timer;
    public int clock;
    public int MaxScheduledTrains;
    public int TimetableBeg, TimetableEnd;
    private TrainRecord TrainInfo;
    public List<TrainRecord> ScheduledTrains;
    public System.Random rand = new System.Random();
    public List<int> UsedId = new List<int>();
    public PeronFolder[] Platforms;
    public int CompressionRate=1;
    public bool Emergency = false;
    public PeronFolder ChoosenPlatform;
    public TrainRecord ChangingPlatTrain;
    public Text ChoosenPlatText;
    [HideInInspector]public int Points;
    [HideInInspector] public int BestScore, OldBestScore;
    [HideInInspector]public int Stars;
    public int OneStar, TwoStars, ThreeStars;
    public GameObject GameOverCanvas;
    public bool PreventFromZoom = false;
    public int RemaningTrains;
    public int TrainAmount = 0;
    public float ToFocusOn = 0;
    private bool[] StarsMessages = {false, false, false};
    bool HighScoreMessage = false;
    public List<MsgCreator> MessageList;
    public int probability;

    void MessageOrderGuard()
    {
        if (MessageList.Count > 0)
        {
            if (MessageList[0].DisplayedTime > 3f && MessageList.Count > 3 || MessageList[0].TimeOnTop>3f)
            {
                if (MessageList.Count > 0) MessageList[0].MessageDissapearOrder();
                if (MessageList.Count > 0) MessageList.RemoveAt(0);
                if (MessageList.Count > 0) MessageList[0].TimeOnTop = 0;
            };
        }
    }
    public void SaveToDisc()
    {
        if (BestScore == Points)
        {
            PlayerPrefs.SetInt(LevelId.ToString(), Points);
            PlayerPrefs.SetInt((-1*LevelId).ToString(), Stars);
        }

    }

    public void LoadDataToLevel()
    {
        BestScore = PlayerPrefs.GetInt(LevelId.ToString(), 0);
        OldBestScore = BestScore;
        GameObject.Find("Best").GetComponent<Text>().text = BestScore.ToString();
    }
    public void AddScoreDialog(int ScoreFactor, string Message)
    {

        GameObject NewDialogBox = Instantiate(Resources.Load("DialogMessage")) as GameObject;
        NewDialogBox.transform.SetParent(GameObject.Find("DialogBox").transform, false);
        NewDialogBox.GetComponent<MsgCreator>().Prepare(ScoreFactor, Message);
     }

    void GenerateTimetable()
    {
        ScheduledTrains = new List<TrainRecord>();
        for(int i = 0; i < MaxScheduledTrains; i++)
        {
            TrainInfo = gameObject.AddComponent<TrainRecord>() as TrainRecord ;
            TrainInfo.Prepare(this);
            ScheduledTrains.Add(TrainInfo);              
        }
    }

    void Clock()
    {
        timer += CompressionRate * Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            clock++;
        }
        if (clock == 86400) clock = 0;
        zegar.clock = clock;
    }

    void FillScore()
    {
        
        if (BestScore > OldBestScore && !HighScoreMessage)
        {
            AddScoreDialog(101, "New best score!");
            GameObject.Find("HighScore").GetComponent<AudioSource>().Play();
            HighScoreMessage = true;
        }
        if (HighScoreMessage && Points < OldBestScore) HighScoreMessage = false;
        GameObject.Find("Score").GetComponent<Text>().text = Points.ToString();
        if (Points >= BestScore)
        {
            GameObject.Find("Best").GetComponent<Text>().text = Points.ToString();
            BestScore = Points;
        }
        else if (Points < BestScore)
        {
            GameObject.Find("Best").GetComponent<Text>().text = OldBestScore.ToString();
            BestScore = OldBestScore;
        }
        if (Points < OneStar)
        {
            GameObject.Find("Stars").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Stars/0");
            Stars = 0;
        }
        else if (Points >= OneStar && Points < TwoStars)
        {
            GameObject.Find("Stars").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Stars/1");
            StarsMessages[1] = false;
            StarsMessages[2] = false;

            if (!StarsMessages[0])
            {
                AddScoreDialog(100, "You've gained first star");
                GameObject.Find("1 Star").GetComponent<AudioSource>().Play();
                StarsMessages[0] = true;
            }
            Stars = 1;
        }
        else if (Points >= TwoStars && Points < ThreeStars)
        {
            GameObject.Find("Stars").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Stars/2");
            StarsMessages[0] = false;
            StarsMessages[2] = false;
            if (!StarsMessages[1])
            {
                AddScoreDialog(100, "You've gained second star");
                GameObject.Find("2 Stars").GetComponent<AudioSource>().Play();
                StarsMessages[1] = true;
            }
            Stars = 2;
        }
        else if (Points >= ThreeStars)
        {
            GameObject.Find("Stars").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Stars/3");
            StarsMessages[0] = false;
            StarsMessages[1] = false;
            if (!StarsMessages[2])
            {
                AddScoreDialog(100, "You've gained third star");
                GameObject.Find("3 Stars").GetComponent<AudioSource>().Play();
                StarsMessages[2] = true;
            }
            Stars = 3;
        }
    }

    public void ShufflePlatforms()
    {
        for (int i = Platforms.Length - 1; i > 0; i--)
        {
            int swapIndex = rand.Next(i + 1);
            PeronFolder tmp = Platforms[i];
            Platforms[i] = Platforms[swapIndex];
            Platforms[swapIndex] = tmp;
        }
    }

    void SortRecords()
    {
        ScheduledTrains = ScheduledTrains.OrderBy(o => o.ArrivalTime).ThenBy(o=>o.DepartureTime).ToList();
    }

    void DisplayRecords()
    {
        foreach (TrainRecord rekord in ScheduledTrains)
        {
            GameObject nowyRecord = Instantiate(Resources.Load("PREFABS/Trains/Record")) as GameObject;
            nowyRecord.transform.SetParent(GameObject.Find("Rekordy").transform, false);
            Rekord newRecord = nowyRecord.GetComponent<Rekord>();
            newRecord.Train = rekord;
            rekord.TrainUnit.rekord = newRecord;
        }

    }

    public void GameOver(int Reason)
    {
        Time.timeScale = 0;
        if (Reason == 0)
        {
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Message.GetComponent<Text>().text = "Game over";
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Reason.GetComponent<Text>().text = "Multiple trains were going to the same semaphore!";
        }
        else if (Reason == 1)
        {
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Message.GetComponent<Text>().text = "Game over";
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Reason.GetComponent<Text>().text = "Multiple trains were on the same junction!";
        }
        else if (Reason == 2)
        {
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Message.GetComponent<Text>().text = "Game over";
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Reason.GetComponent<Text>().text = "You tried to switch junction with a train on it!";
        }

        else if (Reason == 3)
        {

            GameOverCanvas.GetComponent<GameOverCanvasScript>().Message.GetComponent<Text>().text = "Let's call it a day!";
            GameOverCanvas.GetComponent<GameOverCanvasScript>().Reason.GetComponent<Text>().text = "You've served all trains scheduled for today.";
        }

        GameOverCanvas.GetComponent<GameOverCanvasScript>().Score.GetComponent<Text>().text = "Your score: " + Points.ToString();
        GameOverCanvas.SetActive(true);
        PreventFromZoom = true;
        SaveToDisc();
    }

    private void Start()
    {
        Destroy(GameObject.Find("AudioManager"));
        Time.timeScale = 1;
        LoadDataToLevel();
        ShufflePlatforms();
        GenerateTimetable();
        SortRecords();
        DisplayRecords();
        RekordONChange.ResetNumber();
        RemaningTrains = MaxScheduledTrains;
        //PlayerPrefs.DeleteAll();
    }

    void FixedUpdate()
    {
        Compression.text = "x" + CompressionRate.ToString();
        Clock();
        FillScore();
        MessageOrderGuard();
    }
}
