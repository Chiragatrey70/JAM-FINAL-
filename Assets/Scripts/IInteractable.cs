// An interface is a contract, not a full class with logic.
public interface IInteractable
{
    // Any script that uses this interface MUST have a string property called InteractionPrompt.
    public string InteractionPrompt { get; }

    // Any script that uses this interface MUST have a public method called Interact.
    public bool Interact(PlayerInteractor interactor);
}