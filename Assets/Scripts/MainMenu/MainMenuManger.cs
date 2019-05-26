using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using Newtonsoft.Json;

public class MainMenuManger : MonoBehaviour
{
    public Text file_name;
    public Text savetime;
    public Text savedetialtime;
    public InputField inputname;
    public GameObject newgame;
    public GameObject loadfile;
    private List<SaveFile> files;
    public GameObject nextbtn;
    public GameObject lastbtn;
    private string year;
    private string month;
    private string day;
    private string hour;
    private string minute;
    private string second;
    private string time;
    private string detailtime;
    private string filename;
    private int index = 0;

    private void Awake()
    {
        string filepath = Application.dataPath + "/Doc" + "/flielist.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            files = JsonConvert.DeserializeObject<List<SaveFile>>(str);//反序列化为List
            Debug.Log(str);
        }

    }

    public void NewGame()
    {
        newgame.SetActive(true);
       //SceneManager.LoadScene(1);
    }

    public void LoadFile(SaveFile file)
    {
        file_name.text = file.file_name;
        savetime.text = file.savetime;
        savedetialtime.text = file.savedetailtime;
    }

    public void SaveFileToJson()
    {
        int index = 0;
        List<Card> lic = new List<Card>();
        for (int i = 0; i < 7; i++)
        {
            string xxx = "old wang" + index.ToString();
            Card s = new Card(index, xxx, "强", "攻击", 10, 20, 1, 0,10, CardType.Attack, SkillType.Attack);
            lic.Add(s);//添加卡牌到List
                       // lic.Add(s.card_id, s);
            index++;
        }
        string ddd = JsonConvert.SerializeObject(lic, Formatting.Indented);//序列化卡牌为JSON格式
        Debug.Log(ddd);
        string filepath = Application.dataPath + "/Doc/"+filename  + "/cardlist_all.json";
        //string farmatc = JsonMapper.ToJson(save);
        StreamWriter sw = new StreamWriter(filepath);//保存到指定路径文件
        sw.Write(ddd);
        sw.Close();
        if (File.Exists(filepath))
        {

            Debug.Log("Save Successd");
        }
    }

    public void GetName()
    {
        filename = inputname.text;
        Playerdata playerdata = new Playerdata(filename,100, 0, 0, 0, 0, 0, 0);
        PlayerManger playerManger = new PlayerManger();
        playerManger.PlayerDataSet(playerdata);
        year = DateTime.Now.Year.ToString();
        month = DateTime.Now.Month.ToString();
        day = DateTime.Now.Day.ToString();
        hour = DateTime.Now.Hour.ToString();
        minute = DateTime.Now.Minute.ToString();
        second = DateTime.Now.Second.ToString();
        time = year + "年" + month + "月" + day + "日";
        detailtime = hour + ":" + minute + ":" + second;
        SaveFile saveFile = new SaveFile(filename, time, detailtime);
        List<Card> newcardlist = CreatCardList();
        string file = JsonConvert.SerializeObject(newcardlist, Formatting.Indented);
        string data = JsonConvert.SerializeObject(playerdata, Formatting.Indented);
        files.Add(saveFile);
        string filestext = JsonConvert.SerializeObject(files, Formatting.Indented);
        string filelistpath = Application.dataPath + "/Doc" + "/flielist.json";
        string savepath = Application.dataPath + "/Doc/" + saveFile.file_name;
        if (!Directory.Exists(savepath))
        {
            Directory.CreateDirectory(savepath);
            StreamWriter sw = new StreamWriter(savepath + "/cardlist.json");
            sw.Write(file);
            sw.Close();
            StreamWriter saveplayerdata = new StreamWriter(savepath + "/playerdata.json");
            saveplayerdata.Write(data);
            saveplayerdata.Close();
            StreamWriter filelistwriter = new StreamWriter(filelistpath);
            filelistwriter.Write(filestext);
            filelistwriter.Close();
            SceneManager.LoadScene(1);
            Debug.Log("创建" + saveFile.file_name);
            Debug.Log(savepath);
        }
        else
        {
            Debug.Log("该名字存档已存在！请重新输入");
        }
        
    }

    public List<Card> CreatCardList()
    {
        Card card1 = new Card(0, "基础攻击", "对敌方单体造成", "攻击", 6, 9, 1, 0,10, CardType.Attack, SkillType.Attack);
        Card card2 = new Card(1, "基础攻击", "对敌方单体造成", "攻击", 6, 9, 1, 0,10, CardType.Attack, SkillType.Attack);
        Card card3 = new Card(2, "基础攻击", "对敌方单体造成", "攻击", 6, 9, 1, 0,10, CardType.Attack, SkillType.Attack);
        Card card4 = new Card(3, "防御", "获得", "攻击", 5, 8, 1, 0, 10,CardType.Defense, SkillType.Defense);
        Card card5 = new Card(4, "防御", "获得", "攻击", 5, 8, 1, 0, 10, CardType.Defense, SkillType.Defense);
        Card card6 = new Card(5, "防御", "获得", "攻击", 5, 8, 1, 0, 10, CardType.Defense, SkillType.Defense);
        Card card7 = new Card(6, "穿刺攻击", "对敌方全体造成", "攻击", 5, 11, 1, 0, 15,CardType.Skill, SkillType.Attack);
        Card card8 = new Card(7, "灵活防御", "获得", "攻击", 8, 11, 1, 1, 17, CardType.Skill, SkillType.Defense);
        Card card9 = new Card(8, "穿刺攻击", "对敌方全体造成", "攻击", 5, 11, 1, 0, 15, CardType.Skill, SkillType.Attack);
        List<Card> cards = new List<Card>();
        cards.Add(card1);
        cards.Add(card2);
        cards.Add(card3);
        cards.Add(card4);
        cards.Add(card5);
        cards.Add(card6);
        cards.Add(card7);
        cards.Add(card8);
        cards.Add(card9);
        return cards;
    }
    
    public void ShowLoadPanel()
    {
        Debug.Log(files.Count);
        Debug.Log(index);
        if (index < files.Count - 1 && index > 0)
        {
            lastbtn.SetActive(true);
            nextbtn.SetActive(true);
        }
        else if(index == 0)
        {
            lastbtn.SetActive(false);
        }
        else
        {
            nextbtn.SetActive(false);
        }
        loadfile.SetActive(true);
        file_name.text = files[index].file_name;
        savetime.text = files[index].savetime;
        savedetialtime.text = files[index].savedetailtime;
    }
    
    public void CloseLoadPanel()
    {
        loadfile.SetActive(false);
    }

    public void NextFile()
    {
        index++;
        ShowLoadPanel();
    }

    public void LastFile()
    {
        index--;
        ShowLoadPanel();
    }

    public void ChooseFile()
    {
        string okpath = Application.dataPath + "/Doc/" + files[index].file_name+"/playerdata.json";
        if (File.Exists(okpath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(okpath);//读取JSON文件
            string str = sr.ReadToEnd();
            Playerdata loaddata = JsonConvert.DeserializeObject<Playerdata>(str);//反序列化为List
            PlayerManger playerManger = new PlayerManger();
            playerManger.PlayerDataSet(loaddata);
            SceneManager.LoadScene(1);
            Debug.Log(str);
        }
        else
        {
            Debug.Log("Error");
        }
        
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    

}
