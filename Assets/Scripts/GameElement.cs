public class GameElement
{
    public int x { get; set; }
    public int y { get; set; }

    public static bool operator !=(GameElement left, GameElement right)
    {
        return
               left.x == right.x &&
               left.y == right.y;
    }
    public static bool operator ==(GameElement left, GameElement right)
    {
        if (left != null || right != null)
        {
            if (left.x == right.x && left.y == right.y)
                return true;
            else
                return false;
        }
        else
        {
            if (left == null && right == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override bool Equals(object obj)
    {
        var element = obj as GameElement;
        return x == element.x &&
               y == element.y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }

    public GameElement(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


}