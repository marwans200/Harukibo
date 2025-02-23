using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class ClientManager : MonoBehaviour
{
    public NetworkRunner _runner;

    private async void Start()
    {

        // Ensure the Scene Manager is added
        if (_runner.GetComponent<NetworkSceneManagerDefault>() == null)
        {
            _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = "Harukibo", // Must match the server session name
            SceneManager = _runner.GetComponent<NetworkSceneManagerDefault>(),
            Address = NetAddress.Any()
        });

        if (result.Ok)
        {
            Debug.Log("✅ Client connected successfully!");
        }
        else
        {
            Debug.LogError($"❌ Failed to connect: {result.ShutdownReason}");
        }
    }
}
