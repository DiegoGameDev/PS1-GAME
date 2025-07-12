using Player;

public interface IInteractive
{
    public virtual void Interact(PlayerController player)
    {

    }

    public void Looking(bool looking);
}