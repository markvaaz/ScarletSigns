using System;
using System.Collections.Generic;
using ProjectM;
using ScarletCore.Services;
using ScarletCore.Systems;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ScarletSigns.Service;

public static class SignService {
  public static readonly PrefabGUID HorsePrefab = new(1149585723);
  public static readonly PrefabGUID SignpostPrefab = new(314589946);
  public static readonly PrefabGUID InvisibilityBuff = new(1160901934);
  public static readonly PrefabGUID ImmaterialBuff = new(1360141727);
  public const string Style = "<size={0}><color={1}>{2}";
  public const string SignId = "ScarletSign";
  public static readonly List<Entity> Signs = [];

  public static void Initialize() {
    LoadSigns();
  }

  public static void LoadSigns() {
    Signs.Clear();
    var query = EntityLookupService.Query(EntityQueryOptions.IncludeDisabled, typeof(Mountable), typeof(UserMapZonePackedRevealElement));

    try {
      foreach (var entity in query) {
        string id = entity.GetId();

        if (id != SignId) {
          continue;
        }

        Signs.Add(entity);
      }
    } finally {
      query.Dispose();
    }
  }

  public static bool Create(string name, float3 position, float fontSize, string color = "white", bool showSignPost = false) {
    var text = FormatString(name, fontSize, color);

    Entity horse = SpawnerService.ImmediateSpawn(HorsePrefab, position);

    horse.SetId(SignId);

    if (horse.Has<SnapToHeight>()) {
      horse.Remove<SnapToHeight>();
    }

    horse.HasWith((ref Interactable interactable) => {
      interactable.Disabled = true;
    });

    horse.HasWith((ref NameableInteractable nameable) => {
      nameable.Name = new(text);
    });

    horse.AddWith((ref Immortal immortal) => {
      immortal.IsImmortal = true;
    });


    horse.With((ref DynamicCollision dynamicCollision) => {
      dynamicCollision.Immobile = true;
    });

    BuffService.TryApplyBuff(horse, InvisibilityBuff, -1f);
    BuffService.TryApplyBuff(horse, ImmaterialBuff, -1f);

    horse.Remove<SnapToHeight>();
    horse.Remove<DisableWhenNoPlayersInRange>();
    horse.Add<PreventDisableWhenNoPlayersInRange>();

    ActionScheduler.DelayedFrames(() => {
      TeleportService.TeleportToPosition(horse, position);
    }, 10);

    Signs.Add(horse);

    return true;
  }

  public static Entity Get(float3 position, float distance = 2f) {
    foreach (var horse in Signs) {
      if (math.distance(horse.Position(), position) < distance) {
        return horse;
      }
    }

    return Entity.Null;
  }

  public static Entity Rename(float3 position, string newName, float distance = 2f, float fontSize = 25f, string color = "white") {
    var horse = Get(position, distance);

    if (!horse.Exists()) {
      return Entity.Null;
    }

    var text = FormatString(newName, fontSize, color);

    horse.HasWith((ref NameableInteractable nameable) => {
      nameable.Name = new FixedString64Bytes(text);
    });

    return horse;
  }

  public static string FormatString(string name, float fontSize, string color) {
    return TruncateToByteLimit(Style.Replace("{0}", fontSize.ToString()).Replace("{1}", color).Replace("{2}", name).Replace("\\n", "\n"), 61);
  }

  public static bool Remove(float3 position, float distance = 2f) {
    var query = EntityLookupService.GetAllEntitiesInRadius(position, distance);

    foreach (var entity in query) {
      if (!entity.Exists() || !entity.IdEquals(SignId)) continue;

      var dist = math.distance(entity.Position(), position);

      if (dist > distance) continue;

      entity.Destroy();
      Signs.Remove(entity);

      return true;
    }

    return false;
  }

  private static string TruncateToByteLimit(string input, int maxBytes) {
    if (string.IsNullOrEmpty(input)) return input;

    var encoding = System.Text.Encoding.UTF8;

    if (encoding.GetByteCount(input) <= maxBytes) {
      return input;
    }

    string result = input;
    while (encoding.GetByteCount(result) > maxBytes && result.Length > 0) {
      result = result[..^1];
    }

    return result;
  }
}