using System.Collections.Generic;
using UnityEngine;

public class PreviewNextCharacters : MonoBehaviour
{
    [SerializeField] private CharacterProvider characterProvider;
    private readonly List<Character> characters = new List<Character>();

    public void Set(int[] nextCharacters)
    {
        foreach (var item in characters)
        {
            item.DestroyWithEffect(true, false);
        }

        characters.Clear();

        for (int i = 0; i < nextCharacters.Length; i++)
        {
            var position = transform.TransformPoint(new Vector3(i, 0, 0));
            var prefab = characterProvider.GetPrefab(nextCharacters[i]);

            if(prefab != null)
            {
                var character = Character.Instantiate(prefab, nextCharacters[i], transform, position);
                characters.Add(character);
            }
        }
    }
}