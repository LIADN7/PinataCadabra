using UnityEngine;

/// <summary>
/// Abstract base class for game events.
/// </summary>
public abstract class GameEvent : MonoBehaviour
{
    // A unique identifier for the event (can be useful for logging or management)
    public string EventID { get; protected set; }

    // Indicates whether the event is currently running
    protected bool isRunning = false;

    /// <summary>
    /// Starts the event.
    /// </summary>
    public abstract void StartEvent();

    /// <summary>
    /// Stops the event.
    /// </summary>
    public abstract void StopEvent();

    /// <summary>
    /// Optional: Called every frame or at set intervals while the event is running.
    /// Subclasses can override this if they need to perform ongoing tasks.
    /// </summary>
    protected virtual void UpdateEvent()
    {
        // Default implementation does nothing.
        // Override in derived classes if needed.
    }

    /// <summary>
    /// Helper method to update the event state.
    /// This can be called within Update() if you want your event to process logic continuously.
    /// </summary>
    protected virtual void Update()
    {
        if (isRunning)
        {
            UpdateEvent();
        }
    }
}
