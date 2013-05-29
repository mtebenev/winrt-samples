using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mt.Common.WinRtUiCore.MvvmCore
{
	/// <summary>
	/// Helper for determining when the DataContext has changed.
	/// Original idea by Jessy Liberty
	/// </summary>
	public static class DataContextChangedHelper<T> where T : DependencyObject, IDataContextChangedHandler<T>
	{
		private const string InternalContext = "InternalDataContext";

		public static readonly DependencyProperty InternalDataContextProperty =
			DependencyProperty.Register(InternalContext, typeof(object), typeof(T), new PropertyMetadata(DependencyProperty.UnsetValue, DataContextChanged));

		private static void DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			T control = (T)sender;
			control.DataContextChanged(control, e);
		}

		public static void Bind(T control)
		{
			BindingOperations.SetBinding(control, InternalDataContextProperty, new Binding());
		}
	}

	public interface IDataContextChangedHandler<T> where T : DependencyObject
	{
		void DataContextChanged(T sender, DependencyPropertyChangedEventArgs e);
	}
}
