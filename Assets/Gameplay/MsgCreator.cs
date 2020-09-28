using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgCreator : MonoBehaviour {
    public Text Point;
    public Text Text;
    int ScoreBalance;
    Color32 MessageColor;
    Color32 ColorOff;
    Color32 Red = new Color32(244, 66, 116, 255);
    Color32 Green = new Color32(65, 244, 77, 255);
    Color32 Gold = new Color32(255, 215, 0, 255);
    Color32 NiewiadomoJaki = new Color32(221, 253, 255, 255);
    bool dissapear=false;
    bool Visible = true;
    bool Rolled = false;
    public float DisplayedTime = 0;
    public float TimeOnTop = 0;

    float lerpTime = 10000000000f;
    float currentLerpTime;



    public  void MessageDissapearOrder()
    {
        dissapear = true;
    }
    void MessageDestroy()
    {
        if (dissapear && Visible)
        {
            currentLerpTime = 0f;           

            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;
            Point.color = Color32.Lerp(ColorOff, MessageColor, perc);
            Text.color = Color32.Lerp(ColorOff, MessageColor, perc);
        }
        if (Point.color.a == 0 && dissapear )
        {
            Visible = false;
            GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(GetComponent<RectTransform>().sizeDelta, new Vector2(0, 0),Time.deltaTime*4);
            if(GetComponent<RectTransform>().sizeDelta == new Vector2(0, 0)) Rolled = true;
        }

        if (Point.color.a == 0 && dissapear && Rolled)
        {
           Destroy(gameObject);
        }


    }
    public void Prepare(int ScoreFactor, string Message)
    {
        TrainGame.Generator().MessageList.Add(this);
        if (ScoreFactor < 0)
        {
            MessageColor = Red;
        }
        else if (ScoreFactor == 100)
        {
            MessageColor = Gold;
        }
        else if (ScoreFactor == 101)
        {
            MessageColor = NiewiadomoJaki;
        }
        else
        {
            MessageColor = Green;
        }
        ColorOff = MessageColor;
        ColorOff.a = 0;
        Text.color = MessageColor;
        Point.color = MessageColor;

        if (ScoreFactor != 100 && ScoreFactor!=101) Point.text = ScoreFactor.ToString();
        else if(ScoreFactor==100 || ScoreFactor==101) Point.text = "";
        Text.text = Message;
        ScoreBalance = ScoreFactor;
    }
    void FixedUpdate()
    {
        DisplayedTime += Time.deltaTime;
        TimeOnTop += Time.deltaTime;
        MessageDestroy();
    }
}
