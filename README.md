# Escape The Lab

> Unity · C# · First-Person Puzzle

## // complete the objectives · beat the clock

Wake up in an abandoned sci-fi facility and solve **four sequential puzzles** to escape before the **30-minute countdown** runs out. Built in Unity with a fully modular C# architecture.

---

## Scripts

### 🎮 Player

| File | Description |
|------|------------|
| `FPSController.cs` | Core player controller — movement, sprinting, jumping, crouching, head bob, zoom, and footstep audio |
| `PlayerDoorInteraction.cs` | Detects player proximity to doors, handles E key interaction and code submission |
| `KeyboardToUI.cs` | Captures keyboard input for the door code puzzle and displays it on-screen |

### 🚪 Doors & Puzzles

| File | Description |
|------|------------|
| `DoorController.cs` | Handles door rotation, unlock state, player-side detection, and audio feedback |
| `PuzzleManager.cs` | Orchestrates the four puzzle sequences with sequential dependency gating |
| `Key.cs` | Collectible trigger that advances the in-game map UI on pickup |
| `FinishTrigger.cs` | Detects the player reaching the exit and triggers the win state |

### 🔊 Audio & UI

| File | Description |
|------|------------|
| `AudioManager.cs` | Singleton managing all music, movement SFX, and event SFX across the game |
| `UIManager.cs` | Singleton handling panel visibility, game state transitions, and scene management |
| `CountdownTimer.cs` | 30-minute countdown — triggers Game Over when reaching zero |
| `PopupSound.cs` | Plays a sound whenever a UI popup becomes active |

### ✨ Visual Effects

| File | Description |
|------|------------|
| `EmissionFlicker.cs` | Randomly flickers material emission on lights to create an unsettling atmosphere |
| `RotateAndBob.cs` | Rotates and bobs collectible items to draw player attention |

---

## Puzzle Progression

1. **DOOR 01 — Access Code Entry**  
   Type the 4-digit code via keyboard, press Return to submit

2. **DOOR 02 — Stop Gas Leak**  
   Hold E for 3 seconds — requires Door 01 complete

3. **DOOR 03 — Activate Cryopod**  
   Hold E for 4 seconds — requires Doors 01 & 02 complete

4. **DOOR 04 — Collect Battery**  
   Auto-triggers when all previous puzzles are complete

5. **EXIT — Reach the exit trigger → You Win**

---

## Controls

| Key | Action |
|-----|--------|
| `WASD` | Move |
| `Left Shift` | Sprint |
| `Left Ctrl` | Crouch |
| `Space` | Jump |
| `E` | Interact |
| `Return` | Submit door code |
| `O` | Toggle objectives |
| `Right Mouse` | Zoom |
| `Escape` | Toggle cursor lock |

---

## Built With

- Unity
- C#
- Rigidbody Physics
- Unity Canvas UI
- AudioSource System
- Singleton Pattern
- Coroutines
- Trigger Colliders

---

## Author

**Maliq Ajala**  
Student ID: G00402583
