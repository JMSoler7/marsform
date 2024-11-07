[System.Serializable]
public class ParcelState
{
    public int x;
    public int y;
    public ParcelCondition state;

    public ParcelState() { }
    public ParcelState(int x, int y, ParcelCondition state)
    {
        this.x = x;
        this.y = y;
        this.state = state;
    }

    public void LoadParcelState(GameState gameState)
    {
        foreach (ParcelState parcel in gameState.parcels)
        {
            if (parcel.x == x && parcel.y == y)
            {
                this.state = parcel.state;
            }
        }
    }
}
