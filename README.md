# ScarletSigns

ScarletSigns adds a customizable sign system to V Rising servers. Admins can create floating text signs with custom colors, sizes, and multiple lines to label areas, provide information, or enhance the server's visual experience. Signs can be renamed and removed as needed. All features are accessible through chat commands.

---

## Support & Donations

<a href="https://www.patreon.com/bePatron?u=30093731" data-patreon-widget-type="become-patron-button"><img height='36' style='border:0px;height:36px;' src='https://i.imgur.com/o12xEqi.png' alt='Become a Patron' /></a>  <a href='https://ko-fi.com/F2F21EWEM7' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' alt='Buy Me a Coffee at ko-fi.com' /></a>

---

## Features

- **Floating Text Signs:**  
  Create floating text labels at any location with customizable font size and color.

- **Multiple Lines:**  
  Use `\n` in your text to create multi-line signs for better organization.

- **Customization:**  
  Control font size and text color to match your server's aesthetic.

- **Sign Management:**  
  Rename and remove signs easily with simple commands.

- **Persistent Signs:**  
  All signs are automatically saved and restored on server restart.

---

## Usage

For a full list of commands and usage, expand the **Show Commands**.

<details>
<summary>Show Commands</summary>

## Admin Commands

- `.sign create`
  - **Usage:** `.sign create <text> [color] [fontSize]`
  - **Description:** Create a floating text sign at your current position.
  - **Parameters:**
    - `text`: The text to display (use `\n` for multiple lines)
    - `color`: Optional color name or hex code (default: "white")
    - `fontSize`: Optional font size (default: 18)
  - **Example:** `.sign create "Welcome\nto my castle" red 30`

- `.sign rename`
  - **Usage:** `.sign rename <newName> [color] [fontSize]`
  - **Description:** Rename the nearest sign (within 2 units) to the specified text.
  - **Parameters:**
    - `newName`: The new text to display (use `\n` for multiple lines)
    - `color`: Optional color name or hex code (default: "white")
    - `fontSize`: Optional font size (default: 18)
  - **Example:** `.sign rename "Now Closed" gray 25`

- `.sign remove`
  - **Usage:** `.sign remove [radius]`
  - **Description:** Remove the nearest sign within the specified radius.
  - **Parameters:**
    - `radius`: Optional search radius in units (default: 1)
  - **Example:** `.sign remove 3`

</details>

---

## Installation

### Requirements

This mod requires the following dependencies to function correctly:

* **[BepInEx (RC2)](https://wiki.vrisingmods.com/user/bepinex_install.html)**
* **[ScarletCore](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletCore/)**
* **[VampireCommandFramework](https://thunderstore.io/c/v-rising/p/deca/VampireCommandFramework/)**

Make sure both are installed and loaded **before** installing ScarletSigns.

### Manual Installation

1. Download the latest release of **ScarletSigns**.

2. Extract the contents into your `BepInEx/plugins` folder:

   ```
   <V Rising Server Directory>/BepInEx/plugins/
   ```

   Your folder should now include:

   ```
   BepInEx/plugins/ScarletSigns.dll
   ```

3. Ensure **ScarletCore** and **VampireCommandFramework** are also installed in the `plugins` folder.

4. Start or restart your server.

---

## Configuration

All settings can be adjusted in the `ScarletSigns.cfg` file located in your server's `BepInEx/config` folder.

<details>
<summary>Show Settings</summary>

### General

- **Enable**: Enable or disable the plugin.  
  *Default: true*

</details>

---

## Tips & Tricks

- Use `\n` in your text to create multiple lines (e.g., `"Line 1\nLine 2\nLine 3"`)
- Color names include: white, red, green, blue, yellow, cyan, magenta, gray, etc.
- You can also use hex color codes (e.g., `#FF5733`)

---

## Join the [V Rising Mod Community on Discord](https://vrisingmods.com/discord)
