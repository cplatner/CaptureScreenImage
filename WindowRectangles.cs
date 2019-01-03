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
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CaptureScreenImage
{
	public class WindowRectangles
	{
		/// <summary>A list of window rectangles.  The topmost window is first
		/// in the list, followed by others in descending Z order.</summary>
		private List<Rectangle> _windows = new List<Rectangle>();

		/// <summary>Returns a dictionary that contains the handle and title of
		/// all the open windows.
		/// </summary>
		/// <returns>
		/// A dictionary that contains the handle and title of all the open windows.
		/// </returns>
		private static IDictionary<IntPtr, string> GetOpenWindows()
		{
			IntPtr shellWindow = NativeMethods.GetShellWindow();
			Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();

			NativeMethods.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (hWnd == shellWindow) {
					return true;
				}
				if (!NativeMethods.IsWindowVisible(hWnd)) {
					return true;
				}

				int titleLength = NativeMethods.GetWindowTextLength(hWnd);
				if (titleLength == 0) {
					return true;
				}

				StringBuilder builder = new StringBuilder(titleLength);
				NativeMethods.GetWindowText(hWnd, builder, titleLength + 1);

				windows[hWnd] = builder.ToString();
				return true;
			}, IntPtr.Zero);

			return windows;
		}

		/// <summary>Capture the locations of all of the open windows on a
		/// given system.
		/// </summary>
		public WindowRectangles()
		{
			foreach (KeyValuePair<IntPtr, string> window in GetOpenWindows()) {
				IntPtr hwnd = window.Key;
				string title = window.Value;

				Logger.Log(string.Format("{0}: {1}", hwnd, title));
				Rectangle r;
				NativeMethods.GetWindowRect(hwnd, out r);
				Logger.Log(r.ToString());
				_windows.Add(r);
			}
		}

		/// <summary>Return the topmost window that contains the point
		/// <code>p</code>, or an empty rectangle if none exists.
		/// </summary>
		public Rectangle GetTopWindowRect(Point p)
		{
			Rectangle rect = Rectangle.Empty;

			foreach (Rectangle r in _windows) {
				if (r.Contains(p)) {
					rect = r;
					Logger.Log(r.ToString());
					break;
				}
			}

			return rect;
		}
	}
}
