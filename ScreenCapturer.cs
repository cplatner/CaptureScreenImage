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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CaptureScreenImage
{
	public class ScreenCapturer
	{
		/// <summary>Capture the entire screen to the given file.
		/// </summary>
		/// <remarks>
		/// Get the bounds of the entire screen.  For multiple screens, get the
		/// total width and height
		/// </remarks>
		public static void CaptureWholeScreen(string filename)
		{
			Rectangle bounds = new Rectangle(0, 0, 0, 0);
			foreach (Screen screen in Screen.AllScreens) {
				bounds.Width = Math.Max(bounds.Width, screen.Bounds.Location.X + screen.Bounds.Width);
				bounds.Height = Math.Max(bounds.Height, screen.Bounds.Location.Y + screen.Bounds.Height);
			}

			using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height)) {
				using (Graphics g = Graphics.FromImage(bitmap)) {
					g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
				}
				try {
					bitmap.Save(filename, ImageFormat.Png);
				}
				catch (Exception) {
					//* Eat it
				}
			}
		}

		/// <summary>Capture a rectangle (region) to the given file.
		/// </summary>
		public static void CaptureRegion(string filename, Rectangle rect)
		{
			if (rect == null || rect.Width == 0 || rect.Height == 0) {
				return;
			}

			using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height)) {
				using (Graphics g = Graphics.FromImage(bitmap)) {
					g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
				}
				try {
					bitmap.Save(filename, ImageFormat.Png);
				}
				catch (Exception) {
					//* Eat it
				}
			}
		}

		public static void CaptureRegion(string filename, int x, int y, int width, int height)
		{
			CaptureRegion(filename, new Rectangle(x, y, width, height));
		}

		/// <summary>Capture the current (topmost) window to the given file.
		/// </summary>
		public static void CaptureCurrentWindow(string filename)
		{
			using (Bitmap bitmap = ScreenCapturer.CaptureCurrentWindow()) {
				try {
					bitmap.Save(filename, ImageFormat.Png);
				}
				catch (Exception) {
					//* Eat it
				}
			}
		}

		private static Bitmap CaptureCurrentWindow()
		{
			var foregroundWindowsHandle = GetForegroundWindow();
			var rect = new Rect();
			GetWindowRect(foregroundWindowsHandle, ref rect);
			Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

			var result = new Bitmap(bounds.Width, bounds.Height);

			using (var g = Graphics.FromImage(result)) {
				g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
			}

			return result;
		}

		//* Keep local p/invokes in the same class
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		//* unused - needs to be removed
		private static Bitmap xCaptureScreen()
		{
			Rectangle bounds = Screen.GetBounds(Point.Empty);

			var result = new Bitmap(bounds.Width, bounds.Height);

			using (var g = Graphics.FromImage(result)) {
				g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
			}

			return result;
		}
	}
}
