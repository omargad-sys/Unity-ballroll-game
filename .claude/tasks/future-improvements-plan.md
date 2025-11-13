# Future Improvements Plan

**Created:** 2025-11-12
**Purpose:** Strategic roadmap for enhancing the Unity game project

## Overview

This document outlines potential improvements, features, and refactoring opportunities identified during project initialization. These suggestions prioritize MVP principles and incremental value delivery.

---

## High Priority: Code Quality & Architecture

### 1. Extract Game Progression Manager
**Current State:** Progression logic is tightly coupled to PlayerController
**Proposed Solution:** Create GameManager singleton

**Benefits:**
- Single source of truth for game state
- Easier to test progression logic
- Cleaner PlayerController focusing only on player input/physics
- Simplified save system integration

**Effort:** Medium (2-3 hours)

**Implementation Approach:**
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int collectibleCount = 0;
    public int CollectibleCount => collectibleCount;

    public UnityEvent<int> OnCollectibleCountChanged;
    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;

    // Door threshold configuration
    [SerializeField] private DoorConfig[] doorConfigs;
}
```

### 2. Configuration via ScriptableObjects
**Current State:** Magic numbers hardcoded (4, 6, 8, 10, 12 for thresholds)
**Proposed Solution:** ScriptableObject-based configuration

**Benefits:**
- Designer-friendly tuning without code changes
- Easy to create multiple difficulty levels
- Version control friendly (JSON-like format)
- Runtime modifications for testing

**Effort:** Low (1-2 hours)

**Files to Create:**
- Assets/Scripts/Config/GameConfig.cs
- Assets/ScriptableObjects/GameConfig.asset

### 3. Implement Event System
**Current State:** Direct GameObject references, tight coupling
**Proposed Solution:** Event-driven architecture using UnityEvents or custom event system

**Benefits:**
- Reduced dependencies between systems
- Easier to add new features without modifying existing code
- Better testability
- Support for multiple listeners

**Effort:** Medium (3-4 hours)

---

## Medium Priority: Gameplay Enhancements

### 4. Audio System Improvements
**Current Features:** Basic background music, pickup audio
**Proposed Additions:**
- Audio Manager singleton with volume controls
- Sound effects for: jump, land, door unlock, respawn, game over, win
- Audio mixing with separate channels (music, SFX, UI)
- Player preferences persistence

**Effort:** Medium (3-4 hours)

### 5. Visual Feedback & Polish
**Current State:** Minimal feedback on actions
**Proposed Additions:**
- Particle effects on item collection
- Door unlock animations/particles
- Player respawn effect
- Camera shake on enemy collision
- UI animations (count increment, door unlock notifications)

**Effort:** Medium-High (4-6 hours)

### 6. Enhanced Player Movement
**Current Features:** Walk, sprint, jump
**Proposed Additions:**
- Coyote time (grace period after leaving ledge)
- Jump buffering (press jump slightly before landing)
- Variable jump height (hold for higher jump)
- Double jump option
- Ground dust particles when moving

**Effort:** Medium (3-4 hours)

---

## Low Priority: New Features

### 7. Save/Load System
**Purpose:** Persist player progress between sessions

**Scope:**
- Save collectible count
- Save door states
- Save player position
- Auto-save on significant events
- Manual save option

**Implementation:**
- JSON serialization to Application.persistentDataPath
- Unity's JsonUtility or external library (Newtonsoft.Json)

**Effort:** Medium (4-5 hours)

### 8. Multiple Levels/Scenes
**Current State:** Single scene (MiniGame.unity)
**Proposed Enhancement:**
- Level selection menu
- Scene transition system
- Progress tracking across levels
- Unlocking system

**Effort:** High (6-8 hours)

### 9. Enemy Variety
**Current State:** Single enemy type with NavMesh chase
**Proposed Additions:**
- Patrolling enemies
- Stationary turret enemies
- Flying enemies
- Boss encounters

**Effort:** High (8-10 hours per enemy type)

### 10. Tutorial System
**Purpose:** Teach players game mechanics

**Components:**
- Popup hints for first-time actions
- Practice area for movement/jumping
- Arrow indicators for objectives
- Dismissible tutorial prompts

**Effort:** Medium (4-5 hours)

---

## Technical Debt & Maintenance

### 11. File Organization
**Issues:**
- PetBee.cs in Assets/ root instead of Scripts/
- No subfolder organization in Scripts/

**Proposed Structure:**
```
Assets/Scripts/
├── Player/
│   ├── PlayerController.cs
│   ├── PlayerAnimator.cs
│   └── PlayerAudio.cs
├── Enemy/
│   ├── EnemyMovement.cs
│   └── EnemyHealth.cs
├── Camera/
│   └── CameraController.cs
├── Collectibles/
│   └── Rotator.cs
├── Companions/
│   └── PetBee.cs
├── Managers/
│   ├── GameManager.cs
│   └── AudioManager.cs
├── UI/
│   └── UIManager.cs
└── Config/
    └── GameConfig.cs
```

**Effort:** Low (1 hour)

### 12. Add XML Documentation
**Current State:** Minimal code comments
**Proposed:** Add XML documentation to all public APIs

**Example:**
```csharp
/// <summary>
/// Controls player movement, jumping, and interaction with game objects.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Maximum movement speed in units per second.
    /// </summary>
    public float speed = 0;
}
```

**Effort:** Medium (2-3 hours)

### 13. Unit Testing Setup
**Current State:** Test Framework installed but no tests
**Proposed:**
- Test game progression logic
- Test door unlock thresholds
- Test player grounded detection logic
- Test collection counting

**Effort:** Medium (3-4 hours for initial setup + tests)

---

## Performance Optimizations

### 14. Object Pooling
**Use Cases:**
- Particle effects (when added)
- Collectible items (if procedurally spawning)
- Enemy projectiles (if added)

**Effort:** Medium (2-3 hours)

### 15. Optimize Raycast Calls
**Current State:** Raycast in OnCollisionEnter for ground detection
**Optimization:** Consider SphereCast or reducing frequency

**Effort:** Low (1 hour)

---

## Quality of Life Features

### 16. Settings Menu
**Features:**
- Volume controls (Master, Music, SFX)
- Graphics quality presets
- Control rebinding
- Reset progress option

**Effort:** Medium-High (5-6 hours)

### 17. Pause Menu
**Current State:** Pause freezes game with no UI
**Enhancement:**
- Pause menu UI overlay
- Resume, restart, quit options
- Access to settings from pause

**Effort:** Low-Medium (2-3 hours)

### 18. Death/Respawn Animation
**Current State:** Instant respawn or destruction
**Enhancement:**
- Fade to black transition
- "Respawning..." text
- Brief delay for impact
- Sound effect

**Effort:** Low (1-2 hours)

---

## Mobile Platform Support

### 19. Touch Controls
**Purpose:** Enable mobile gameplay
**Components:**
- Virtual joystick for movement
- Jump button
- Sprint button
- UI scaling for different screen sizes

**Effort:** Medium-High (5-7 hours)

### 20. Mobile Optimization
**Considerations:**
- Reduce draw calls
- LOD (Level of Detail) for models
- Simplified particle effects
- Texture compression
- Target 60 FPS on mid-range devices

**Effort:** High (8-12 hours)

---

## Monetization (If Applicable)

### 21. Unity Ads Integration
**Note:** Package already installed (com.unity.ads 4.4.2)
**Implementation:**
- Rewarded ads (extra lives, hints)
- Interstitial ads (between levels)
- Banner ads (non-intrusive placement)

**Effort:** Medium (3-5 hours)

### 22. In-App Purchases
**Note:** Package already installed (com.unity.purchasing 4.12.2)
**Options:**
- Remove ads purchase
- Cosmetic items (player skins)
- Level packs
- Currency packs

**Effort:** Medium-High (6-8 hours)

---

## Accessibility Features

### 23. Accessibility Options
**Features:**
- Colorblind modes
- Adjustable font sizes
- Subtitles for audio cues
- Simplified controls option
- Reduced motion mode

**Effort:** High (10-15 hours)

---

## Prioritization Matrix

### Do First (High Impact, Low Effort)
1. File Organization (1 hour)
2. ScriptableObject Configuration (1-2 hours)
3. Pause Menu UI (2-3 hours)

### Do Next (High Impact, Medium Effort)
1. Game Manager Extraction (2-3 hours)
2. Audio System Improvements (3-4 hours)
3. Event System Implementation (3-4 hours)

### Plan For Later (High Impact, High Effort)
1. Multiple Levels/Scenes (6-8 hours)
2. Mobile Touch Controls (5-7 hours)
3. Save/Load System (4-5 hours)

### Optional Enhancements (Lower Priority)
1. Enemy Variety (8-10 hours each)
2. Accessibility Features (10-15 hours)
3. Mobile Optimization (8-12 hours)

---

## Implementation Guidelines

When implementing any of these improvements:

1. **Create a Plan**: Use .claude/tasks/ to document approach before coding
2. **One Thing at a Time**: Complete and test each feature before moving to next
3. **Maintain Backward Compatibility**: Don't break existing functionality
4. **Test Incrementally**: Playtest after each change
5. **Document Changes**: Update CLAUDE.md with architectural changes
6. **Commit Frequently**: Small, focused commits with clear messages
7. **Consider MVP**: Implement core functionality first, polish later

---

## Notes

This plan is a living document. As the project evolves, priorities may shift based on:
- User feedback
- Technical discoveries
- Time constraints
- Platform requirements
- Business goals

Review and update this plan periodically to ensure alignment with project goals.
