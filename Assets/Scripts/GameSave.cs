using System;
using System.IO;
using UnityEngine;

public class GameSave
{
    private readonly Score _score;
    private readonly GameWorld _gameWorld;
    private readonly CharacterProvider _characterProvider;
    private readonly string filePath;

    public GameSave(GameWorld gameWorld, CharacterProvider characterProvider, Score score)
    {
        filePath = Application.persistentDataPath + "/save.gamesave";

        _score = score;
        _gameWorld = gameWorld;
        _characterProvider = characterProvider;
    }

    public void Save()
    {
        var nextIndexPrefab = _gameWorld.GetNextPrefabIndexs();

        var allCharacters = _characterProvider.GetAllFillPosition();
        SaveCharacter[] allSavedCharacters = new SaveCharacter[allCharacters.Count];

        for (int i = 0; i < allCharacters.Count; i++)
        {
            var x = allCharacters[i].x;
            var y = allCharacters[i].y;
            var character = _characterProvider.Get(x, y);

            if (character != null)
            {
                allSavedCharacters[i] = new SaveCharacter() { prefabIndex = character.PrefabIndex, x = x, y = y };
            }
        }

        Save save = new Save() {
            score = _score.Value,
            allCharacters = allSavedCharacters,
            nextCharacters = nextIndexPrefab
        };

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(filePath, json);
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("Нет сохранения");
            _gameWorld.Restart();
            return;
        }

        Save save;

        try
        {
            string json = File.ReadAllText(filePath);
            save = JsonUtility.FromJson<Save>(json);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);

            Debug.LogWarning("сохранения не подходят");
            _gameWorld.Restart();
            return;
        }

        if (save.allCharacters == null || save.nextCharacters == null)
        {
            Debug.LogWarning("в файле сохраниения нет данных");
            _gameWorld.Restart();
            return;
        }

        _score.Reset();
        _characterProvider.Clear();

        _score.Add(save.score);

        SaveCharacter[] savedCharacters = save.allCharacters;

        foreach (var item in savedCharacters)
        {
            _characterProvider.Create(item.x, item.y, item.prefabIndex);
        }

        _gameWorld.SetNextCharacters(save.nextCharacters);

        if (_characterProvider.GetAllFillPosition().Count == 0)
        {
            _gameWorld.RandomSpawn();
        }
    }

    public void Delete()
    {
        File.Delete(filePath);
    }
}

[Serializable]
public struct Save
{
    public int score;
    public SaveCharacter[] allCharacters;
    public int[] nextCharacters;
}

[Serializable]
public struct SaveCharacter
{
    public int prefabIndex;
    public int x;
    public int y;
}