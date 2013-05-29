using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Mt.Common.WinRtUiCore.Platform
{
	internal static class ExtendedVisualStateManagerExtenstions
	{
		public readonly static Object DP_UNSET_VALUE = DependencyProperty.UnsetValue;

		public static readonly DependencyProperty CurrentStoryboardsProperty = DependencyProperty.RegisterAttached(
				"CurrentStoryboards", typeof(Collection<Storyboard>), typeof(ExtendedVisualStateManagerExtenstions),
				new PropertyMetadata(DP_UNSET_VALUE));

		public static readonly DependencyProperty CurrentStateProperty = DependencyProperty.RegisterAttached(
				"CurrentState", typeof(VisualState), typeof(ExtendedVisualStateManagerExtenstions),
				new PropertyMetadata(DP_UNSET_VALUE));

		public static readonly DependencyProperty DynamicStoryboardCompletedProperty = DependencyProperty.RegisterAttached(
				"DynamicStoryboardCompleted", typeof(bool), typeof(ExtendedVisualStateManagerExtenstions),
				new PropertyMetadata(false));

		public static readonly DependencyProperty ExplicitStoryboardCompletedProperty = DependencyProperty.RegisterAttached(
				"ExplicitStoryboardCompleted", typeof(bool), typeof(ExtendedVisualStateManagerExtenstions),
				new PropertyMetadata(false));

		public static Collection<Storyboard> GetCurrentStoryboards(this VisualStateGroup group)
		{
			var _currentStoryboards = (Collection<Storyboard>)group.GetValue(CurrentStoryboardsProperty);
			if(_currentStoryboards == null)
			{
				_currentStoryboards = new Collection<Storyboard>();
				group.SetValue(CurrentStoryboardsProperty, _currentStoryboards);
			}

			return _currentStoryboards;
		}

		public static void StartNewThenStopOld(this VisualStateGroup group, FrameworkElement element, params Storyboard[] newStoryboards)
		{
			// Start the new Storyboards
			for(int index = 0; index < newStoryboards.Length; ++index)
			{
				if(newStoryboards[index] == null)
				{
					continue;
				}

				newStoryboards[index].Begin(); //.Begin(element, HandoffBehavior.SnapshotAndReplace, true);

				// Silverlight had an issue where initially, a checked CheckBox would not show the check mark
				// until the second frame. They chose to do a Seek(0) at this point, which this line
				// is supposed to mimic. It does not seem to be equivalent, though, and WPF ends up
				// with some odd animation behavior. I haven't seen the CheckBox issue on WPF, so
				// commenting this out for now.
				// newStoryboards[index].SeekAlignedToLastTick(element, TimeSpan.Zero, TimeSeekOrigin.BeginTime);
			}

			var _currentStoryboards = group.GetCurrentStoryboards();

			// Stop the old Storyboards
			for(int index = 0; index < _currentStoryboards.Count; ++index)
			{
				if(_currentStoryboards[index] == null)
				{
					continue;
				}

				_currentStoryboards[index].Stop(); // .Stop(element);
			}

			// Hold on to the running Storyboards
			_currentStoryboards.Clear();
			for(int index = 0; index < newStoryboards.Length; ++index)
			{
				_currentStoryboards.Add(newStoryboards[index]);
			}
		}

		public static VisualState GetCurrentState(this VisualStateGroup group)
		{
			return (VisualState)group.GetValue(CurrentStateProperty);
		}

		public static void SetCurrentState(this VisualStateGroup group, VisualState state)
		{
			group.SetValue(CurrentStateProperty, state);
		}

		public static bool GetDynamicStoryboardCompleted(this VisualTransition transition)
		{
			var _value = transition.GetValue(DynamicStoryboardCompletedProperty) ?? false;
			return Convert.ToBoolean(_value);
		}

		public static void SetDynamicStoryboardCompleted(this VisualTransition transition, bool dynamicStoryboardCompleted)
		{
			transition.SetValue(DynamicStoryboardCompletedProperty, dynamicStoryboardCompleted);
		}

		public static bool GetExplicitStoryboardCompleted(this VisualTransition transition)
		{
			var _value = transition.GetValue(ExplicitStoryboardCompletedProperty) ?? false;
			return Convert.ToBoolean(_value);
		}

		public static void SetExplicitStoryboardCompleted(this VisualTransition transition, bool explicitStoryboardCompleted)
		{
			transition.SetValue(ExplicitStoryboardCompletedProperty, explicitStoryboardCompleted);
		}
	}

}
