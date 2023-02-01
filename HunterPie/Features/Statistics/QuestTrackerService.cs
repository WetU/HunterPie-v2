﻿using HunterPie.Core.Game;
using HunterPie.Core.Game.Events;
using HunterPie.Domain.Interfaces;
using HunterPie.Features.Account;
using HunterPie.Features.Account.Config;
using HunterPie.Features.Statistics.Models;
using HunterPie.Integrations.Poogie.Statistics;
using HunterPie.Integrations.Poogie.Statistics.Models;
using System;
using System.Threading.Tasks;

namespace HunterPie.Features.Statistics;

#nullable enable
internal class QuestTrackerService : IContextInitializer, IDisposable
{
    private IContext? _context;
    private readonly PoogieStatisticsConnector _connector = new();

    private HuntStatisticsService? _statisticsService;

    private void HookEvents()
    {
        _context!.Game.OnQuestStart += OnQuestStart;
        _context.Game.OnQuestEnd += OnQuestEnd;
    }

    private void UnhookEvents()
    {
        _context!.Game.OnQuestStart -= OnQuestStart;
        _context.Game.OnQuestEnd -= OnQuestEnd;
    }

    private async void OnQuestEnd(object? sender, QuestStateChangeEventArgs e)
    {
        if (!await AccountManager.IsLoggedIn() || !LocalAccountConfig.Config.IsHuntUploadEnabled)
            return;

        HuntStatisticsModel? exported = _statisticsService?.Export();

        _statisticsService?.Dispose();

        if (exported is null)
            return;

        if (!ShouldUpload(exported))
            return;

        exported = exported with { FinishedAt = exported.StartedAt.Add(e.QuestTime) };

        var exportedRequest = PoogieQuestStatisticsModel.From(exported);

        await _connector.Upload(exportedRequest)
            .ConfigureAwait(false);
    }

    private void OnQuestStart(object? sender, QuestStateChangeEventArgs e)
    {
        _statisticsService = new HuntStatisticsService(_context);
    }

    public void Dispose()
    {
        UnhookEvents();
        _statisticsService?.Dispose();
    }

    private static bool ShouldUpload(HuntStatisticsModel model)
    {
        return model.Monsters.Count > 0;
    }

    public Task InitializeAsync(IContext context)
    {
        _context = context;
        HookEvents();

        return Task.CompletedTask;
    }
}
