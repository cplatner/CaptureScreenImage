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
using System.Windows.Forms;
using RawInput;

namespace CaptureScreenImage
{
	public class PrtScnCatcher : IDisposable
	{
		#region data
		/// <summary> static list of keyboard devices </summary>
		private static InputDevice _keyboardDevice = null;

		/// <summary> message pump for capturing keyboard events </summary>
		private KeyboardMessagePump _keyboardMessagePump = null;

		#endregion data

		#region Events

		public class ScreenPrintPressEventArgs : EventArgs
		{
			public ScreenPrintKind Kind { get; private set; }
			public ScreenPrintPressEventArgs(ScreenPrintKind kind)
			{
				Kind = kind;
			}
		}

		/// <summary>Occurs when one of the keys is depressed.</summary>
		public event KeyboardButtonEventHandler ScreenPrintPress;
		public delegate void KeyboardButtonEventHandler(object sender, ScreenPrintPressEventArgs e);
		public enum ScreenPrintKind { FullAuto, TopWindowAuto, Manual };

		#endregion Events

		#region public methods

		/// <summary>Set up a hook to trap keyboard events at a low level</summary>
		public void Initialize()
		{
			if (_keyboardDevice == null)
				_keyboardDevice = new InputDevice(InputDevice.DeviceType.Keyboard);

			if (_keyboardDevice.Count > 0) {
				_keyboardMessagePump = new KeyboardMessagePump();
				_keyboardMessagePump.Show();
				_keyboardDevice.RegisterCallingWindow(_keyboardMessagePump.Handle);
				_keyboardDevice.KeyPressed += new InputDevice.DeviceEventHandler(KeyPressed);
				_keyboardDevice.KeyReleased += new InputDevice.DeviceEventHandler(KeyReleased);
			}
		}

		/// <summary> Uninstall the message pump </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing) {
				if (_keyboardDevice == null)
					return;

				_keyboardDevice.KeyPressed -= new InputDevice.DeviceEventHandler(KeyPressed);
				_keyboardDevice.KeyReleased -= new InputDevice.DeviceEventHandler(KeyReleased);

				if (_keyboardMessagePump != null) {
					_keyboardMessagePump.Dispose();
					_keyboardMessagePump = null;
				}
			}
		}

		#endregion public methods

		#region private methods
		private bool _controlPressed;
		private bool _shiftPressed;

		/// <summary> called on keyboard activity </summary>
		private void KeyPressed(object sender, InputDevice.RawKeyEventArgs e)
		{
			if (ScreenPrintPress != null) {
				if (e.vKey == Keys.PrintScreen
					&& _shiftPressed && _controlPressed) {
					//Console.WriteLine("KeyDn: " + e.vKey);
					ScreenPrintPress.Invoke(this, new ScreenPrintPressEventArgs(ScreenPrintKind.Manual));
				}
				else if (e.vKey == Keys.PrintScreen && _shiftPressed) {
					//Console.WriteLine("KeyDn: " + e.vKey);
					ScreenPrintPress.Invoke(this, new ScreenPrintPressEventArgs(ScreenPrintKind.FullAuto));
				}
				else if (e.vKey == Keys.PrintScreen && _controlPressed) {
					//Console.WriteLine("KeyDn: " + e.vKey);
					ScreenPrintPress.Invoke(this, new ScreenPrintPressEventArgs(ScreenPrintKind.TopWindowAuto));
				}

				if (e.vKey == Keys.ControlKey) {
					//Console.WriteLine("KeyDn: " + e.vKey);
					_controlPressed = true;
				}
				if (e.vKey == Keys.ShiftKey) {
					//Console.WriteLine("KeyDn: " + e.vKey);
					_shiftPressed = true;
				}
			}
		}

		/// <summary> called upon keyboard activity </summary>
		private void KeyReleased(object sender, InputDevice.RawKeyEventArgs e)
		{
			if (ScreenPrintPress != null) {
				if (e.vKey == Keys.ControlKey) {
					//Console.WriteLine("KeyUp: " + e.vKey);
					_controlPressed = false;
				}
				if (e.vKey == Keys.ShiftKey) {
					//Console.WriteLine("KeyUp: " + e.vKey);
					_shiftPressed = false;
				}
			}
		}

		#endregion

		#region MessagePump nested class

		/// <summary>Override the <c>WndProc</c> to provide a place for grabbing input from the keyboard
		/// </summary>
		private class KeyboardMessagePump : Control
		{
			protected override void WndProc(ref Message message)
			{
				_keyboardDevice.ProcessMessage(message);
				base.WndProc(ref message);
			}
		}

		#endregion
	}
}
