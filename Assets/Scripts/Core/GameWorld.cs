using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProvider))]
[RequireComponent(typeof(GameWorldInput))]
[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(Matcher))]
[RequireComponent(typeof(GameSave))]

public class GameWorld : MonoBehaviour
{
    [SerializeField] private PreviewNextCharacters previewNext;
    [SerializeField] private AudioClick selectionSound;
    [SerializeField] private AudioOwlSound audioOwlSound;
    [SerializeField] private Score score;

    private static Character selectedCharacter;

    private CharacterProvider characterProvider;
    private GameWorldInput gameWorldInput;
    private PathFinder pathFinder;
    private GameSave saveGame;
    private Matcher matcher;

    private int[] nextPrefabIndexs;

    public void Restart()
    {
        saveGame.Delete();
        score.Reset();
        characterProvider.Clear();
        pathFinder.DestroyPathMarkers();
        RandomSpawn();
        gameWorldInput.enabled = true;
    }

    public void SelectPosition(Vector2Int position)
    {
        if (characterProvider.IsExistsPosition(position) == false)
        {
            return;
        }

        var character = characterProvider.Get(position.x, position.y);

        if (character != null)
        {
            if (selectedCharacter != character)
            {
                if (selectedCharacter != null)
                {
                    selectedCharacter.OnMoved -= MovedCharacterHandler;
                    selectedCharacter.Unselect();
                }

                selectedCharacter = character;
                selectionSound.Play();
                character.Select();
                var pos = Vector2Int.RoundToInt(character.transform.localPosition);
                pathFinder.CreatePathMap(pos);
            }
        }
        else if (selectedCharacter != null && pathFinder.IsInPathMap(position))
        {
            selectionSound.Play();
            gameWorldInput.enabled = false;
            pathFinder.DestroyPathMarkers();
            var startPosition = Vector2Int.RoundToInt(selectedCharacter.transform.localPosition);
            var path = pathFinder.GetPath(startPosition, position);

            selectedCharacter.OnMoved += MovedCharacterHandler;
            selectedCharacter.StartMove(path);
        }
    }

    public void RandomSpawn()
    {
        var combinedCallback = new CombinedEventHandler<Character>(SpawnedCharactersHandler);

        foreach (var prefabindex in nextPrefabIndexs)
        {
            var empty = characterProvider.GetAllEmptyPosition();
            var index = UnityEngine.Random.Range(0, empty.Count);
            var pos = empty[index];
            var character = characterProvider.Create(pos.x, pos.y, prefabindex);

            if(character != null)
            {
                combinedCallback.BindCharacter(character);
                character.OnSpawned += combinedCallback.Callback;
            }
        }
    }

    public void SetNextCharacters(int[] prefabIndexs)
    {
        nextPrefabIndexs = prefabIndexs;
        previewNext.Set(prefabIndexs);
    }
    
    public int[] GetNextPrefabIndexs() => (int[])nextPrefabIndexs.Clone();

    private void Start()
    {
        gameWorldInput = GetComponent<GameWorldInput>();
        pathFinder = GetComponent<PathFinder>();
        matcher = GetComponent<Matcher>();
        characterProvider = GetComponent<CharacterProvider>();

        saveGame = new GameSave(this, characterProvider, score);
        saveGame.Load();
    }

    private void GenerateNextCharacters()
    {
        int emptyCount = characterProvider.GetAllEmptyPosition().Count;

        if (emptyCount == 0)
        {
            StartCoroutine(GameOver());
            return;
        }

        int countNextCharacters = emptyCount > 3
            ? 3
            : emptyCount == 1
                ? 1
                : emptyCount - 1;

        var prefabIndexs = new int[countNextCharacters];

        for (int i = 0; i < countNextCharacters; i++)
        {
            prefabIndexs[i] = characterProvider.GetRandomPrefabIndex();
        }

        SetNextCharacters(prefabIndexs);
    }

    private void MovedCharacterHandler(Character character, Vector2Int moveTo)
    {
        if (selectedCharacter == character)
        {
            selectedCharacter.OnMoved -= MovedCharacterHandler;
            selectedCharacter.Unselect();
            selectedCharacter = null;
        }

        characterProvider.Move(moveTo, character);

        int countDeleteMatched = matcher.DeleteMatched();

        if (countDeleteMatched == 0)
        {
            RandomSpawn();
        }
        else
        {
            score.Add(countDeleteMatched);
            audioOwlSound.Play();
            if (characterProvider.GetAllFillPosition().Count == 0)
            {
                RandomSpawn();
            }

            saveGame.Save();
            gameWorldInput.enabled = true;
        }
    }

    private void SpawnedCharactersHandler()
    {
        int countDeleteMatched = matcher.DeleteMatched();

        if (countDeleteMatched > 0)
        {
            audioOwlSound.Play();
            score.Add(countDeleteMatched);
        }

        GenerateNextCharacters();
        saveGame.Save();
        gameWorldInput.enabled = true;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        Restart();
    }
}

public class CombinedEventHandler<T>
{
    private Action _callback;
    private List<T> _expectedCharacters;

    public CombinedEventHandler(Action callback)
    {
        _expectedCharacters = new List<T>();
        _callback = callback;
    }

    public void BindCharacter(T character)
    {
        _expectedCharacters.Add(character);
    }

    public void Callback(T character)
    {
        _expectedCharacters.Remove(character);

        if (_expectedCharacters.Count == 0)
        {
            _callback?.Invoke();
        }
    }
}