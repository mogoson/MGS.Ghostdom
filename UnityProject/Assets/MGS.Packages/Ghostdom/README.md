[TOC]

# MGS.Ghostdom

## Summary
- Display / Modify Game Object hierarchy tree, properties and Application Log by scene UI runtime to help analyze program bug.

## Version

- 0.1.0

## Environment
- Unity 5.6 or above.
- .Net Framework 3.5 or above.

## Platform
- Windows.
- Android.
- iOS.

## Demand
- Display Hierarchy, Inspector and Console runtime.
- Display Fields and Properties of each Component.
- Modify public Fields and writeable Properties runtime.

## Usage

1. Find the menu item "Tool/Ghostdom/Active" in Unity editor menu bar and click it to Active Ghostdom and the "Ghostdom" UI will auto display when your game is playing.
2. Click the menu item "Tool/Ghostdom/Inactive" to inactive Ghostdom before final build project to release, and the script and resource of Ghostdom will not build into game package.
3. The Lyre button "}{" to toggle the Ghostdom  UI.

   - "H" button to toggle the Hierarchy panel.
   - "I" button to toggle the Inspector panel.
   - "C" button to toggle the Console panel.
   - "#" button to switch the layout of panels.
4. The Hierarchy display node tree of Game Objects.

   - Click button "R" to refresh.

   - Input keyword to filter Game Object.
   - Click item to select.
5. The Inspector display the Components of the select Game Object, and Fields and Properties of each Component.

   - Click button "R" to refresh.
   - Input keyword to filter Component.
   - Input content to modify Field or Property.
6. The Console display Log/Warning/Error message output from Unity engine.

   - "L" button to toggle Log display active/inactive.
   - "W" button to toggle Warning display active/inactive.
   - "E" button to toggle Error display active/inactive.
   - "C" button to clear all messages.
   - Input keyword to filter message.

## Source

- https://github.com/mogoson/MGS.Ghostdom

------

Copyright © 2022 Mogoson.	mogoson@outlook.com
