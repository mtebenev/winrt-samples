using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.Foundation;

namespace Mt.Common.WinRtUiCore.Platform
{
	/// <summary>
	/// Note: c/p from nRoute
	/// based on http://wpf.codeplex.com/SourceControl/changeset/view/29533#370365
	/// </summary>
	public class ExtendedVisualStateManager
			: VisualStateManager
	{
		private static readonly Duration DurationZero = new Duration(TimeSpan.Zero);

		public static bool GoToElementState(FrameworkElement root, string stateName, bool useTransitions)
		{
			var _visualStateManager = VisualStateManager.GetCustomVisualStateManager(root) as ExtendedVisualStateManager;
			if(_visualStateManager != null)
			{
				return _visualStateManager.GoToStateInternal(root, stateName, useTransitions);
			}
			else
			{
				return false;
			}
		}

		protected override bool GoToStateCore(Control control, FrameworkElement templateRoot, string stateName, VisualStateGroup group,
				VisualState state, bool useTransitions)
		{
			return GoToStateInternal(control, templateRoot, group, state, useTransitions);
		}

		private static Storyboard GenerateDynamicTransitionAnimations(FrameworkElement root, VisualStateGroup group, VisualState newState, VisualTransition transition)
		{
			Storyboard dynamic = new Storyboard();
			if(transition != null && transition.GeneratedDuration != null)
			{
				dynamic.Duration = transition.GeneratedDuration;
			}
			else
			{
				dynamic.Duration = new Duration(TimeSpan.Zero);
			}

			Dictionary<TimelineDataToken, Timeline> currentAnimations = FlattenTimelines(group.GetCurrentStoryboards());
			Dictionary<TimelineDataToken, Timeline> transitionAnimations = FlattenTimelines(transition != null ? transition.Storyboard : null);
			Dictionary<TimelineDataToken, Timeline> newStateAnimations = FlattenTimelines(newState.Storyboard);

			// Remove any animations that the transition already animates.
			// There is no need to create an interstitial animation if one already exists.
			foreach(KeyValuePair<TimelineDataToken, Timeline> pair in transitionAnimations)
			{
				currentAnimations.Remove(pair.Key);
				newStateAnimations.Remove(pair.Key);
			}

			// Generate the "to" animations
			foreach(KeyValuePair<TimelineDataToken, Timeline> pair in newStateAnimations)
			{
				// The new "To" Animation -- the root is passed as a reference point for name
				// lookup.  
				Timeline toAnimation = GenerateToAnimation(root, pair.Value, true);

				// If the animation is of a type that we can't generate transition animations
				// for, GenerateToAnimation will return null, and we should just keep going.
				if(toAnimation != null)
				{
					toAnimation.Duration = dynamic.Duration;
					dynamic.Children.Add(toAnimation);
				}

				// Remove this from the list of current state animations we have to consider next
				currentAnimations.Remove(pair.Key);
			}

			// Generate the "from" animations
			foreach(KeyValuePair<TimelineDataToken, Timeline> pair in currentAnimations)
			{
				Timeline fromAnimation = GenerateFromAnimation(pair.Value);
				if(fromAnimation != null)
				{
					fromAnimation.Duration = dynamic.Duration;
					string targetName = Storyboard.GetTargetName(pair.Value);
					Storyboard.SetTargetName(fromAnimation, targetName);

					// If the targetName of the existing Animation is known, then look up the
					// target 
					DependencyObject target = String.IsNullOrEmpty(targetName) ?
																			null : root.FindName(targetName) as DependencyObject;
					if(target != null)
					{
						Storyboard.SetTarget(fromAnimation, target);
					}

					string propertyName = Storyboard.GetTargetProperty(pair.Value);
					Storyboard.SetTargetProperty(fromAnimation, propertyName);
					dynamic.Children.Add(fromAnimation);
				}
			}

			return dynamic;
		}

		internal static VisualTransition GetTransition(FrameworkElement element, VisualStateGroup group, VisualState from, VisualState to)
		{
			if(element == null)
			{
				throw new ArgumentNullException("element");
			}

			if(group == null)
			{
				throw new ArgumentNullException("group");
			}

			if(to == null)
			{
				throw new ArgumentNullException("to");
			}

			VisualTransition best = null;
			VisualTransition defaultTransition = null;
			int bestScore = -1;

			IList<VisualTransition> transitions = (IList<VisualTransition>)group.Transitions;
			if(transitions != null)
			{
				foreach(VisualTransition transition in transitions)
				{
					if(defaultTransition == null && IsDefaultTransition(transition))
					{
						defaultTransition = transition;
						continue;
					}

					int score = -1;

					VisualState transitionFromState = group.States.Where((s) => s.Name == transition.From).FirstOrDefault();
					VisualState transitionToState = group.States.Where((s) => s.Name == transition.To).FirstOrDefault();

					if(from == transitionFromState)
					{
						score += 1;
					}
					else if(transitionFromState != null)
					{
						continue;
					}

					if(to == transitionToState)
					{
						score += 2;
					}
					else if(transitionToState != null)
					{
						continue;
					}

					if(score > bestScore)
					{
						bestScore = score;
						best = transition;
					}
				}
			}

			return best ?? defaultTransition;
		}

		private static Timeline GenerateFromAnimation(Timeline timeline)
		{
			if(timeline is ColorAnimation || timeline is ColorAnimationUsingKeyFrames)
			{
				return new ColorAnimation();
			}

			if(timeline is DoubleAnimation || timeline is DoubleAnimationUsingKeyFrames)
			{
				return new DoubleAnimation();
			}

			if(timeline is PointAnimation || timeline is PointAnimationUsingKeyFrames)
			{
				return new PointAnimation();
			}

			// All other animation types are ignored. We will not build transitions for them,
			// but they will end up being executed.
			return null;
		}

		// These methods are used when generating a transition animation between states.
		// The timeline is the "to" state, and we need to find the To value for the
		// animation we're generating.
		private static Color? GetTargetColor(Timeline timeline, bool isEntering)
		{
			ColorAnimation ca = timeline as ColorAnimation;
			if(ca != null)
			{
				return ca.From.HasValue ? ca.From : ca.To;
			}

			ColorAnimationUsingKeyFrames cak = timeline as ColorAnimationUsingKeyFrames;
			if(cak != null)
			{
				if(cak.KeyFrames.Count == 0)
				{
					return null;
				}

				ColorKeyFrame keyFrame = cak.KeyFrames[isEntering ? 0 : cak.KeyFrames.Count - 1];
				return keyFrame.Value;
			}

			return null;
		}

		private static double? GetTargetDouble(Timeline timeline, bool isEntering)
		{
			DoubleAnimation da = timeline as DoubleAnimation;
			if(da != null)
			{
				return da.From.HasValue ? da.From : da.To;
			}

			DoubleAnimationUsingKeyFrames dak = timeline as DoubleAnimationUsingKeyFrames;
			if(dak != null)
			{
				if(dak.KeyFrames.Count == 0)
				{
					return null;
				}

				DoubleKeyFrame keyFrame = dak.KeyFrames[isEntering ? 0 : dak.KeyFrames.Count - 1];
				return keyFrame.Value;
			}

			return null;
		}

		private static Point? GetTargetPoint(Timeline timeline, bool isEntering)
		{
			PointAnimation pa = timeline as PointAnimation;
			if(pa != null)
			{
				return pa.From.HasValue ? pa.From : pa.To;
			}

			PointAnimationUsingKeyFrames pak = timeline as PointAnimationUsingKeyFrames;
			if(pak != null)
			{
				if(pak.KeyFrames.Count == 0)
				{
					return null;
				}

				PointKeyFrame keyFrame = pak.KeyFrames[isEntering ? 0 : pak.KeyFrames.Count - 1];
				return keyFrame.Value;
			}

			return null;
		}

		private static Timeline GenerateToAnimation(FrameworkElement root, Timeline timeline, bool isEntering)
		{
			Timeline result = null;

			Color? targetColor = GetTargetColor(timeline, isEntering);
			if(targetColor.HasValue)
			{
				ColorAnimation ca = new ColorAnimation() { To = targetColor };
				result = ca;
			}

			if(result == null)
			{
				double? targetDouble = GetTargetDouble(timeline, isEntering);
				if(targetDouble.HasValue)
				{
					DoubleAnimation da = new DoubleAnimation() { To = targetDouble };
					result = da;
				}
			}

			if(result == null)
			{
				Point? targetPoint = GetTargetPoint(timeline, isEntering);
				if(targetPoint.HasValue)
				{
					PointAnimation pa = new PointAnimation() { To = targetPoint };
					result = pa;
				}
			}

			if(result != null)
			{
				string targetName = Storyboard.GetTargetName(timeline);
				Storyboard.SetTargetName(result, targetName);
				if(!String.IsNullOrEmpty(targetName))
				{
					DependencyObject target = root.FindName(targetName) as DependencyObject;
					if(target != null)
					{
						Storyboard.SetTarget(result, target);
					}
				}

				Storyboard.SetTargetProperty(result, Storyboard.GetTargetProperty(timeline));
			}

			return result;
		}

		private static Dictionary<TimelineDataToken, Timeline> FlattenTimelines(Storyboard storyboard)
		{
			Dictionary<TimelineDataToken, Timeline> result = new Dictionary<TimelineDataToken, Timeline>();

			FlattenTimelines(storyboard, result);

			return result;
		}

		private static Dictionary<TimelineDataToken, Timeline> FlattenTimelines(Collection<Storyboard> storyboards)
		{
			Dictionary<TimelineDataToken, Timeline> result = new Dictionary<TimelineDataToken, Timeline>();

			for(int index = 0; index < storyboards.Count; ++index)
			{
				FlattenTimelines(storyboards[index], result);
			}

			return result;
		}

		private static void FlattenTimelines(Storyboard storyboard, Dictionary<TimelineDataToken, Timeline> result)
		{
			if(storyboard == null)
			{
				return;
			}

			for(int index = 0; index < storyboard.Children.Count; ++index)
			{
				Timeline child = storyboard.Children[index];
				Storyboard childStoryboard = child as Storyboard;
				if(childStoryboard != null)
				{
					FlattenTimelines(childStoryboard, result);
				}
				else
				{
					result[new TimelineDataToken(child)] = child;
				}
			}
		}

		// specifies a token to uniquely identify a Timeline object
		private struct TimelineDataToken : IEquatable<TimelineDataToken>
		{
			public TimelineDataToken(Timeline timeline)
			{
				_targetName = Storyboard.GetTargetName(timeline);
				_targetProperty = Storyboard.GetTargetProperty(timeline);
			}

			public bool Equals(TimelineDataToken other)
			{
				return ((other._targetName == _targetName) &&
						(other._targetProperty == _targetProperty));
			}

			public override int GetHashCode()
			{
				return _targetName.GetHashCode() ^ _targetProperty.GetHashCode();
			}

			private string _targetName;
			private string _targetProperty;
		}

		private static bool GoToStateInternal(Control control, FrameworkElement element, VisualStateGroup group, VisualState state, bool useTransitions)
		{
			if(element == null)
			{
				throw new ArgumentNullException("element");
			}

			if(state == null)
			{
				throw new ArgumentNullException("state");
			}

			if(group == null)
			{
				throw new InvalidOperationException();
			}

			//VisualState lastState = group.CurrentState;
			VisualState lastState = group.GetCurrentState();
			if(lastState == state)
			{
				return true;
			}

			// Get the transition Storyboard. Even if there are no transitions specified, there might
			// be properties that we're rolling back to their default values.
			VisualTransition transition = useTransitions ? ExtendedVisualStateManager.GetTransition(element, group, lastState, state) : null;

			// Generate dynamicTransition Storyboard
			Storyboard dynamicTransition = GenerateDynamicTransitionAnimations(element, group, state, transition);

			// If the transition is null, then we want to instantly snap. The dynamicTransition will
			// consist of everything that is being moved back to the default state.
			// If the transition.Duration and explicit storyboard duration is zero, then we want both the dynamic 
			// and state Storyboards to happen in the same tick, so we start them at the same time.
			if(transition == null || (transition.GeneratedDuration == DurationZero &&
																			(transition.Storyboard == null || transition.Storyboard.Duration == DurationZero)))
			{
				// Start new state Storyboard and stop any previously running Storyboards
				if(transition != null && transition.Storyboard != null)
				{
					group.StartNewThenStopOld(element, transition.Storyboard, state.Storyboard);
				}
				else
				{
					group.StartNewThenStopOld(element, state.Storyboard);
				}

				// Fire both CurrentStateChanging and CurrentStateChanged events
				//group.RaiseCurrentStateChanging(element, lastState, state, control);
				//group.RaiseCurrentStateChanged(element, lastState, state, control);
			}
			else
			{
				// In this case, we have an interstitial storyboard of duration > 0 and/or
				// explicit storyboard of duration >0 , so we need 
				// to run them first, and then we'll run the state storyboard.
				// we have to wait for both storyboards to complete before
				// starting the steady state animations.
				transition.SetDynamicStoryboardCompleted(false);  //transition.DynamicStoryboardCompleted = false;

				// Hook up generated Storyboard's Completed event handler
				dynamicTransition.Completed += delegate(object sender, Object e)
				{
					// transition.ExplicitStoryboardCompleted) &&
					if((transition.Storyboard == null || transition.GetExplicitStoryboardCompleted()) &&

							// If the element or control is removed from the tree, then the new
						// storyboards will not be able to resolve target names. Thus,
						// if the element or control is unloaded, don't start the new
						// storyboards.
						//(element.IsLoaded && (control == null || control.IsLoaded)))
							(element.Parent != null && (control == null || control.Parent != null)))
					{
						group.StartNewThenStopOld(element, state.Storyboard);
					}

					//group.RaiseCurrentStateChanged(element, lastState, state, control);
					transition.SetDynamicStoryboardCompleted(true);  //transition.DynamicStoryboardCompleted = true;
				};

				// if (transition.Storyboard != null && transition.ExplicitStoryboardCompleted == true)
				if(transition.Storyboard != null && transition.GetExplicitStoryboardCompleted() == true)
				{
					EventHandler<Object> transitionCompleted = null;
					transitionCompleted = new EventHandler<Object>(delegate(object sender, Object e)
					{
						if(transition.GetDynamicStoryboardCompleted() &&

								// If the element or control is removed from the tree, then the new
							// storyboards will not be able to resolve target names. Thus,
							// if the element or control is unloaded, don't start the new
							// storyboards.
							//(element.IsLoaded && (control == null || control.IsLoaded)))
								(element.Parent != null && (control == null || control.Parent != null)))
						{
							group.StartNewThenStopOld(element, state.Storyboard);
						}

						//group.RaiseCurrentStateChanged(element, lastState, state, control);
						transition.Storyboard.Completed -= transitionCompleted;
						transition.SetExplicitStoryboardCompleted(true);
					});

					// hook up explicit storyboard's Completed event handler
					transition.SetExplicitStoryboardCompleted(false);
					transition.Storyboard.Completed += transitionCompleted;
				}

				// Start transition and dynamicTransition Storyboards
				// Stop any previously running Storyboards
				group.StartNewThenStopOld(element, transition.Storyboard, dynamicTransition);
				//group.RaiseCurrentStateChanging(element, lastState, state, control);
			}

			//group.CurrentState = state;
			group.SetCurrentState(state);

			return true;
		}

		private bool GoToStateInternal(FrameworkElement stateGroupsRoot, string stateName, bool useTransitions)
		{
			var _group = default(VisualStateGroup);
			var _state = default(VisualState);

			if(TryGetState(stateGroupsRoot, stateName, out _group, out _state))
			{
				return GoToStateInternal(default(Control), stateGroupsRoot, _group, _state, useTransitions);
			}
			else
			{
				return false;
			}
		}

		private bool TryGetState(FrameworkElement element, string stateName, out VisualStateGroup group, out VisualState state)
		{
			group = default(VisualStateGroup);
			state = default(VisualState);

			// find
			var _groups = VisualStateManager.GetVisualStateGroups(element);
			foreach(VisualStateGroup _visualStateGroup in _groups)
			{
				var _states = _visualStateGroup.States;
				foreach(VisualState _visualState in _states)
				{
					if(_visualState.Name == stateName)
					{
						group = _visualStateGroup;
						state = _visualState;
						return true;
					}
				}
			}

			// all else
			return false;
		}

		private static bool IsDefaultTransition(VisualTransition transition)
		{
			return (transition.From == null && transition.To == null);
		}
	}
}