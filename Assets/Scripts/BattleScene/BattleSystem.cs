using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BattleSystem : MonoBehaviour
{
    public Image bg_stage;
    public Text hp;
    public Image bg_hp;
    public Text enegy;
    public Image bg_enegy;
    public Text money;
    public Text curepotion;
    public Text armorpotion;
    public Text damagepotion;
    public Image bg_armor;
    public Text text_armor;
    private List<Card> playercards;
    private List<Card> licards;
    private List<Monster> monsterlist;
    private List<Card> usedcard;
    private List<Card> rewordcard;
    private GameObject[] allcard;
    public Text flyText;
    private GameObject[] allmonster;
    public ScrollRect rect;
    public ScrollRect monsterrect;
    public ScrollRect cardchoose;
    public Transform newcard;
    public Transform reword;
    public GameObject DeathPanel;
    public GameObject WinPanel;
    public Text getmoney;
    private int count = 0;

    public int playerhp;
    public int playerenegy;
    public  int playerarmor;
    private int playerenegy_max;
    private int playermoney;
    private int curepotioncount;
    private int armorpotioncount;
    private int damagepotioncount;
    public int deathmonster = 0;
    private string playername;
    RandomCard RandomCard;

    private void Awake()
    {
        //战斗场景初始设置
        playerhp = PlayerPrefs.GetInt("hp");
        playerenegy =3;
        playerenegy_max = 3;
        playerarmor = PlayerPrefs.GetInt("armor");
        playermoney = PlayerPrefs.GetInt("money");
        curepotioncount = PlayerPrefs.GetInt("curepotion");
        armorpotioncount = PlayerPrefs.GetInt("armorpotion");
        damagepotioncount = PlayerPrefs.GetInt("damagepotion");
        RandomCard = new RandomCard();

        string bg = PlayerPrefs.GetString("bg_stage");
        bg_stage.sprite = Resources.Load <Sprite>("Textures/" + bg);
        hp.text = playerhp.ToString();
        bg_hp.fillAmount = playerhp / 100f;
        enegy.text = playerenegy.ToString();
        bg_enegy.fillAmount = playerenegy / playerenegy_max;
        money.text = playermoney.ToString();
        curepotion.text = curepotioncount.ToString();
        armorpotion.text = armorpotioncount.ToString();
        damagepotion.text = damagepotioncount.ToString();
        playername = PlayerPrefs.GetString("playername");
        GameObject.Find("Panel").GetComponent<PanelListener>().enegy = playerenegy;
        usedcard = new List<Card>();
        DicLoadTest();
        LoadMonster();
        bg_armor.gameObject.SetActive(false);
        text_armor.gameObject.SetActive(false);
        DeathPanel.SetActive(false);
    }
    //读取JSON文件内容反序列化为List
    private void DicLoadTest()
    {
        string filepath = Application.dataPath + "/Doc/" + playername +"/cardlist.json";
        Debug.Log(filepath);
        if (File.Exists(filepath))
        {
            Debug.Log("card load");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            playercards = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List
            licards = new List<Card>(playercards);
            licards = RandomCard.RandomCards(licards);
        }
        else
        {
            Debug.Log("文件不存在");
        }
        DrawCard(5);

        /*for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            go.transform.Find("ui").gameObject.name = (++count).ToString();
            Settest setcard = go.GetComponent<Settest>();
            CardListener cardlistener = go.GetComponentInChildren<CardListener>();
            cardlistener.GetCardData(licards[i]);
            setcard.Textset(licards[i]);
        }*/
        /*foreach (var pair in licards)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            go.transform.Find("ui").gameObject.name =  (++count).ToString(); 
            Settest setcard = go.GetComponent<Settest>();
            CardListener cardlistener = go.GetComponentInChildren<CardListener>();
            cardlistener.GetCardData(pair);
            setcard.Textset(pair);
        }*/
    }
    //加载怪物
    private void LoadMonster()
    {
        string filepath = Application.dataPath + "/Doc" + "/monster_all.json";
        if (File.Exists(filepath))
        {
            Debug.Log("monster load");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            monsterlist = JsonConvert.DeserializeObject<List<Monster>>(str);//反序列化为List
        }
        for(int i = 0; i < 2; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + monsterlist[i].monster_prefab_name);
            GameObject go = Instantiate(prefab, monsterrect.content);
            MonsterState monsterlistener = go.GetComponentInChildren<MonsterState>();
            monsterlistener.GetMonsterData(monsterlist[i]);
        }
    }
    //场景跳转
    public void SceneChange()
    {
        PlayerPrefs.SetInt("hp", playerhp);
        PlayerPrefs.SetInt("money", playermoney);
        SaveTool saveTool = new SaveTool();
        saveTool.SaveCardList(playercards);
        saveTool.SavePlayerData();
        Debug.Log("保存成功！");
        SceneManager.LoadScene(1);

    }
    public void PlayerDeath()
    {
        Playerdata newplayerdata = new Playerdata(playername, 100, 0, 0, 0, 0, 0, 0);
        MainMenuManger mainMenu = new MainMenuManger();
        SaveTool saveTool = new SaveTool();
        PlayerManger playerManger = new PlayerManger();
        playercards = mainMenu.CreatCardList();
        playerManger.PlayerDataSet(newplayerdata);
        saveTool.SaveCardList(playercards);
        saveTool.SavePlayerData();
        SceneManager.LoadScene(0);
    }


    //结束回合
    public void EndTurn()
    {
        allmonster = GameObject.FindGameObjectsWithTag("monster");
        StartCoroutine(enumerator());
        allcard = GameObject.FindGameObjectsWithTag("card");
        for(int i = 0; i < allcard.Length; i++)
        {
            EndTurnUseCard(allcard[i].GetComponentInChildren<CardListener>().ChangeCard());
            allcard[i].SetActive(false);
        }
        playerenegy = 3;
        GameObject.Find("Panel").GetComponent<PanelListener>().enegy = playerenegy;
        if (playerhp == 0) DeathPanel.SetActive(true);
    }

    //回合结束消除卡片
    public void EndTurnUseCard(Card card)
    {
        usedcard.Add(card);
        licards.Remove(card);
    }

    //玩家扣血
    public void PlayerDamageManager(Monster monster )
    {
        playerhp -= monster.monster_damage;
    }
    //怪物扣血传入参数（卡片伤害，怪物血量）
    public int MonsterDamageManager(int carddamage,int monsterhp)
    {
        monsterhp -= carddamage;
        return monsterhp;
    }
    //刷新UI
    private void Update()
    {
        hp.text = playerhp.ToString();
        bg_hp.fillAmount = playerhp / 100f;
        enegy.text = playerenegy.ToString();
        bg_enegy.fillAmount = playerenegy / 3f;
        money.text = playermoney.ToString();
        curepotion.text = curepotioncount.ToString();
        armorpotion.text = armorpotioncount.ToString();
        damagepotion.text = damagepotioncount.ToString();
        if (playerarmor>0)
        {
            bg_armor.gameObject.SetActive(true);
            text_armor.gameObject.SetActive(true);
            text_armor.text = playerarmor.ToString();
        }
        else
        {
            bg_armor.gameObject.SetActive(false);
            text_armor.gameObject.SetActive(false);
        }
        
    }
    //玩家被攻击
    public void BeAttack(int damage)
    {
        if (playerarmor < damage)
        {
            playerhp += playerarmor;
            playerhp -= damage;
            flyText.text = "-" + (damage - playerarmor);
            playerarmor = 0;
            
        }
        else
        {
            playerarmor -= damage;
            text_armor.text = playerarmor.ToString();
            flyText.text = "-0";
        }
        
        FlyTo(flyText);
        if (playerhp <= 0)
        {
            playerhp = 0;
            //结束画面
            DeathPanel.SetActive(true);
        }
    }

    IEnumerator enumerator()
    {
        for (int i = 0; i < allmonster.Length; i++)
        {
            allmonster[i].SendMessage("Attack");
            yield return new WaitForSeconds(1.0f);
        }
        DrawCard(5); 
    }
    //抽卡
    private void DrawCard(int num)
    {
        if (licards.Count < num)
        {
            for(int j = 0; j < usedcard.Count; j++)
            {
                licards.Add(usedcard[j]);
                licards = RandomCard.RandomCards(licards);
                usedcard.Remove(usedcard[j]);
            }
        }
        for(int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            go.transform.Find("ui").gameObject.name = (++count).ToString();
            Settest setcard = go.GetComponent<Settest>();
            CardListener cardlistener = go.GetComponentInChildren<CardListener>();
            cardlistener.GetCardData(licards[i]);
            setcard.Textset(licards[i]);
        }
    }
    //使用卡片后对卡片进行处理
    public int UseCard(Card usecard)
    {
        if (usecard.card_num != 0) DrawCard(usecard.card_num);
        playerenegy -= usecard.card_cost;
        usedcard.Add(usecard);
        licards.Remove(usecard);
        return playerenegy;
    }
    //扣血特效
    public void FlyTo(Graphic graphic)
    {
        RectTransform rt = graphic.rectTransform;
        Color c = graphic.color;
        c.a = 0;
        graphic.color = c;                                                   //先将字体透明  
        Sequence mySequence = DOTween.Sequence();                            //创建空序列  
        float tey = rt.localPosition.y;
        Tweener move1 = rt.DOLocalMoveY(rt.localPosition.y+20, 0.1f);            //创建向上移动的第一个动画  
        Tweener move2 = rt.DOLocalMoveY(rt.localPosition.y+40, 0.1f);           //创建向上移动的第二个动画  
        Tweener move3 = rt.DOLocalMoveY(tey, 0f);
        Tweener alpha1 = graphic.DOColor(new Color(c.r, c.g, c.b, 1), 0.1f);//创建Alpha由0到1渐变的动画  
        Tweener alpha2 = graphic.DOColor(new Color(c.r, c.g, c.b, 0), 0.1f);//创建Alpha由1到0渐变的动画  
        mySequence.Append(move1);                  //先添加向上移动的动画  
        mySequence.Join(alpha1);                   //同时执行Alpha由0到1渐变的动画  
        mySequence.AppendInterval(0.2f);              //延迟1秒钟  
        mySequence.Append(move2);                  //添加向上移动的动画  
        mySequence.Join(alpha2);                   //同时执行Alpha由1到0渐变的动画 
        mySequence.Append(move3);
    }

    //赢得游戏后显示画面
    public void WinGame()
    {
        WinPanel.SetActive(true);
        string filepath = Application.dataPath + "/Doc" + "/cardlist_all.json";
        if (File.Exists(filepath))
        {
            Debug.Log("card load");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            rewordcard = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(reword.gameObject, cardchoose.content);
            go.transform.Find("ui").gameObject.name = (++count).ToString();
            Settest setcard = go.GetComponent<Settest>();
            setcard.Textset(rewordcard[i]);
        }
        System.Random random = new System.Random();
        int gold = random.Next(13, 37);
        getmoney.text = "+" + gold.ToString();
        playermoney += gold;
    }

    //结算选卡
    public void GetNewCard(Card card)
    {
        playercards.Add(card);

    }

    //怪物死亡
    public void MonsterDeath()
    {
        deathmonster++;
        if (deathmonster == 2)
        {
            WinGame();
            Debug.Log("11111");
        }
    }

}
