using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Mt.Common.UiCore.Platform
{
	/// <summary>
	/// Enumerates a visual tree and applies specified action on a certain nodes
	/// </summary>
	public static class VisualTreeEnumerator
	{
		public static void EnumerateSubtree<T>(DependencyObject root, Action<T> callback)
			where T : DependencyObject
		{
			if(root is T)
			{
				T component = (T)((object)root);
				callback(component);
			}

			int childCount = VisualTreeHelper.GetChildrenCount(root);
			for(int i = 0; i < childCount; i++)
			{
				DependencyObject obj = VisualTreeHelper.GetChild(root, i);
				EnumerateSubtree(obj, callback);
			}

			//fix for panels
			//usual enumeration by visual tree doesn't work for elements on tab items
			if(root is ItemsControl)
			{
				ItemsControl itemsControl = root as ItemsControl;
				_EnumerateItemsControl(itemsControl, callback);
			}
			else if(root is ContentControl)
			{
				ContentControl control = (ContentControl)root;
				object contentObject = control.Content;
				if(contentObject is DependencyObject)
					EnumerateSubtree((DependencyObject)contentObject, callback);
			}
			else if(root is Border)
			{
				Border border = (Border)root;
				object content = border.Child;
				if(content is DependencyObject)
					EnumerateSubtree((DependencyObject)content, callback);
			}
		}

		private static void _EnumerateItemsControl<T>(ItemsControl itemsControl, Action<T> callback)
			where T : DependencyObject
		{
			foreach(object item in itemsControl.Items)
			{
				if((null != item) && (item is T))
				{
					T component = (T)(item);
					callback(component);
				}

				ContentControl contentControl = item as ContentControl;
				if(contentControl != null && contentControl.Content != null && contentControl.Content is DependencyObject)
				{
					EnumerateSubtree(contentControl.Content as DependencyObject, callback);
				}
			}
		}

		public static void EnumerateLogicalParents<T>(FrameworkElement node, Action<T> callback)
			where T : FrameworkElement
		{
			DependencyObject parent = GetLogicalParent(node);
			while(parent != null)
			{
				if(parent is T)
					callback((T)parent);

				parent = GetLogicalParent(parent);
			}
		}

		public static void EnumerateLogicalParents<T, TP>(FrameworkElement node, Action<T> callback)
			where T : FrameworkElement
		{
			DependencyObject parent = GetLogicalParent(node);
			while(((parent is TP) == false) && (parent != null))
			{
				if(parent is T)
					callback((T)parent);

				parent = GetLogicalParent(parent);
			}
		}

		public static void EnumerateVisualParents<T>(FrameworkElement node, Action<T> callback)
			where T : FrameworkElement
		{
			DependencyObject parent = GetVisualParent(node);
			while(parent != null)
			{
				if(parent is T)
					callback((T)parent);

				parent = GetVisualParent(parent);
			}
		}

		public static void EnumerateVisualParents<T, TP>(FrameworkElement node, Action<T> callback)
			where T : FrameworkElement
		{
			DependencyObject parent = GetVisualParent(node);
			while(((parent is TP) == false) && (parent != null))
			{
				if(parent is T)
					callback((T)((object)parent));

				parent = GetVisualParent(parent);
			}
		}

		public static DependencyObject FindDescendantByName(DependencyObject element, string name, bool isApplyTemplate)
		{
			if(element is FrameworkElement && (element as FrameworkElement).Name == name)
				return element;

			DependencyObject result = null;

			if(element is Control && isApplyTemplate)
				(element as Control).ApplyTemplate();

			for(int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
			{
				DependencyObject visual = VisualTreeHelper.GetChild(element, i);
				result = FindDescendantByName(visual, name, isApplyTemplate);
				if(result != null)
					break;
			}

			return result;
		}

		public static T FindParentElement<T>(FrameworkElement element)
			where T : FrameworkElement
		{
			T parentElement = null;

			for(var parent = VisualTreeHelper.GetParent(element); parent != null; parent = VisualTreeHelper.GetParent(parent))
			{
				if(parent is T)
				{
					parentElement = (T)parent;
					break;
				}
			}

			return parentElement;
		}

		public static T FindResource<T>(this FrameworkElement element, string key)
		{
			object resourceObj = null;
			for(FrameworkElement ancestor = element; ancestor != null; ancestor = VisualTreeHelper.GetParent(ancestor) as FrameworkElement)
			{
				resourceObj = ancestor.Resources[key];
				if(resourceObj != null)
				{
					break;
				}
			}

			if(resourceObj == null)
				resourceObj = Application.Current.Resources[key];

			T resource = default(T);

			if(resourceObj != null)
				resource = (T) resourceObj;

			return resource;
		}

		private static DependencyObject GetVisualParent(DependencyObject element)
		{
			DependencyObject result = VisualTreeHelper.GetParent(element);
			return result;
		}

		private static DependencyObject GetLogicalParent(DependencyObject node)
		{
			DependencyObject parent = null;

			FrameworkElement frameworkElement = node as FrameworkElement;
			if(frameworkElement != null)
				parent = frameworkElement.Parent;

			return parent;
		}


		/// <summary>
		/// Determines whether the specified element is truly visible on the screen.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="topParent">The top parent element.</param>
		/// <returns>
		///   <c>true</c> if the specified element is visible; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVisible(this FrameworkElement element, FrameworkElement topParent = null)
		{
			while(element != topParent && element != null)
			{
				if(element.Visibility == Visibility.Collapsed)
					return false;

				element = VisualTreeHelper.GetParent(element) as FrameworkElement;
			}

			return true;
		}
	}
}