using System.Collections.Generic;

[System.Serializable]
public class GameState
{
    public List<ParcelState> parcels;

    public GameState()
    {
        parcels = new List<ParcelState>();
    }
}
