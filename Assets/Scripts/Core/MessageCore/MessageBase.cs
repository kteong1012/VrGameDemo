public abstract class MessageBase
{
    public GameMessageType messageId;
    public bool once = false;
    public bool mustBeHandled = false;
}

public enum GameMessageType
{
    GameOver = 0,
    AttackPlayer = 1,
    KillEnemy = 2,
}

public class GameOverMessage : MessageBase
{
    public GameOverMessage()
    {
        messageId = GameMessageType.GameOver;
    }
}
public class AttackPlayerMessage : MessageBase
{
    public float damage;
    public AttackPlayerMessage(float damage)
    {
        messageId = GameMessageType.AttackPlayer;
        this.damage = damage;
    }   
}
public class KillEnemyMessage : MessageBase
{
    public KillEnemyMessage()
    {
        messageId = GameMessageType.KillEnemy;
    }
}