using System;
using Mt.Common.WinRtUiCore.Platform;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Mt.Common.UiCore.Behaviors
{
	public class GoToStateAction
			: TargetedTriggerAction<FrameworkElement>
	{
		private const string NO_STATE_GROUP = "GoToStateAction's Target has no state groups";
		public readonly static Object DP_UNSET_VALUE = DependencyProperty.UnsetValue;

		public static readonly DependencyProperty StateNameProperty =
				DependencyProperty.Register("StateName", typeof(string), typeof(GoToStateAction),
				new PropertyMetadata(DP_UNSET_VALUE));

		public static readonly DependencyProperty UseTransitionsProperty =
				DependencyProperty.Register("UseTransitions", typeof(bool), typeof(GoToStateAction),
				new PropertyMetadata(true));

		private FrameworkElement _stateTarget;

		public string StateName
		{
			get { return Convert.ToString(GetValue(StateNameProperty)); }
			set
			{
				SetValue(StateNameProperty, value);
			}
		}

		public bool UseTransitions
		{
			get { return Convert.ToBoolean(GetValue(UseTransitionsProperty)); }
			set { SetValue(UseTransitionsProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			if(_stateTarget == null) ResolveStatefulTarget();
		}

		protected override void OnTargetChanged(FrameworkElement oldTarget, FrameworkElement newTarget)
		{
			base.OnTargetChanged(oldTarget, newTarget);
			ResolveStatefulTarget();
		}

		protected override void Invoke(object parameter)
		{
			if(!string.IsNullOrWhiteSpace(this.StateName) && _stateTarget != null)
			{
				Control _control = _stateTarget as Control;
				if(_control != null)
				{
					_control.ApplyTemplate();
					VisualStateManager.GoToState(_control, this.StateName, this.UseTransitions);
				}
				else
				{
					ExtendedVisualStateManager.GoToElementState(_stateTarget, this.StateName, this.UseTransitions);
				}
			}
		}

		// based on logic from Microsoft.Expression.Interactivity.VisualStateUtilities

		private void ResolveStatefulTarget()
		{
			FrameworkElement _resolvedControl = default(FrameworkElement);
			if(string.IsNullOrEmpty(this.TargetName) && (this.Target == null))
			{
				if(!TryFindNearestStatefulControl(this.AssociatedObject, out _resolvedControl) && _resolvedControl != null)
				{
					throw new InvalidOperationException(NO_STATE_GROUP);
				}
			}
			else
			{
				_resolvedControl = this.Target;
			}

			_stateTarget = _resolvedControl;
		}

		private bool TryFindNearestStatefulControl(FrameworkElement contextElement, out FrameworkElement resolvedControl)
		{
			FrameworkElement _targetElement = contextElement;
			if(_targetElement == null)
			{
				resolvedControl = default(FrameworkElement);
				return false;
			}
			else
			{
				FrameworkElement _parentElement = _targetElement.Parent as FrameworkElement;
				bool _flag = true;

				// loop
				for(; !HasVisualStateGroupsDefined(_targetElement) && ShouldContinueTreeWalk(_parentElement); _parentElement = _parentElement.Parent as FrameworkElement)
				{
					_targetElement = _parentElement;
				}

				if(HasVisualStateGroupsDefined(_targetElement))
				{
					FrameworkElement _statefulElement = VisualTreeHelper.GetParent(_targetElement) as FrameworkElement;
					if(_statefulElement != null && _statefulElement is Control)
					{
						_targetElement = _statefulElement;
					}
				}
				else
				{
					_flag = false;
				}

				resolvedControl = _targetElement;
				return _flag;
			}
		}

		private bool HasVisualStateGroupsDefined(FrameworkElement frameworkElement)
		{
			if(frameworkElement != null)
			{
				return VisualStateManager.GetVisualStateGroups(frameworkElement).Count != 0;
			}
			else
			{
				return false;
			}
		}

		private bool ShouldContinueTreeWalk(FrameworkElement element)
		{
			if(element == null || element is UserControl)
			{
				return false;
			}
			if(element.Parent == null)
			{
				var _templatedParent = FindTemplatedParent(element);
				if(_templatedParent == null || !(_templatedParent is Control) && !(_templatedParent is ContentPresenter))
				{
					return false;
				}
			}
			return true;
		}

		private FrameworkElement FindTemplatedParent(FrameworkElement parent)
		{
			return VisualTreeHelper.GetParent(parent) as FrameworkElement;
		}
	}
}
