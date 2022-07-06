using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Godot.Gestures
{
  public sealed class GodotInputEventBroadcaster : Node
  {

    private readonly CompositeDisposable subscriptions;
    public GodotInputEventBroadcaster()
    {
      subscriptions = new CompositeDisposable();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        subscriptions.Dispose();
      }
      base.Dispose(disposing);
    }

    public override void _Ready()
    {
      base._Ready();
      var compound = new CompositeDisposable();
      foreach (var child in GetChildren())
      {
        if (SubscribeToSource(child) is IDisposable subscriptions)
        {
          compound.Add(subscriptions);
        }
      }
    }

    private IDisposable? SubscribeToSource(object? source)
    {
      if (source == null) return null;

      var potentialSubscriptions = new Func<object?, IDisposable?>[]
      {
        HandleTaps,
        HandleLongPresses,
        HandlePinches,
        HandleTwists,
      };

      var subscriptions = new CompositeDisposable();

      foreach (var subscribeTo in potentialSubscriptions)
      {
        if (subscribeTo(source) is IDisposable subscription)
        {
          subscriptions.Add(subscription);
        }
      }

      return subscriptions;
    }

    private IDisposable? HandleTaps(object? source)
    {
      return (source as ITapGestureRecognizer)?.Taps
      .Do(tap => { Input.ParseInputEvent(new InputEventSingleScreenTap()); })
      .Subscribe();
    }

    private IDisposable? HandleLongPresses(object? source)
    {
      return (source as ILongPressRecognizer)?.LongPresses
      .Do(longPress => { Input.ParseInputEvent(new InputEventSingleScreenLongPress()); })
      .Subscribe();
    }

    private IDisposable? HandlePinches(object? source)
    {
      return (source as IPinchGestureRecognizer)?.Pinches
      .Do(pinch =>
      {
        Input.ParseInputEvent(new InputEventScreenPinch
        {
          Factor = pinch.Factor,
        });
      })
      .Subscribe();
    }

    private IDisposable? HandleTwists(object? source)
    {
      return (source as ITwistGestureRecognizer)?.Twists
      .Do(twist => { Input.ParseInputEvent(new InputEventScreenTwist()); })
      .Subscribe();
    }
  }
}