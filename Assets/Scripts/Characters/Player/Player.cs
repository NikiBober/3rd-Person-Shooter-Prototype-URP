/// <summary>
/// For static reference to player.
/// </summary>
public class Player : Singleton<Player>
{
    private bool _isAlive = true;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
}
