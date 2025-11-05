# Deckbuilder

Unity project (deck-building game prototype). Includes scenes: `testScene` (main menu) and `OptionsScene` (options menu with TMP UI). Uses Universal Render Pipeline and TextMesh Pro.

## Requires
- Unity Editor (version recorded in `ProjectSettings/ProjectVersion.txt`)
- TextMesh Pro (included via Packages)
- Universal Render Pipeline

## Getting Started
1. Clone the repo:
   ```bash
   git clone https://github.com/jsaura27/Deckbuilder.git
   cd Deckbuilder
   ```
2. Open in Unity Hub (it will read ProjectVersion.txt and select matching editor).
3. Open `Assets/Scenes/testScene.unity` to start.

## Folder Structure
- `Assets/` Source assets, scripts, scenes.
- `Packages/` Unity package manifest and lock file (keep committed).
- `ProjectSettings/` Unity project configuration (keep committed).

Generated folders ignored by `.gitignore`: `Library/`, `Temp/`, `Logs/`, `UserSettings/`.

## Options Menu
Implemented in `Assets/Scripts/UI/MainMenu/OptionsController.cs` with:
- Master volume slider (AudioListener.volume)
- Fullscreen toggle
- Quality dropdown (TMP)
- Back button (returns to main scene)

## Contributing
Branch from `main`, create feature branches, open PRs. Keep `.meta` files when adding assets.

## License
Add a license of your choice (e.g., MIT) here.
