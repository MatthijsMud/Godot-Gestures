# Godot Gestures (C#)

This project relies on [`System.Reactive`][rxnet] If not using this project's C# file
```bash
$ dotnet add package System.Reactive
```


## Modes

### Touch

Open the project settings:
<kbd><kbd><samp>Project</samp></kbd> » <kbd><samp>Project Settings…</samp></kbd></kbd>, and head to the <kbd><samp>AutoLoad</samp></kbd> tab. 

When selecting the <kbd><samp>Path</samp></kbd> to the "singleton", it is suggested to use the scene `addons/gestures/TouchGestureController.tscn` (assuming the default path) rather than the script. Both instances should work just fine, but using the scene allows for modifying the settings from the inspector (by editing the scene in question).

This controller provides no useful interface for other classes; instead passing the events to the `Input` singleton. As such, <kbd><samp>Node Name</samp></kbd> is of little relevance.

### Emulation

Some devices do not have a means to input any of the [Supported gestures](#supported-gestures). Be it due to not having a touch screen, or only supporting a single touch. Development PC's are more prone to this limitation, which can be inconvenient. 

To alleviate this issue somewhat, this project provides alternative means to input certain gestures; Instead relying on mouse clicks/movements, scrolling and some keyboard presses to modify the gestures on the fly.

> :warning: **Warning:** These emulated gestures aren't a perfect replacement for the ones based on touches. They should however make it possible for more easily creating cross-platform applications with different input methods.

## Supported gestures

- [Tap](#tap)
- [Long press](#long-press)
- [Drag](#drag)
- [Multi drag](#multi-drag)
- [Pinch](#pinch)
- [Twist](#twist)

### Tap

<div align="center">

![One blob (representing a finger touching the screen) that disappears shortly after appearing, demonstrating the "tap" gesture.][tap]

</div> 

Gesture where one finger makes contact with the screen for a very short time, with little movement. This gesture is typically used for selecting things or clicking on buttons.

#### Details



#### `InputEventScreenTap`

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Last known position of the finger that triggered this event before it no longer touched the screen. |

### Long press

<div align="center">

![One stationary blob (representing a finger touching the screen) that disappears about 1 second after appearing, demonstrating the "tap" gesture.][longpress]

</div> 

#### `InputEventScreen`

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Position of the finger at the moment this event was triggered.  |

### Drag

<div align="center">

![One blob (representing fingers touching the screen) moving up and down in usison, demonstrating the "drag" gesture.][drag]

</div> 

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Position of the finger that triggered |
| `Delta` | Indicates the movement of the finger relative to the previous frame. |

### Multi-drag

<div align="center">

![Two blobs (representing fingers touching the screen) moving up and down in usison, demonstrating the "multi-drag" gesture.][multidrag]

</div> 

Gesture where two or more fingers all move in the same general direction. This gesture is typically used in contexts where at least two types of actions are associated with a drag gesture, where the number of fingers is used to determine which action to perform.

For example: A drawing application that supports zooming in and out might also want to support panning the canvas. Both drawing and panning would typically involve the dragging gesture. 



| Property | Description |
|-|-|
| `Position` | Center of the fingers involved in the gesture. |
| `Delta` | Indicates the movement of the center relative to the previous frame. | 

### Pinch

<div align="center">

![Two blobs (representing fingers touching the screen) moving towards and away from each other, demonstrating the "pinch" gesture.][pinch]

</div>

Gesture where two or more fingers either move toward or away from one another. This gesture is typically used for zooming in/out.

#### `InputEventScreenPinch`

| Property | Description |
|-|-|
| `Position` | Center of the fingers involved in the gesture. |
| `Factor` | Positive numbers indicate the touch positions move away from eachother; negative numbers indicate they move toward eachother.|


### Twist

<div align="center">

![Two blobs (representing fingers touching the screen) moving in circles around a single point, demonstrating the "twist" gesture.][twist]
  
</div>

Gesture where two or more fingers move in "circles" around a point. This gesture is typically used for rotating a canvas, or twisting knobs.

#### `InputEventScreenTwist`

| Property | Description |
|-|-|
| `Position` | Center of the fingers involved in the gesture. | 
| `Angle` | Rotation (in radians) | 


[rxnet]: https://github.com/dotnet/reactive

[tap]: ./readme/Tap.svg
[longpress]: ./readme/Longpress.svg
[drag]: ./readme/Drag.svg
[multidrag]: ./readme/Multidrag.svg
[pinch]: ./readme/Pinch.svg
[twist]: ./readme/Twist.svg
