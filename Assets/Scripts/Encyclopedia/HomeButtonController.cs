using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButtonController : MonoBehaviour
{   //Panel首页
    public GameObject homePanel;
    public Button hp1Button;
    public Button pp1Button;
    public Button tp1Button;
   
    //Panel历史及发展
    public GameObject historyhomePanel;
    //Panel历史首页
    public GameObject historyPanel1;
    public Button hp2Button;
    public Button hp3Button;
    public Button hp4Button;
    public Button hp5Button;
    public Button hp6Button;
    public Button hp1backtohome;
    //Panel简介
    public GameObject historyPanel2;
    public Button hp2backtohp1;
    //Panel发明历程
    public GameObject historyPanel3;
    public Button hp3backtohp1;
    //Panel文献记载
    public GameObject historyPanel4;
    public Button hp4backtohp1;
    //Panel世界发展
    public GameObject historyPanel5;
    public Button hp5backtohp1;
    //Panel争议
    public GameObject historyPanel6;
    public Button hp6backtohp1;

    //Panel相关人物
    public GameObject peoplehomePanel;
    //panel人物首页
    public GameObject peoplePanel1;
    public Button pp2Button;
    public Button pp3Button;
    public Button pp4Button;
    public Button pp5Button;
    public Button pp1backtohome;
    //Panel毕昇
    public GameObject peoplePanel2;
    public Button pp2backtopp1;
    //Panel王祯
    public GameObject peoplePanel3;
    public Button pp3backtopp1;
    //Panel约翰.古腾堡
    public GameObject peoplePanel4;
    public Button pp4backtopp1;
    //Panel朱权
    public GameObject peoplePanel5;
    public Button pp5backtopp1;

    //Panel工艺技术
    public GameObject techhomePanel;
    //Panel技术首页
    public GameObject techPanel1;
    public Button tp1Totp2Button;
    public Button tp1Totp3Button;
    public Button tp1backtohome;
    //Panel文字描述
    public GameObject techPanel2;
    public Button tp2backtotp1;
    //Panel视频
    public GameObject techPanel3;
    public Button tp3backtotp1;

    void Start()
    {
        home();
        hp1Button.onClick.AddListener(hp1);
        pp1Button.onClick.AddListener(pp1);
        tp1Button.onClick.AddListener(tp1);

        hp2Button.onClick.AddListener(hp2);
        hp3Button.onClick.AddListener(hp3);
        hp4Button.onClick.AddListener(hp4);
        hp5Button.onClick.AddListener(hp5);
        hp6Button.onClick.AddListener(hp6);

        pp2Button.onClick.AddListener(pp2);
        pp3Button.onClick.AddListener(pp3);
        pp4Button.onClick.AddListener(pp4);
        pp5Button.onClick.AddListener(pp5);

        tp1Totp2Button.onClick.AddListener(tp2);
        tp1Totp3Button.onClick.AddListener(tp3);

        hp1backtohome.onClick.AddListener(home);
        hp2backtohp1.onClick.AddListener(hp1);
        hp3backtohp1.onClick.AddListener(hp1);
        hp4backtohp1.onClick.AddListener(hp1);
        hp5backtohp1.onClick.AddListener(hp1);
        hp6backtohp1.onClick.AddListener(hp1);
        pp1backtohome.onClick.AddListener(home);
        pp2backtopp1.onClick.AddListener(pp1);
        pp3backtopp1.onClick.AddListener(pp1);
        pp4backtopp1.onClick.AddListener(pp1);
        pp5backtopp1.onClick.AddListener(pp1);
        tp1backtohome.onClick.AddListener(home);
        tp2backtotp1.onClick.AddListener(tp1);
        tp3backtotp1.onClick.AddListener(tp1);
    }
    
    public void allfalse()
    {
     homePanel.SetActive(false);
     historyhomePanel.SetActive(false);
     historyPanel1.SetActive(false);
     historyPanel2.SetActive(false);
     historyPanel3.SetActive(false);
     historyPanel4.SetActive(false);
     historyPanel5.SetActive(false);
     historyPanel6.SetActive(false);
     peoplehomePanel.SetActive(false);
     peoplePanel1.SetActive(false);
     peoplePanel2.SetActive(false);
     peoplePanel3.SetActive(false);
     peoplePanel4.SetActive(false);
     peoplePanel5.SetActive(false);
     techhomePanel.SetActive(false);
     techPanel1.SetActive(false);
     techPanel2.SetActive(false);
     techPanel3.SetActive(false);
    }

    public void home()
    {
     allfalse();
     homePanel.SetActive(true);
    }

    public void hp1()
    {
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel1.SetActive(true);
    }

    public void pp1()
    {
     allfalse();
     peoplehomePanel.SetActive(true);
     peoplePanel1.SetActive(true);
    }

    public void tp1()
    {
     allfalse();
     techhomePanel.SetActive(true);
     techPanel1.SetActive(true);
    }

    public void hp2(){
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel2.SetActive(true);
     
    }

     public void hp3(){
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel3.SetActive(true);
    }

     public void hp4(){
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel4.SetActive(true);
    }

    public void hp5(){
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel5.SetActive(true);
    }

    public void hp6(){
     allfalse();
     historyhomePanel.SetActive(true);
     historyPanel6.SetActive(true);
    }

    public void pp2(){
     allfalse();
     peoplehomePanel.SetActive(true);
     peoplePanel2.SetActive(true);
    }

    public void pp3(){
     allfalse();
     peoplehomePanel.SetActive(true);
     peoplePanel3.SetActive(true);
    }

    public void pp4(){
     allfalse();
     peoplehomePanel.SetActive(true);
     peoplePanel4.SetActive(true);
    }

    public void pp5(){
     allfalse();
     peoplehomePanel.SetActive(true);
     peoplePanel5.SetActive(true);
    }
   
    public void tp2(){
     allfalse();
     techhomePanel.SetActive(true);
     techPanel2.SetActive(true);
    }

    public void tp3(){
     allfalse();
     techhomePanel.SetActive(true);
     techPanel3.SetActive(true);
    }
}
