using ScarletCore.Commanding;
using ScarletCore.Localization;
using ScarletSigns.Service;

namespace ScarletSigns.Commands;

[CommandGroup("sign", Language.English, adminOnly: true)]
public static class Commands {
  [Command("create", Language.English)]
  public static void CreateSign(CommandContext context, string text, string color = "white", float fontSize = 18f, bool showOnPvp = false) {
    var player = context.Sender;

    if (player == null) {
      context.Reply("Player data not found.".FormatError());
      return;
    }

    SignService.Create(text, player.Position, fontSize, color, showOnPvp);

    context.Reply("Sign created successfully.".FormatSuccess());
  }

  [Command("rename", Language.English)]
  public static void RenameSign(CommandContext context, string newName, string color = "white", float fontSize = 18f) {
    var player = context.Sender;

    if (player == null) {
      context.Reply("Player data not found.".FormatError());
      return;
    }

    SignService.Rename(player.Position, newName, 2f, fontSize, color);

    context.Reply("Sign renamed successfully.".FormatSuccess());
  }

  [Command("remove", Language.English)]
  public static void Remove(CommandContext context, float radius = 1f) {
    var player = context.Sender;

    if (player == null) {
      context.Reply("Player data not found.".FormatError());
      return;
    }

    if (!SignService.Remove(player.Position, radius)) {
      context.Reply("No sign found nearby to remove.".FormatError());
    }

    context.Reply("Sign removed successfully.".FormatSuccess());
  }
}