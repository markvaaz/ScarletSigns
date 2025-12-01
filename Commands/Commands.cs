using ScarletSigns.Service;
using VampireCommandFramework;

namespace ScarletSigns.Commands;

[CommandGroup("sign")]
public static class Commands {
  [Command("create", adminOnly: true)]
  public static void CreateSign(ChatCommandContext context, string text, string color = "white", float fontSize = 18f) {
    var player = context.User.GetPlayerData();

    if (player == null) {
      context.Reply("Player data not found.");
      return;
    }

    SignService.Create(text, player.Position, fontSize, color);

    context.Reply("Sign created successfully.");
  }

  [Command("rename", adminOnly: true)]
  public static void RenameSign(ChatCommandContext context, string newName, string color = "white", float fontSize = 18f) {
    var player = context.User.GetPlayerData();

    if (player == null) {
      context.Reply("Player data not found.");
      return;
    }

    SignService.Rename(player.Position, newName, 2f, fontSize, color);

    context.Reply("Sign renamed successfully.");
  }

  [Command("remove", adminOnly: true)]
  public static void Remove(ChatCommandContext context, float radius = 1f) {
    var player = context.User.GetPlayerData();

    if (player == null) {
      context.Reply("Player data not found.");
      return;
    }

    if (SignService.Remove(player.Position, radius)) {
      context.Reply("Sign removed successfully.");
    } else {
      context.Reply("No sign found nearby to remove.");
    }
  }
}