# VR Archery Game Documentation

## Overview
This document provides detailed information about the VR Archery Game developed using Unity and the Meta SDK. The game features realistic bow and arrow mechanics, including interactions using VR controllers and a physics-based system for firing arrows.

---

## Classes and Scripts

### 1. **GrabInteractableWraper**
This script acts as a wrapper for the `GrabInteractable` class, providing additional functionality for managing interactions.

#### Key Methods:
- `GetInteractable()`: Retrieves the `GrabInteractable` component.
- `HaveSelectedInteractor()`: Checks if any interactor is selecting the interactable.
- `GetSelectedInteractorList()`: Returns a list of current selecting interactors.
- `GetSingleSelectedInteractor()`: Retrieves the interactor from either the left or right controller.
- `IsLeftControllerInteractorIsSelected()`: Checks if the left controller interactor is selecting.
- `IsRightControllerInteractorIsSelected()`: Checks if the right controller interactor is selecting.
- `IsControllerInteractorIsSelected()`: Checks if either controller is selecting.

#### Usage Example:
```csharp
if (grabInteractableWraper.IsControllerInteractorIsSelected()) {
    Debug.Log("Controller is interacting with the object.");
}
```

---

### 2. **GrabInteractorWraper**
This script wraps the `GrabInteractor` class, managing interaction events and controller types.

#### Key Methods:
- `GetInteractor()`: Retrieves the `GrabInteractor` component.
- `HaveSelectedInteractable()`: Checks if the interactor has selected any interactable.
- `SetInteractable(GrabInteractable grabInteractable)`: Forces selection of a specific interactable.
- `ReleaseInteractable()`: Releases the currently selected interactable.
- `GetControllerType()`: Returns the type of controller (left or right).

#### Usage Example:
```csharp
GrabInteractorWraper.leftControllerInteractor.SetInteractable(targetGrabInteractable);
```

---

### 3. **Arrow**
Handles the arrow's interactions, physics, and firing logic.

#### Key Methods:
- `ConstrainGrabInteractor(bool value)`: Constrains the rotation of the arrow while grabbing.
- `Fire(float powerRatio)`: Fires the arrow based on the draw power ratio.
- `DisableSnapInteractor()`: Disables the snap interactor and grab interactable for the arrow.

#### Usage Example:
```csharp
arrow.Fire(0.8f); // Fires the arrow with 80% power.
```

#### Inspector Fields:
- `snapInteractor`: Reference to the `SnapInteractor`.
- `grabInteractable`: Reference to the `GrabInteractable`.
- `force`: Base force multiplier for firing the arrow.

---

### 4. **Bow**
Handles bow mechanics, including string interactions and firing logic.

#### Key Methods:
- `ResetStringPosition()`: Resets the bowstring to its default position.
- `WhenSnapHapends(SnapInteractor interactor)`: Handles snap events for attaching an arrow.
- `WhenSelectingInteractorRemoved(GrabInteractor interactor)`: Fires the arrow when the interactor releases the string.

#### Inspector Fields:
- `stringGrabInteractorWraper`: Wrapper for the bowstring's grab interactable.
- `stringTransform`: Transform of the bowstring.
- `stringSnapInteractable`: Snap interactable for the string.

#### Usage Example:
```csharp
if (bow.currentArrow != null) {
    bow.ResetStringPosition();
}
```

---

## Key Features

### 1. **Grab and Snap Interactions**
- **GrabInteractableWraper** and **GrabInteractorWraper** provide high-level APIs for managing VR controller interactions.
- The bowstring and arrow are interactable via grab mechanics, allowing realistic string pulling and arrow nocking.

### 2. **Physics-Based Firing**
- The `Arrow` script uses Unity's Rigidbody physics to simulate arrow motion.
- Power calculation is based on the bowstring draw amount.

### 3. **Controller-Specific Interactions**
- Left and right controllers are managed separately using the `Handedness` property in **GrabInteractorWraper**.
- Each controller can independently interact with objects in the scene.

---

## Setup and Integration

### Prerequisites:
- Unity version: 2021.3 or later.
- Meta SDK for VR integration.

### Steps:
1. Import the Meta SDK and required packages.
2. Add the provided scripts to the appropriate game objects.
3. Configure inspector fields for `Arrow`, `Bow`, and related components.
4. Set up grab and snap interactables for the bowstring and arrows.

---

## Debugging and Testing

### Common Issues:
1. **Arrow Not Firing:**
   - Ensure the `Arrow` script references are correctly set in the inspector.
   - Verify the Rigidbody is not marked as kinematic.

2. **Bowstring Not Resetting:**
   - Check if the `ResetStringPosition()` method is called properly.
   - Ensure the string grab interactable is active.

3. **Controller Interactions Not Working:**
   - Confirm the controller interactors are correctly assigned in the `GrabInteractorWraper`.

### Debug Logs:
Use the `Debug.Log` statements in the scripts for runtime inspection of events and state changes.

---

## Future Improvements
1. **Enhanced Haptics:** Add haptic feedback when the arrow is released.
2. **Visual Effects:** Implement particle effects for fired arrows.
3. **Score System:** Track hits and misses for player performance evaluation.
4. **Multiplayer Mode:** Enable competition by adding multiple players.

---

## Contact
For further assistance or feature requests, please contact the development team.

