using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameInputContext
{
    public static GameInputContext Instance = new GameInputContext();

    public readonly List<UIEvent> Events = new List<UIEvent>();

    public bool HitUI;
    public void Clear()
    {
        Events?.Clear();
    }

    public void AddEvent(UIEvent uiEvent)
    {
        Events.Add(uiEvent);
    }
}

