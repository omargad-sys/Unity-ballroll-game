# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity 3D game project (Unity 6000.2.4f1) built with the Universal Render Pipeline (URP). The game is a collect-and-progress platformer where the player collects items, unlocks doors, avoids enemies, and navigates through a 3D environment.

**Project Name:** My project (DefaultCompany)
**Unity Version:** 6000.2.4f1
**Main Scene:** Assets/MiniGame.unity
**Template:** URP Blank Template

## Core Game Systems

### Player Controller (PlayerController.cs)
The player controller is the heart of the game, managing:
- Movement via Unity's new Input System (WASD/arrow keys)
- Jump mechanics with ground detection (Space key, using raycasts and collision events)
- Sprint functionality (hold Shift for speed multiplier)
- Dash feature (recent addition per git history)
- Pause system (Escape key toggles Time.timeScale)
- Item collection counter that triggers door unlocks at specific thresholds
- Respawn system when touching respawn triggers
- Enemy collision detection (destroys player and shows "You Lose!")

**Critical Implementation Details:**
- Ground detection uses both `Physics.Raycast` (2.0f downward for "Ground" tag) and OnCollisionEnter/Exit
- Doors unlock progressively: Door1 rotates at count >= 4, Door2 destroyed at >= 6, Door3 at >= 8, Door4 at >= 10
- Win condition at count >= 12 (destroys enemy, shows win text)
- Jump force is applied via `AddForce` with ForceMode.Impulse (jumpForce = 8.0f)
- Sprint multiplier is 2.0f

### Enemy System (EnemyMovement.cs)
- Uses Unity's NavMesh AI for pathfinding
- Continuously chases the player's Transform
- NavMesh baked surfaces exist for "Ground" and "Terrain" in Assets/MiniGame/
- Requires NavMeshAgent component

### Camera System (CameraController.cs)
- Third-person follow camera with fixed offset
- Calculates offset at Start() and maintains it in LateUpdate()
- Null-checks player before updating position

### Supporting Components
- **Rotator.cs:** Rotates objects (used for collectible pickup items) at constant speed (15, 30, 45 degrees/sec)
- **BackgroundMusic.cs:** Sets up looping background music on Start() with 0.5f volume
- **PetBee.cs:** Follow companion that maintains a stopping distance from the player and looks at them

## Unity Project Structure

```
Assets/
├── MiniGame.unity          # Main game scene
├── Scripts/                # All C# gameplay scripts
│   ├── PlayerController.cs
│   ├── EnemyMovement.cs
│   ├── CameraController.cs
│   ├── Rotator.cs
│   └── BackgroundMusic.cs
├── PetBee.cs              # Companion/pet system (note: not in Scripts/)
├── MiniGame/              # Scene-specific assets (NavMesh, lighting data)
├── PreFabs/               # Reusable game objects
├── Material/              # Custom materials
├── Resources/             # Runtime-loaded assets
├── Scenes/                # Additional scenes
└── [Asset Packs]          # Third-party assets (EVil Wizard, FourEvilDragonsHP, etc.)
```

## Tag System Requirements

The game relies on Unity tags for collision detection:
- **"PickUp"**: Collectible items (triggers count increment)
- **"Enemy"**: Enemy entities (triggers game over)
- **"Ground"**: Surfaces for ground detection and NavMesh
- **"respawn"**: Triggers that reset player position

## Input System

The project uses Unity's new Input System (InputSystem package 1.14.2):
- **InputSystem_Actions.inputactions**: Defines input actions
- Movement via OnMove() callback receiving Vector2 input
- Direct keyboard input for Jump (Space), Sprint (Shift), and Pause (Escape)

## Build Information

**Recent Builds:**
- Build 10-10/ (Nov 1, 2024)
- build 9-12/ (archived, should be in .gitignore)
- Updated 11:7/ (untracked, contains recent changes)

**Build Commands:**
Unity projects are typically built through the Unity Editor:
1. Open Unity Editor
2. File → Build Settings
3. Select target platform
4. Click "Build" or "Build and Run"

For command-line builds:
```bash
# Example Unity build command (adjust paths)
/Applications/Unity/Hub/Editor/[VERSION]/Unity.app/Contents/MacOS/Unity \
  -quit -batchmode -projectPath "/Users/omargad/Desktop/Downloads/My project" \
  -buildTarget StandaloneOSX -buildPath "./Builds/Mac" -executeMethod BuildCommand.PerformBuild
```

## Key Unity Packages

- **com.unity.render-pipelines.universal** (17.2.0): URP rendering
- **com.unity.inputsystem** (1.14.2): New input system
- **com.unity.ai.navigation** (2.0.9): NavMesh navigation
- **com.unity.ads** (4.4.2): Unity Ads integration
- **com.unity.purchasing** (4.12.2): In-app purchases
- **com.unity.ugui** (2.0.0): UI system (TextMeshPro for UI text)

## Development Workflow

### Working with Scripts
- C# scripts are in Assets/Scripts/ (except PetBee.cs which is in Assets/)
- Unity auto-compiles scripts on save
- Scripts must inherit from MonoBehaviour for Unity lifecycle methods
- Use SerializeField or public fields for Inspector-visible properties

### NavMesh Workflow
1. Select Ground/Terrain objects
2. Mark as "Navigation Static" in Inspector
3. Window → AI → Navigation → Bake
4. NavMesh data saved to Assets/MiniGame/NavMesh-*.asset

### Scene Editing
- Primary scene: Assets/MiniGame.unity
- Uses baked lighting (Lightmap data in Assets/MiniGame/)
- ReflectionProbe for realistic reflections

## Testing

Unity Test Framework (1.5.1) is installed:
- **Play Mode Tests**: Test gameplay at runtime
- **Edit Mode Tests**: Test editor-time functionality

Run tests via: Window → General → Test Runner

## Recent Features (Git History)

Based on recent commits:
- Jump mechanics implementation (Nov 12)
- Respawn points system (Nov 12)
- Pause functionality (Nov 10)
- Dash feature (Nov 10)
- Build artifacts cleanup

## Git Workflow

**Active Branch:** main
**Important:**
- Never commit .claude/ directory or CLAUDE.md files
- No AI attribution in commit messages
- Build folders should be gitignored (Build 10-10, build 9-12, etc.)
- Unity meta files (.meta) are tracked for proper asset reference
- Library/, Temp/, and Logs/ are gitignored

**Current uncommitted changes:**
- Modified: Assets/MiniGame.unity, ProjectSettings/TagManager.asset
- Untracked: Performance test files, recovery files, "Updated 11:7/" directory, mono_crash logs

## Common Issues and Solutions

### Ground Detection Problems
If jumping becomes unresponsive, check:
- Ground objects have "Ground" tag
- Raycast distance in PlayerController (currently 2.0f)
- Player's collider size and pivot position

### NavMesh Enemy Not Moving
- Ensure NavMesh is baked for current scene geometry
- Verify enemy has NavMeshAgent component
- Check that player Transform is assigned in Inspector

### Doors Not Unlocking
Doors use GameObject references and require specific count thresholds:
- Door1: Rotates at count >= 4
- Door2: Destroyed at count >= 6
- Door3: Destroyed at count >= 8
- Door4: Destroyed at count >= 10

Verify GameObject references are assigned in PlayerController Inspector.

## Architecture Patterns

### Progression System
The game uses a simple integer counter (`count` in PlayerController) that:
1. Increments when collecting "PickUp" tagged objects
2. Updates UI via TextMeshProUGUI component
3. Triggers door state changes at hardcoded thresholds
4. Determines win condition (>= 12)

This is tightly coupled to PlayerController; refactoring to a separate GameManager would improve modularity.

### Event-Driven Interactions
Currently using Unity's physics callbacks (OnTriggerEnter, OnCollisionEnter) for:
- Item collection
- Enemy detection
- Respawn triggers
- Ground state changes

### State Management
Minimal state management:
- Pause state via `isPaused` boolean and Time.timeScale
- Grounded state via `grounded` boolean
- Game over/win via UI activation

No formal state machine; adding one would benefit future complexity.

## Performance Considerations

- NavMesh agent count impacts performance (currently minimal)
- Baked lighting reduces runtime cost
- URP is optimized for mid-range devices
- Physics raycasts run every collision frame (consider optimization if adding many ground checks)
