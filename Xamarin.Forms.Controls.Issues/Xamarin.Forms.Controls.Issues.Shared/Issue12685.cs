﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Shapes;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 12685, "[iOs][Bug] TapGestureRecognizer in Path does not work on iOS", PlatformAffected.iOS)]
#if UITEST
	[NUnit.Framework.Category(Core.UITests.UITestCategories.Github10000)]
	[NUnit.Framework.Category(UITestCategories.Shape)]
#endif
	public partial class Issue12685 : TestContentPage
	{
		const string ResetStatus = "Path touch event not fired, touch path above.";
		const string ClickedStatus = "Path was clicked, click reset button to start over.";

		protected override void Init()
		{
			var layout = new StackLayout();
			var statusLabel = new Label
			{
				AutomationId = "LabelValue",
				Text = ResetStatus,
			};

			var lgb = new LinearGradientBrush();
			lgb.GradientStops.Add(new GradientStop(Color.White, 0));
			lgb.GradientStops.Add(new GradientStop(Color.Orange, 1));

			var pathGeometry = new PathGeometry();
			PathFigureCollectionConverter.ParseStringToPathFigureCollection(pathGeometry.Figures, "M0,0 V300 H300 V-300 Z");

			var path = new Path
			{
				Data = pathGeometry,
				Fill = lgb
			};

			var touch = new TapGestureRecognizer
			{
				Command = new Command(_ => statusLabel.Text = ClickedStatus),
			};
			path.GestureRecognizers.Add(touch);

			var resetButton = new Button
			{
				Text = "Reset",
				Command = new Command(_ => statusLabel.Text = ResetStatus),
			};

			layout.Children.Add(path);
			layout.Children.Add(statusLabel);
			layout.Children.Add(resetButton);

			Content = layout;
		}

#if UITEST
		[Test]
		public void ShapesPathReceiveGestureRecognizers()
		{
			var testLabel = RunningApp.WaitForFirstElement("LabelValue");
			Assert.AreEqual(ResetStatus, testLabel.ReadText());
			var pathRect = testLabel.Rect;
			RunningApp.TapCoordinates(pathRect.X + 100, pathRect.Y-100);
			Assert.AreEqual(ClickedStatus, RunningApp.WaitForFirstElement("LabelValue").ReadText());
		}
#endif
	}
}