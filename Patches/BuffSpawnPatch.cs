using HarmonyLib;
using ProjectM;
using ProjectM.Gameplay.Systems;
using ScarletCore.Services;
using ScarletSigns.Service;
using Stunlock.Core;
using Unity.Collections;

namespace ScarletSigns.Patches;

[HarmonyPatch]
public static class BuffSpawnPatch {
  public static readonly PrefabGUID DownedHorseBuff = new(-266455478);
  [HarmonyPatch(typeof(DownedEventSystem), nameof(DownedEventSystem.OnUpdate))]
  [HarmonyPrefix]
  public static void Prefix(ref DownedEventSystem __instance) {
    var query = __instance._DownedEventQuery.ToEntityArray(Allocator.Temp);

    foreach (var entity in query) {
      var downed = entity.Read<DownedEvent>().Entity;

      if (!downed.Has<Mountable>() || !downed.HasId() || !downed.IdEquals(SignService.SignId)) continue;

      BuffService.TryRemoveBuff(downed, DownedHorseBuff);

      downed.HasWith((ref Health health) => {
        health.Value = health.MaxHealth;
        health.MaxRecoveryHealth = health.MaxHealth;
      });
    }
  }
}