using Leadr;
using UnityEngine;

public class LeadrInitializer : MonoBehaviour
{
    [SerializeField] private LeadrSettings settings;

    private static bool _initialized;

    private void Awake()
    {
        if (_initialized)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        LeadrClient.Instance.Initialize(settings);
        _initialized = true;
    }
}