# Godot Gestures (C#)

This project relies on [`System.Reactive`][rxnet] If not using this project's C# file
```bash
$ dotnet add package System.Reactive
```

Open the project settings:
<kbd><kbd><samp>Project</samp></kbd> » <kbd><samp>Project Settings…</samp></kbd></kbd>, and head to the <kbd><samp>AutoLoad</samp></kbd> tab. 

When selecting the <kbd><samp>Path</samp></kbd> to the "singleton", it is suggested to use the scene `addons/gestures/TouchGestureController.tscn` (assuming the default path) rather than the script. Both instances should work just fine, but using the scene allows for modifying the settings from the inspector (by editing the scene in question).

This controller provides no useful interface for other classes; instead passing the events to the `Input` singleton. As such, <kbd><samp>Node Name</samp></kbd> is of little relevance.


## Tap

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Last known position of the finger that triggered this event before it no longer touched the screen. |

## Long press

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Position of the finger at the moment this event was triggered.  |

## Drag

| Property | Description |
|-|-|
| `Index` | Finger which triggered this touch event. |
| `Position` | Position of the finger that triggered |
| `Delta` | |

## Multi-drag

| Property | Description |
|-|-|
| `Position` | |
| `Delta` | | 

## Pinch

<div align="center">

![Two blobs (representing fingers touching the screen) moving towards and away from each other, demonstrating the "pinch" gesture.][pinch]

</div>

Gesture where two or more fingers either move toward or away from one another. This gesture is typically used for zooming in/out.

| Property | Description |
|-|-|
| `Position` | Center of |
| `Factor` | |


## Twist

<div align="center">

![Two blobs (representing fingers touching the screen) moving in circles around a single point, demonstrating the "twist" gesture.][twist]
  
</div>

Gesture where two or more fingers move in "circles" around a point. This gesture is typically used for rotating a canvas, or twisting knobs.

| Property | Description |
|-|-|
| `Position` | Center


[rxnet]: https://github.com/dotnet/reactive
[pinch]: ./readme/Pinch.svg
[twist]: ./readme/Twist.svg