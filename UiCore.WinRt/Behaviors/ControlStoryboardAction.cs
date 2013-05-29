using System;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Mt.Common.UiCore.Behaviors
{
	// based on Microsoft.Expression.Interactivity.Media.ControlStoryboardAction
	public class ControlStoryboardAction
			: TriggerAction<FrameworkElement>
	{
		public static readonly DependencyProperty StoryboardProperty =
				DependencyProperty.Register("Storyboard", typeof(Storyboard), typeof(ControlStoryboardAction),
				new PropertyMetadata(DependencyProperty.UnsetValue));

		public static readonly DependencyProperty ControlStoryboardOptionProperty =
				DependencyProperty.Register("ControlStoryboardOption", typeof(ControlStoryboardOption), typeof(ControlStoryboardAction),
				new PropertyMetadata(ControlStoryboardOption.Pause));

		public Storyboard Storyboard
		{
			get { return (Storyboard)GetValue(StoryboardProperty); }
			set { SetValue(StoryboardProperty, value); }
		}

		public ControlStoryboardOption ControlStoryboardOption
		{
			get { return (ControlStoryboardOption)GetValue(ControlStoryboardOptionProperty); }
			set { SetValue(ControlStoryboardOptionProperty, value); }
		}

		protected override void Invoke(object type)
		{
			if(this.AssociatedObject == null || this.Storyboard == null) return;

			switch(this.ControlStoryboardOption)
			{
				case ControlStoryboardOption.Play:
					this.Storyboard.Begin();
					break;

				case ControlStoryboardOption.Stop:
					this.Storyboard.Stop();
					break;

				case ControlStoryboardOption.TogglePlayPause:
					ClockState _currentState = ClockState.Stopped;
					try
					{
						_currentState = this.Storyboard.GetCurrentState();
					}
					catch(InvalidOperationException) { }

					if(_currentState == ClockState.Stopped)
					{
						this.Storyboard.Resume();   // or Begin()
						break;
					}
					else
					{
						this.Storyboard.Pause();
						break;
					}

				case ControlStoryboardOption.Pause:
					this.Storyboard.Pause();
					break;

				case ControlStoryboardOption.Resume:
					this.Storyboard.Resume();
					break;

				case ControlStoryboardOption.SkipToFill:
					this.Storyboard.SkipToFill();
					break;
			}
		}
	}
}