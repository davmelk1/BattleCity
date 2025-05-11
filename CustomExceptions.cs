namespace MyGame;

class GameOverException : ApplicationException
{
    public GameOverException(string message) : base(message)
    {
    }
}
class WinException : ApplicationException
{
    public WinException(string message) : base(message)
    {
    }
}