using UnityEngine;

/// <summary>
/// Simple singleton to use with different components.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance { get => _instance; set => _instance = value; }

    public virtual void Awake()
    {
        Instance = this as T;
    }
}