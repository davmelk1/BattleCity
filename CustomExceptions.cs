namespace MyGame;

public class GameOverException(string message) : ApplicationException(message);
public class WinException(string message) : ApplicationException(message);