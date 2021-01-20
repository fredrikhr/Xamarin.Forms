namespace Xamarin.Platform.Handlers
{
	public partial class CheckBoxHandler
	{
		public static PropertyMapper<ICheck, CheckBoxHandler> CheckBoxMapper = new PropertyMapper<ICheck, CheckBoxHandler>(ViewHandler.ViewMapper)
		{
			[nameof(ICheck.IsChecked)] = MapIsChecked,
			[nameof(ICheck.Color)] = MapColor
		};

		public CheckBoxHandler() : base(CheckBoxMapper)
		{

		}

		public CheckBoxHandler(PropertyMapper mapper) : base(mapper ?? CheckBoxMapper)
		{

		}

		public static void MapIsChecked(CheckBoxHandler handler, ICheck check)
		{
			ViewHandler.CheckParameters(handler, check);
			handler.TypedNativeView?.UpdateIsChecked(check);
		}

		public static void MapColor(CheckBoxHandler handler, ICheck check)
		{
			ViewHandler.CheckParameters(handler, check);
			handler.TypedNativeView?.UpdateColor(check);
		}
	}
}