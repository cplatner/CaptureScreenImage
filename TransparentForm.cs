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
using System.Windows.Forms;

namespace CaptureScreenImage
{
	/// <summary>This form is loaded, and covers the entire screen when the crosshair is shown.
	/// </summary>
	public partial class TransparentForm : Form
	{
		private WindowRectangles _windowRectangles;
		private Pen _crosshairPen;
		private Pen _selectionPen;

		public TransparentForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			//* Get the overall height and width of the screen area
			int height = 0;
			int width = 0;
			foreach (Screen screen in Screen.AllScreens) {
				//* take smallest height
				height = Math.Max(screen.Bounds.Height, height);
				width += screen.Bounds.Width;
			}

			_windowRectangles = new WindowRectangles();

			this.Bounds = new Rectangle(0, 0, width, height);

			_crosshairPen = new Pen(Color.Red, 3);
			_selectionPen = new Pen(Color.Aqua, 1);

			this.BringToFront();
			this.TopMost = true;

			base.OnLoad(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (_crosshairPen != null) {
				_crosshairPen.Dispose();
				//_crosshairPen = null;
			}
			if (_selectionPen != null) {
				_selectionPen.Dispose();
				//_selectionPen = null;
			}

			base.OnFormClosing(e);
		}

		private Point h1;
		private Point h2;
		private Point v1;
		private Point v2;

		private Point _mousePos;

		protected override void OnMouseMove(MouseEventArgs e)
		{
			_mousePos = e.Location;
			h1 = new Point(this.Bounds.Left, e.Y);
			h2 = new Point(this.Bounds.Right, e.Y);
			v1 = new Point(e.X, this.Bounds.Top);
			v2 = new Point(e.X, this.Bounds.Bottom);

			if (_mousedown) {
				_lowerright.X = e.X;
				_lowerright.Y = e.Y;
				Invalidate();
			}
			Invalidate();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				Close();
				e.Handled = true;
			}
			base.OnKeyDown(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			//e.Graphics.DrawLine(_crosshairPen, h1, h2);
			//e.Graphics.DrawLine(_crosshairPen, v1, v2);
			Rectangle rect;

			if (!_mousedown && (rect = _windowRectangles.GetTopWindowRect(_mousePos)) != Rectangle.Empty) {
				//int x = _upperleft.X < _lowerright.X ? _upperleft.X : _lowerright.X;
				//int y = _upperleft.Y < _lowerright.Y ? _upperleft.Y : _lowerright.Y;
				//int width = Math.Abs(_upperleft.X - _lowerright.X);
				//int height = Math.Abs(_upperleft.Y - _lowerright.Y);
				e.Graphics.DrawRectangle(_selectionPen, rect);
				e.Graphics.FillRectangle(Brushes.Blue, rect);
			}


			if (_mousedown) {
				int x = _upperleft.X < _lowerright.X ? _upperleft.X : _lowerright.X;
				int y = _upperleft.Y < _lowerright.Y ? _upperleft.Y : _lowerright.Y;
				int width = Math.Abs(_upperleft.X - _lowerright.X);
				int height = Math.Abs(_upperleft.Y - _lowerright.Y);
				e.Graphics.DrawRectangle(_selectionPen, x, y, width, height);
				e.Graphics.FillRectangle(Brushes.Blue, x + 1, y + 1, width - 2, height - 2);
			}

			e.Graphics.DrawLine(_crosshairPen, h1, h2);
			e.Graphics.DrawLine(_crosshairPen, v1, v2);

		}

		Point _upperleft = new Point();
		Point _lowerright = new Point();
		bool _mousedown = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//* Save the upper left
			if (!_mousedown) {
				_upperleft.X = e.X;
				_upperleft.Y = e.Y;
				_mousedown = true;
				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			//* Testing
			Hide();
			_mousedown = false;
			Invalidate();
			//this.Opacity = 0;
			//DateTime now = DateTime.Now;
			//string timestamp = now.ToString("yyyy-MM-dd-HH-mm-ss");
			if (_upperleft != e.Location) {
				int x = _upperleft.X < _lowerright.X ? _upperleft.X : _lowerright.X;
				int y = _upperleft.Y < _lowerright.Y ? _upperleft.Y : _lowerright.Y;
				int width = Math.Abs(_upperleft.X - _lowerright.X);
				int height = Math.Abs(_upperleft.Y - _lowerright.Y);
				//*

				_upperleft.X = 0;
				_upperleft.Y = 0;
				_lowerright.X = 0;
				_lowerright.Y = 0;

				Close();

				ScreenCapturer.CaptureRegion(
					//Path.Combine(Settings.Default.TempDirectory, "region-" + timestamp + ".jpg"),
					Utils.GetDefaultImageName(),
					x, y, width, height);
			}
			else {
				Rectangle rect = _windowRectangles.GetTopWindowRect(e.Location);

				//*

				_upperleft.X = 0;
				_upperleft.Y = 0;
				_lowerright.X = 0;
				_lowerright.Y = 0;

				Close();

				ScreenCapturer.CaptureRegion(
					//Path.Combine(Settings.Default.TempDirectory, "region-" + timestamp + ".jpg"),
					Utils.GetDefaultImageName(),
					rect.X, rect.Y, rect.Width, rect.Height);
			}
		}
	}
}
