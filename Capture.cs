//******************************************************************************
//* Copyright (c) 2012-2013, Chris Platner
//* All rights reserved.
//*
//* Redistribution and use in source and binary forms, with or without
//* modification, are permitted provided that the following conditions are met:
//*     * Redistributions of source code must retain the above copyright
//*       notice, this list of conditions and the following disclaimer.
//*     * Redistributions in binary form must reproduce the above copyright
//*       notice, this list of conditions and the following disclaimer in the
//*       documentation and/or other materials provided with the distribution.
//*     * Neither the name of the author nor the names of its contributors may
//*       be used to endorse or promote products derived from this software
//*       without specific prior written permission.
//*
//* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//* ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//* DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY
//* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//******************************************************************************

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CaptureScreenImage.Properties;

namespace CaptureScreenImage
{
	/// <summary>This is the main form for this application - generally
	/// hidden, but can be shown for debugging and testing purposes.
	/// </summary>
	public partial class Capture : Form
	{
		private PrtScnCatcher _keyboard;

		public Capture()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
			Hide();
			base.OnShown(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			ShowInTaskbar = false;
			WindowState = FormWindowState.Minimized;
			Hide();
			if (Settings.Default == null 
				|| string.IsNullOrEmpty(Settings.Default.TempDirectory)
				|| !Directory.Exists(Settings.Default.TempDirectory)) {
				settingsMenuItem_Click(null, null);
			}

			_keyboard = new PrtScnCatcher();
			_keyboard.Initialize();
			_keyboard.ScreenPrintPress += new PrtScnCatcher.KeyboardButtonEventHandler(keyboard_ScreenPrintPress);

			notify.BalloonTipTitle = "Screen Capture Tool";
			notify.BalloonTipText = "Click to capture the screen";
			notify.ShowBalloonTip(5);
		}

		protected override void OnResize(EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState) {
				Hide();
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			//if (_keyboard != null) {
			//    _keyboard.ScreenPrintPress -= _keyboard_ScreenPrintPress;
			//    _keyboard.Dispose();
			//    _keyboard = null;
			//}
		}

		private void keyboard_ScreenPrintPress(object sender, PrtScnCatcher.ScreenPrintPressEventArgs e)
		{
			switch (e.Kind) {
				case PrtScnCatcher.ScreenPrintKind.Manual:
					ShowManualSelectionForm();
					break;

				case PrtScnCatcher.ScreenPrintKind.FullAuto:
					ScreenCapturer.CaptureWholeScreen(Utils.GetDefaultImageName());
					break;

				case PrtScnCatcher.ScreenPrintKind.TopWindowAuto:
					ScreenCapturer.CaptureCurrentWindow(Utils.GetDefaultImageName());
					break;
			}
		}

		private void captureScreen_Click(object sender, EventArgs e)
		{
			//* Test capture methods
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

			ScreenCapturer.CaptureWholeScreen(
				Path.Combine(Settings.Default.TempDirectory, "wholescreen-" + timestamp + ".jpg"));
			ScreenCapturer.CaptureCurrentWindow(
				Path.Combine(Settings.Default.TempDirectory, "currentwindow-" + timestamp + ".jpg"));
		}


		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void settingsMenuItem_Click(object sender, EventArgs e)
		{
			CaptureSettings settings = new CaptureSettings();
			settings.ShowDialog();
		}

		private void captureMenuItem_Click(object sender, EventArgs e)
		{
			//button1_Click(sender, e);
			button5_Click(sender, e);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Point cursorLocation;
			NativeMethods.GetCursorPos(out cursorLocation);
			Screen screen = Screen.FromPoint(cursorLocation);

			Point h1 = new Point(screen.Bounds.Left, cursorLocation.Y);
			Point h2 = new Point(screen.Bounds.Right, cursorLocation.Y);
			Point v1 = new Point(cursorLocation.X, screen.Bounds.Top);
			Point v2 = new Point(cursorLocation.X, screen.Bounds.Bottom);

			using (Graphics graphics = Graphics.FromHwnd(NativeMethods.GetDesktopWindow())) {
				NativeMethods.SHChangeNotify(NativeMethods.HChangeNotifyEventID.SHCNE_ASSOCCHANGED,
					NativeMethods.HChangeNotifyFlags.SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
				graphics.DrawLine(Pens.Red, h1, h2);
				graphics.DrawLine(Pens.Red, v1, v2);
			}
		}

		private TransparentForm _transparentForm;

		private void ShowManualSelectionForm()
		{
			if (_transparentForm == null || _transparentForm.IsDisposed) {
				_transparentForm = new TransparentForm();
			}

			_transparentForm.Visible = !_transparentForm.Visible;
		}

		private void button5_Click(object sender, EventArgs e)
		{
			ShowManualSelectionForm();
		}

		private void aboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			about.ShowDialog();
		}

		private void helpMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start(Path.Combine(Utils.ProgramDirectory(), "Help.html"));
		}

		private void notify_MouseClick(object sender, MouseEventArgs e)
		{
			contextMenu.Show(Control.MousePosition);
		}

		private void contextMenu_MouseLeave(object sender, EventArgs e)
		{
			contextMenu.Close();
		}

		//ScreenCaptureJob screencap = new ScreenCaptureJob();
		//private void button2_Click(object sender, EventArgs e)
		//{
		//    screencap.OutputPath = Settings.Default.TempDirectory;
		//    screencap.Start();
		//    screencap.CaptureMouseCursor = true;
		//    screencap.ShowFlashingBoundary = true;
		//}

		//private void button3_Click(object sender, EventArgs e)
		//{
		//    screencap.Stop();
		//}
	}
}
