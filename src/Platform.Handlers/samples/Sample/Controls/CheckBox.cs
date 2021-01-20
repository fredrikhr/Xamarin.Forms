using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class CheckBox : Xamarin.Forms.View, ICheck
	{
		public bool IsChecked { get; set; }

		public Color Color { get; set; }
	}
}