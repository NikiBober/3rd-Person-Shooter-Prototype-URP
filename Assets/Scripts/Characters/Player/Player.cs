// For static reference to player.
public class Player : Singleton<Player>
{
    private bool _isPlayerAlive = true;

    public bool IsPlayerAlive { get => _isPlayerAlive; set => _isPlayerAlive = value; }
}
