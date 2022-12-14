using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

    [System.Serializable]
    public class SaveData
    {
        public List<string> testDataA = new List<string>();
        public List<int> testDataB = new List<int>();

        public int gold;
        public int damage;
        public int defence;
        public int critical;
        public int skillDamage;
        public float skillRange;
        public float colldown;
        public float speed;
        public float drop;
        public float attackSpeed;
        public float dodge;

    }

    public class DataManager : MonoBehaviour
    {
         string path;
         void Start()
         {
         path = Path.Combine(Application.persistentDataPath, "database.json");
         JsonLoad();
         }

        public void JsonLoad()
        {
            SaveData saveData = new SaveData();

            if (!File.Exists(path))
            {
            GameManager.instance.playerGold = 100;
            GameManager.instance.playerDamage = 4;
            JsonSave();
            }
            else
            {
                string loadJson = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(loadJson);
                if (saveData != null)
                {
                    for (int i = 0; i < saveData.testDataA.Count; i++)
                    {
                        GameManager.instance.testDataA.Add(saveData.testDataA[i]);
                    }
                    for (int i = 0; i < saveData.testDataB.Count; i++)
                    {
                        GameManager.instance.testDataB.Add(saveData.testDataB[i]);
                    }
                    GameManager.instance.playerGold = saveData.gold;
                    GameManager.instance.playerDamage = saveData.damage;
                }
            }
        }

        public void JsonSave()
        {
            SaveData saveData = new SaveData();

            for (int i = 0; i < 10; i++)
            {
                saveData.testDataA.Add("?????? ?????? No. " + i);
            }
            for (int i = 0; i < 10; i++)
            {
                saveData.testDataB.Add(i);
            }

            saveData.gold = GameManager.instance.playerGold;
            saveData.damage = GameManager.instance.playerDamage;

            string json = JsonUtility.ToJson(saveData, true);

            File.WriteAllText(path, json);
        }
    }
/*
???? ????????
string path = Path.Combine(Application.dataPath, "database.json");

//????????
string loadJson = File.ReadAllText(path);
saveData = JsonUtility.FromJson<SaveData>(loadJson);

//????????
string json = JsonUtility.ToJson(saveData);
File.WriteAllText(path, json);
*/

