using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistentObjectManager : MonoBehaviour {

    public int HighScore1;
    public int HighScore2;
    public int HighScore3;

    private const string hs1key = "hs1";
    private const string hs2key = "hs2";
    private const string hs3key = "hs3";


    public static PersistentObjectManager control;

    // Use this for initialization
    void Awake ()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            Load();
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
	}
	
	void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 1000, 300), "HS 1: " + HighScore1 + "  HS 2: " + HighScore2 + "  HS 3: " + HighScore3);
	}

    public void Save()
    {
        /* this is the "local file" version; works on every platform BUT web

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/info.dat");

        PlayerData data = new PlayerData();
        data.HighScore1 = HighScore1;
        data.HighScore2 = HighScore2;
        data.HighScore3 = HighScore3;

        bf.Serialize(fs, data);
        fs.Close();
        */
        PlayerPrefs.SetInt(hs1key, HighScore1);
        PlayerPrefs.SetInt(hs2key, HighScore2);
        PlayerPrefs.SetInt(hs3key, HighScore3);
        PlayerPrefs.Save();
    }


    public void Load()
    {
        /* this is the "local file" version; works on every platform BUT web
        if(File.Exists(Application.persistentDataPath + "/info.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);
            // PlayerData data = (PlayerData)bf.Deserialize(fs);
            PlayerData data = bf.Deserialize(fs) as PlayerData;
            fs.Close();

            HighScore1 = data.HighScore1;
            HighScore2 = data.HighScore2;
            HighScore3 = data.HighScore3;
        }
        */

        // PlayerPrefs version; works on web
        if(PlayerPrefs.HasKey(hs1key))
            HighScore1 = PlayerPrefs.GetInt(hs1key);
        if (PlayerPrefs.HasKey(hs2key))
            HighScore2 = PlayerPrefs.GetInt(hs2key);
        if (PlayerPrefs.HasKey(hs3key))
            HighScore3 = PlayerPrefs.GetInt(hs3key);

    }

}

/*
[Serializable]
class PlayerData
{
    public int HighScore1 = 0;
    public int HighScore2 = 0;
    public int HighScore3 = 0;
}
*/