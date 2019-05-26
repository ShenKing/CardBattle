using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public string file_name;
    public string savetime;
    public string savedetailtime;

    public SaveFile(string name,string time,string detime)
    {
        this.file_name = name;
        this.savetime = time;
        this.savedetailtime = detime;
    }
}
