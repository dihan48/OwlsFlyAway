using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    string filePath;
    public GameLogic gl;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();
        save.SavePlayers(GameLogic.objs);
        save.SaveNextPlayers(GameLogic.ColorPrefs);
        save.SaveScore(CountingQuantity.count);
        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            gl.RandColorPref();
            gl.RndAddObj();
            Debug.Log("Нет сохранения");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();
        gl.Clear();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                gl.LoadData(save.PlayersData[i, j].Sprite, i, j);
            }
        }
        gl.SetColorPref(save.ColorPrefs);
        gl.UpdateNewPlayersView();
        CountingQuantity.count = save.Score;
    }

    public void DeleteSave()
    {
        File.Delete(filePath);
    }

}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct PlayerSaveGame
    {
        public Vec3 Position;
        public string Sprite;

        public PlayerSaveGame(Vec3 pos, string sprt)
        {
            Position = pos;
            Sprite = sprt;
        }
    }

    public PlayerSaveGame[,] PlayersData = new PlayerSaveGame[9, 9];
    public void SavePlayers(object[,] objs)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (objs[i, j] != null)
                {
                    var go = (GameObject)objs[i, j];
                    string sprt = go.name;
                    Vec3 pos = new Vec3(go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);
                    PlayersData[i, j] = new PlayerSaveGame(pos, sprt);
                }
                else
                {
                    PlayersData[i, j] = new PlayerSaveGame(new Vec3(0, 0, 0), null);
                }

            }
        }
    }

    public List<string> ColorPrefs = new List<string>();
    public void SaveNextPlayers(List<string> cp)
    {
        foreach (var item in cp)
        {
            ColorPrefs.Add(item);
        }
    }
    public int Score;
    public void SaveScore(int score)
    {
        Score = score;
    }
}