using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton Instance;

    void Awake()
    {
        Instance = this;
    }
}
