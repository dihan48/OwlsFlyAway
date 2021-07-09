using System.Collections.Generic;
public enum MatchType
{
    Blue,
    Cyan,
    Green,
    Orange,
    Purple,
    Red,
    yellow
}

public interface IMatchable
{
    public List<MatchType> GetMatchables();
}
