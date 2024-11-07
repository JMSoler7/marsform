using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState currentGameState;
    public SaveManager saveManager;
    public GameObject[] allParcels;

    void Start()
    {
        currentGameState = saveManager.LoadGame();
        if (currentGameState != null)
        {
            foreach (ParcelState parcel in currentGameState.parcels)
            {
                parcel.LoadParcelState(currentGameState);
            }
        }
    }
}
