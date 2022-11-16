using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using System;

public class BotDifficultyManager : MonoBehaviour
{
    [SerializeField] Bot bot;
    [SerializeField] int selectedDifficulty;
    [SerializeField] BotStats[] botDifficulties;

    [Header("Difficulty Settings : ")]
    [SerializeField] bool enableRemoteCongifg = false;
    [SerializeField] string difficultyKey = "Difficulty";

    struct userAttribute { };
    struct appAttribute { };
    IEnumerator Start()
    {
        yield return new WaitUntil(() => bot.IsReady);

        var newStats = botDifficulties[selectedDifficulty];
        bot.SetStats(newStats, true);

        if (enableRemoteCongifg == false)
            yield break;

        yield return new WaitUntil(
            () =>
            UnityServices.State == ServicesInitializationState.Initialized
            &&
            AuthenticationService.Instance.IsSignedIn
            );

        RemoteConfigService.Instance.FetchCompleted += OnRemoteConfigFetched;

        RemoteConfigService.Instance.FetchConfigsAsync(
            new userAttribute(),
            new appAttribute()
            );
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= OnRemoteConfigFetched;
    }

    private void OnRemoteConfigFetched(ConfigResponse response)
    {
        if (RemoteConfigService.Instance.appConfig.HasKey(difficultyKey) == false)
            return;

        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
            case ConfigOrigin.Cached:
                break;
            case ConfigOrigin.Remote:
                selectedDifficulty = RemoteConfigService.Instance.appConfig.GetInt(difficultyKey);
                selectedDifficulty = Mathf.Clamp(selectedDifficulty, 0, botDifficulties.Length - 1);
                var newStats = botDifficulties[selectedDifficulty];
                bot.SetStats(newStats, true);
                break;
        }

    }
}
