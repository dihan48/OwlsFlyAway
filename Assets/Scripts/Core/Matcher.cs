using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProvider))]

public class Matcher : MonoBehaviour
{
    [SerializeField] private int minMatchesCount = 5;

    private CharacterProvider characterProvider;

    public int DeleteMatched()
    {
        int count = 0;

        List<Character[,]> allDirectionsForMatching = new List<Character[,]>()
        {
            HorizontalLines(),
            VerticalLines(),
            UpDiagonalLines(),
            DownDiagonalLines()
        };

        List<IMatchable> lineMatched = new List<IMatchable>();

        foreach (var item in allDirectionsForMatching)
        {
            for (int n = 0; n < item.GetLength(0); n++)
            {
                lineMatched.Clear();

                for (int i = 0; i < item.GetLength(1); i++)
                {
                    IMatchable matchable = item[n, i];
                    if (lineMatched.Count < minMatchesCount)
                    {
                        if (matchable == null)
                        {
                            lineMatched.Clear();
                            continue;
                        }

                        if (lineMatched.Count == 0 || IsMatched(lineMatched[0], matchable) == false)
                        {
                            lineMatched.Clear();
                        }

                        lineMatched.Add(matchable);
                    }
                    else
                    {
                        if (item == null || IsMatched(lineMatched[0], matchable) == false)
                        {
                            break;
                        }

                        lineMatched.Add(matchable);
                    }
                }

                if (lineMatched.Count >= minMatchesCount)
                {
                    foreach (var matchable in lineMatched)
                    {
                        var destroyed = matchable as Character;

                        if (destroyed != null)
                        {
                            characterProvider.Destroy(destroyed);
                        }
                    }

                    count += lineMatched.Count;
                }
            }
        }

        return count;
    }

    private void Start()
    {
        characterProvider = GetComponent<CharacterProvider>();
    }

    private bool IsMatched(IMatchable matchable1, IMatchable matchable2)
    {
        if (matchable1 == null || matchable2 == null)
        {
            return false;
        }

        var match1 = matchable1.GetMatchables();
        var match2 = matchable2.GetMatchables();

        foreach (var item in match1)
        {
            if (match2.IndexOf(item) != -1)
            {
                return true;
            }
        }

        return false;
    }

    private Character[,] HorizontalLines()
    {
        var width = characterProvider.Width;
        var height = characterProvider.Height;
        Character[,] lines = new Character[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                lines[y, x] = characterProvider.Get(x, y);
            }
        }

        return lines;
    }

    private Character[,] VerticalLines()
    {
        var width = characterProvider.Width;
        var height = characterProvider.Height;
        Character[,] lines = new Character[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                lines[x, y] = characterProvider.Get(x, y);
            }
        }

        return lines;
    }

    private Character[,] UpDiagonalLines()
    {
        var width = characterProvider.Width;
        var height = characterProvider.Height;
        var lineSize = width < height ? width : height;
        Character[,] lines = new Character[width + height, lineSize];

        for (int n = 0; n < height; n++)
        {
            var y = height - 1 - n;

            for (int i = 0; i < lineSize; i++)
            {
                lines[n, i] = characterProvider.Get(i, y + i);
            }
        }

        for (int n = 0; n < width; n++)
        {
            for (int i = 0; i < lineSize; i++)
            {
                lines[height + n, i] = characterProvider.Get(n + i, i);
            }
        }

        return lines;
    }

    private Character[,] DownDiagonalLines()
    {
        var width = characterProvider.Width;
        var height = characterProvider.Height;
        var lineSize = width < height ? width : height;
        Character[,] lines = new Character[width + height, lineSize];

        for (int n = 0; n < height; n++)
        {
            var y = height - 1 - n;
            var x = width - 1;

            for (int i = 0; i < lineSize; i++)
            {
                lines[n, i] = characterProvider.Get(x - i, y + i);
            }
        }

        for (int n = 0; n < width; n++)
        {
            var x = width - 1 - n;

            for (int i = 0; i < lineSize; i++)
            {
                lines[height + n, i] = characterProvider.Get(x - i, i);
            }
        }

        return lines;
    }
}
