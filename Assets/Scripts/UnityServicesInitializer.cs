using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityServicesInitializer : MonoBehaviour
{
    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        switch (UnityServices.State)
        {
            case ServicesInitializationState.Uninitialized:
                break;
            case ServicesInitializationState.Initializing:
                break;
            case ServicesInitializationState.Initialized:
                if (await SignAnonymously())
                {
                    SceneManager.LoadSceneAsync(2);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static async Task<bool> SignAnonymously()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        return AuthenticationService.Instance.IsSignedIn;
    }
}
