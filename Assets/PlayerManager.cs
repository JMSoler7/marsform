using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int food;
    public static bool isMenuOpen = false;

    public TextMeshProUGUI foodCounterText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        food = 0;
        UpdateFoodCounter();
    }

    public void AddFood(int amount)
    {
        food += amount;
        UpdateFoodCounter();
    }

    public void RemoveFood(int amount)
    {
        food -= amount;
        if (food < 0) food = 0;
        UpdateFoodCounter();
    }

    private void UpdateFoodCounter()
    {
        if (foodCounterText != null)
        {
            foodCounterText.text = "Food: " + food.ToString();
        }
    }
}
