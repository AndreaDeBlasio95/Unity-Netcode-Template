using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientGameManager
{
    private const string MennuSceneName = "Menu";

    public async Task<bool> InitAsync()
    {
        // Athenticate Unity services
        await UnityServices.InitializeAsync();

        // Authentica player
        AuthState authState = await AuthenticationWrapper.DoAuth();
        if (authState == AuthState.Authenticated)
        {
            return true;
        }

        return false;
    }

    public void GoToMenu ()
    {
        SceneManager.LoadScene(MennuSceneName);
    }
}