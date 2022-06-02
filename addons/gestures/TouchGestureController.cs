using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Disposables;

namespace Godot.Gestures
{
  sealed class TouchGestureController : Node
  {
    #region Settings

    /// <summary>
    /// Maximum amount of time that can pass between pressing and releasing
    /// for the input to be considered a tap. 
    /// </summary>
    [Export]
    public float MaxTapDuration { get; private set; } = 0.2f;

    /// <summary>
    /// Max distance a finger can move before the input is no longer
    /// considered a tap. This to account for slight movements.
    /// </summary>
    [Export]
    public float MaxTapDistance { get; private set; } = 24f;

    /// <summary>
    /// Minimum amount of time that should pass between pressing and releasing
    /// for the input to be considered a long press. 
    /// </summary>
    [Export]
    public float MinLongPressDuration { get; private set; } = 0.75f;

    /// <summary>
    /// Max distance a finger can move before the input is no longer
    /// considered a long press. This to account for slight movements.
    /// </summary>
    [Export]
    public float MaxLongPressDistance { get; private set; } = 24f;
    /// <summary>
    /// Swiping/flicking is distinguished from dragging based on the duration 
    /// of the gesture. Any "gesture" that takes longer than this is thus not
    /// considered a swipe. 
    /// </summary>
    [Export]
    public float MaxSwipeDuration { get; private set; } = 0.5f;
    /// <summary>
    /// Minimum distance a finger should move before the input is considered 
    /// a swipe.
    /// </summary>
    [Export]
    public float MinSwipeDistance { get; private set; } = 200f;
    #endregion
  }
}