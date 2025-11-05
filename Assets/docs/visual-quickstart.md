# Visual Quickstart: Getting Immediate On-Screen Progress

This guide helps you turn existing backend systems (cards, blessings, etc.) into visible Unity feedback fast.

## 1. Main Menu Scene
1. Create a new scene: `MainMenu.unity` (File > New Scene) and save under `Assets/Scenes/`.
2. Add an empty GameObject named `MainMenuController` and attach `MainMenuController.cs`.
3. UI Setup (uGUI):
   - Create a Canvas (if none) + EventSystem.
   - Inside Canvas, add a Panel (stretch middle) -> add a vertical layout.
   - Add Text (Title), Buttons: Play, Credits, Quit.
   - Button wiring detailed steps:
     1. In Hierarchy select the Play button.
     2. In the Inspector find the Button component (if using TextMeshPro it will be `Button (Script)` plus a child text object).
     3. Scroll to the bottom of the Button component where it says `On Click ()`.
     4. Press the `+` button to add a new event slot.
     5. Drag the GameObject that has `MainMenuController` (the one you created in step 2) from the Hierarchy into the empty object field of the new event.
     6. Click the dropdown labeled `No Function` -> navigate: `Game.UI.MainMenuController` -> select `OnPlayClicked()`.
     7. Repeat for the Quit button choosing `OnQuitClicked()` and the Credits button choosing `OnCreditsClicked()`.
     8. For the Back button inside the Credits panel: add an OnClick event and pick `OnBackFromCredits()`.
   - Test in Play Mode: clicking Play should load the gameplay scene (ensure it exists and is in Build Settings).
4. Optional Fade:
   - Add a full-screen panel with an Image + `CanvasGroup` component. Assign to `fadeGroup`.
5. Add a second panel for Credits (text + Back button); reference it in the controller.
6. Add this scene to Build Settings (File > Build Settings > Add Open Scenes).
   - If you're on a Unity version with Build Profiles: File > Build Profiles, select a profile (e.g. Default), click "Add Open Scenes" while MainMenu is open. Ensure both `MainMenu.unity` and later `Gameplay.unity` appear in the list.
   - You can verify from code: `Application.CanStreamedLevelBeLoaded("Gameplay")` will be false until `Gameplay.unity` is added.

## 2. Gameplay Scene Placeholder
1. Create `Gameplay.unity` in `Assets/Scenes/`.
2. Add an empty `RunRoot` GameObject for later systems.
3. (Optional) Place a background sprite or simple color camera clear.
 4. Add it to Build Settings / Build Profiles the same way as MainMenu.
 5. Test: Enter Play on MainMenu scene and press Play. If you see an error that the scene is not in build settings, reopen Gameplay scene and use Add Open Scenes again.

## 3. Card View Prefab
1. Create a `CardView` prefab under `Assets/Prefabs/UI/`.
2. Structure suggestion:
   - Root GameObject with `CardView` component.
   - Child Image: card frame (assign to `frameImage`).
   - Child Image: artwork (assign to `artworkImage`).
   - Text objects: Name (`nameText`), Cost (`costText`), Description (`descriptionText`).
   - Narrow Image for rarity stripe (`rarityStripe`).
3. Drag a `CardDefinition` asset into a test spawner script (you can make an empty `CardTestSpawner.cs` that instantiates and calls `Bind`).
4. Run scene to see card rendering.

## 4. Map Prototype
1. Create an empty GameObject `MapGeneratorRoot` in `Gameplay.unity`.
2. Attach `MapGenerator.cs`.
3. Create a node prefab:
   - Empty GameObject `MapNodePrefab` with `MapNode` component.
   - (Optional) Add a SpriteRenderer (circle sprite) or small UI element.
4. Assign prefab to the generator.
5. Press Play. Gizmos will show colored spheres (toggle Gizmos on in Scene view). Start node (green) at top, Boss (red) at bottom, random path types in between.
6. Adjust rows/columns/spacing/jitter to iterate quickly.

## 5. Wiring Existing Data
- Create a few `CardDefinition` assets (Right-click in Project window > Create > Game > Cards > Card).
- Populate fields (cost >= 0, description, etc.).
- Use the Card View prefab in a temporary scene to display them.

## 6. Suggested Next Visual Steps
- Simple combat board: two horizontal groups (player hand at bottom, enemy intent icons at top).
- Floating damage text (basic Text Mesh Pro). Add later when integrating combat.
- Skill tree branch selection: 4 buttons representing branches shown only once at run start.

## 7. Troubleshooting
- If Play button does nothing: verify `gameplaySceneName` matches an added build scene name.
- Missing colors: ensure references assigned in prefab or adjust inspector values.
- Node prefab warning: must include `MapNode` component.

## 8. Extensibility Notes
- Replace Gizmo spheres with SpriteRenderer icons for production.
- Add a simple line renderer pool for connections (future polish).
- Card hover enlarge: use EventTrigger to call `OnHover(true/false)`.

## 9. Folder Suggestions
```
Assets/
  Scenes/
    MainMenu.unity
    Gameplay.unity
  Prefabs/
    UI/
      CardView.prefab
      MapNodePrefab.prefab
```

## 10. Quick Test Checklist
- [ ] MainMenu loads gameplay scene.
- [ ] Card prefab displays name/cost/description.
- [ ] Map generates colored nodes.
- [ ] No console errors.

Iterate visually here while deeper systems evolve.
