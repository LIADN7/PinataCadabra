using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Manages background events using a dictionary that maps a BgNumEvent to a list of GameEvent instances.
/// </summary>
public class Background : MonoBehaviour
{

    public enum BgNumEvent
    {
        Mainsplash,
        Level1,
        Menu,
        Endgame
    }

    // Dictionary to store events by category
    [SerializeField]
    private Dictionary<BgNumEvent, List<GameEvent>> eventsDictionary = new Dictionary<BgNumEvent, List<GameEvent>>();

    // The currently active background event category
    [SerializeField]
    private BgNumEvent currentBgEvent;

    /// <summary>
    /// Activates all events associated with the specified BgNumEvent.
    /// Stops any currently running events before starting the new set.
    /// </summary>
    /// <param name="bgEvent">The background event category to activate.</param>
    public void ActivateEvents(BgNumEvent bgEvent)
    {
        // Stop all events before switching to new events
        StopAllEvents();
        currentBgEvent = bgEvent;
        if (eventsDictionary.TryGetValue(bgEvent, out List<GameEvent> eventList))
        {
            foreach (GameEvent gameEvent in eventList)
            {
                gameEvent.StartEvent();
            }
        }
        else
        {
            Debug.LogWarning($"No events found for {bgEvent} in eventsDictionary.");
        }
    }

    /// <summary>
    /// Stops all events in every category.
    /// </summary>
    public void StopAllEvents()
    {
        foreach (KeyValuePair<BgNumEvent, List<GameEvent>> kvp in eventsDictionary)
        {
            foreach (GameEvent gameEvent in kvp.Value)
            {
                gameEvent.StopEvent();
            }
        }
    }

}
