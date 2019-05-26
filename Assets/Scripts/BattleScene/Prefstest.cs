using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//实现卡牌的保存和读取
public class Prefstest
{
    public InputField inputf;
    public Text leveltext;
    private Dictionary<int, Card> dics;
    private List<Card> licards;
    public ScrollRect rect;
    public Transform newcard;

    public void Savemesage()
    {
        PlayerPrefs.SetString("level", inputf.text);
        Debug.Log(inputf.text);
    }

    public void Outputmesage()
    {
        string mes = PlayerPrefs.GetString("level");
        Debug.Log(mes);
        leveltext.text = mes;
    }

    //序列化卡牌保存到JSON文件
    public void DicSavetest()
    {
        int index = 0;
        //Dictionary<int, Card> dic = new Dictionary<int, Card>();
        List<Card> lic = new List<Card>();
        for (int i = 0; i < 7; i++)
        {
            string xxx = "old wang" + index.ToString();
            Card s = new Card(index,xxx,"强","攻击",10,20,1,0,20,CardType.Attack,SkillType.Attack);
            lic.Add(s);//添加卡牌到List
           // lic.Add(s.card_id, s);
            index++;
        }
        string ddd = JsonConvert.SerializeObject(lic, Formatting.Indented);//序列化卡牌为JSON格式
        Debug.Log(ddd);
        string filepath = Application.dataPath + "/Doc" + "/cardlist_all.json";
        //string farmatc = JsonMapper.ToJson(save);
        StreamWriter sw = new StreamWriter(filepath);//保存到指定路径文件
        sw.Write(ddd);
        sw.Close();
        if (File.Exists(filepath))
        {

            Debug.Log("Save Successd");
        }
    }


    public void DicSaveMonster()
    {
        int index = 0;
        //Dictionary<int, Card> dic = new Dictionary<int, Card>();
        List<Monster> lic = new List<Monster>();
        for (int i = 0; i < 7; i++)
        {
            string xxx = "old wang" + index.ToString();
            Monster m = new Monster(index, xxx, "monster_tree",35, 0, 10, 1);
            lic.Add(m);
            //Card s = new Card(index, xxx, "强", "土婊", 10,1,CardType.Equip);
            //lic.Add(s);//添加卡牌到List
            //dic.Add(s.card_id, s);
            index++;
        }
        string ddd = JsonConvert.SerializeObject(lic, Formatting.Indented);//序列化卡牌为JSON格式
        Debug.Log(ddd);
        string filepath = Application.dataPath + "/Doc" + "/monster_all.json";
        //string farmatc = JsonMapper.ToJson(save);
        StreamWriter sw = new StreamWriter(filepath);//保存到指定路径文件
        sw.Write(ddd);
        sw.Close();
        if (File.Exists(filepath))
        {

            Debug.Log("Save Successd");
        }
    }

    //读取JSON文件内容反序列化为List
    /*public void DicLoadTest()
    {
        string filepath = Application.dataPath + "/Doc" + "/cardlist_all.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            //Save save = JsonMapper.ToObject<Save>(str);
            //dics = JsonConvert.DeserializeObject<Dictionary<int,Card>>(str);//反序列化为Dictionary
            licards = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List

        }
        foreach(var pair in licards)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            Settest setcard = go.GetComponent<Settest>();
            setcard.Textset(pair);
        }
    }*/

    private List<int> RandomSort(List<int> list)
    {
        var random = new System.Random();
        var newList = new List<int>();
        foreach (var item in list)
        {
            newList.Insert(random.Next(newList.Count), item);
        }
        return newList;
    }

    public void LoadCard(int n)
    {
        for(int i = 0; i < n; i++)
        {
            
        }
    }

    public void SaveJsontest()
    {
        //string xxx = "old wang";
        List<Card> lss = new List<Card>();
        for (int i = 0; i < 10; i++) {
            //Card s = new Card(xxx,23,"男");
            //lss.Add(s);
        }
        //Save save = CreatSave();
        string fff = JsonConvert.SerializeObject(lss, Formatting.Indented);
        string filepath = Application.dataPath + "/Doc" + "/monsterlist_all.json";
        //string farmatc = JsonMapper.ToJson(save);
        StreamWriter sw =new StreamWriter(filepath);
        sw.Write(fff);
        sw.Close();
        if (File.Exists(filepath))
        {
            
            Debug.Log("Save Successd");
        }
    }

    public void Objtest()
    {
        string filepath = Application.dataPath + "/Doc" + "/file.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);
            string str = sr.ReadToEnd();
            sr.Close();
            var ja= JArray.Parse(str);
            var student = new JObject { { "name","mark"},
                                                              { "Age",8},
                                                            { "xxx","aaaa"} };
            ja.Add(student);
            string sss = ja.ToString();
            StreamWriter sw = new StreamWriter(filepath);
            sw.Write(sss);
            sw.Close();
            Debug.Log(sss);
        }
    }

    public void LoadJsontest()
    {
        string filepath = Application.dataPath+ "/Doc" + "/file.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);
            string str = sr.ReadToEnd();
            //Save save = JsonMapper.ToObject<Save>(str);
            Debug.Log(str);
        }
    }

    public void SaveFile()
    {
        List<SaveFile> files = new List<SaveFile>();
        SaveFile saveFile = new SaveFile("Test1", "2019年5月23日", "13:10");
        files.Add(saveFile);
        string ddd = JsonConvert.SerializeObject(files, Formatting.Indented);
        Debug.Log(ddd);
        string filepath = Application.dataPath + "/Doc" + "/flielist.json";
        StreamWriter sw = new StreamWriter(filepath);
        sw.Write(ddd);
        sw.Close();
        if (File.Exists(filepath))
        {

            Debug.Log("Save Successd");
        }
    }


}
