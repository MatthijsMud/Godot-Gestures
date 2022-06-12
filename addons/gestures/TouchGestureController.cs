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

    private readonly ISubject<InputEvent> gestures;
    private readonly IObservable<RawGesture> state;
    private readonly IObservable<(int index, Finger finger)> touches;
    private readonly IObservable<(int index, Finger finger)> releases;
    private readonly IObservable<(int index, Finger finger, Vector2 delta)> moves;

    public TouchGestureController()
    {
      var state = new BehaviorSubject<RawGesture>(new RawGesture());
      this.state = state.AsObservable();

      gestures = new Subject<InputEvent>();

      moves = gestures
      .OfType<InputEventScreenDrag>()
      .Select(@event => (@event.Index, new Finger(@event.Position), @event.Relative))
      .Publish()
      .RefCount();

      var fingerPressedStateChanges = gestures
      .OfType<InputEventScreenTouch>()
      .GroupBy(@event => @event.Index)
      .SelectMany(@event => @event.DistinctUntilChanged(@event => @event.Pressed))
      .Publish()
      .RefCount();

      touches = fingerPressedStateChanges
      .Where(@event => @event.Pressed)
      .Select(@event => (@event.Index, new Finger(@event.Position)))
      .Publish()
      .RefCount();

      releases = fingerPressedStateChanges
      // Note the negation compared to the previous section.
      .Where(@event => !@event.Pressed)
      .Select(@event => (@event.Index, new Finger(@event.Position)))
      .Publish()
      .RefCount();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
      base._UnhandledInput(@event);
      gestures.OnNext(@event);
    }

    private IObservable<(int index, Finger finger)> Tap()
    {
      return touches
      .SelectMany(touch =>
      {
        return releases
        .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(MaxTapDuration)))
        .TakeUntil(moves.Where(move =>
        {
          return (move.index == touch.index)
          && (MaxTapDistance < (touch.finger.Position - move.finger.Position).Length());
        }))
        .Where(release => release.index == touch.index)
        .Take(1);
      });
    }

    private sealed class Finger
    {
      public Vector2 Position { get; }
      public Finger(Vector2 position)
      {
        Position = position;
      }
    }

    /// <summary>
    /// Represents the location of all fingers that are touching the screen
    /// at some point in time.
    /// </summary>
    private sealed class RawGesture
    {
      public IReadOnlyDictionary<int, Finger> Fingers { get; }
      public RawGesture() : this(new Dictionary<int, Finger>())
      {
        // Forwarding constructor.
      }

      public RawGesture(IReadOnlyDictionary<int, Finger> fingers)
      {
        // External code could theoretically modify the dictionary.
        // As a "private" inner class, this scenario is unlikely; hence no
        // defensive copy is made.
        Fingers = fingers;
      }
    }
  }
}