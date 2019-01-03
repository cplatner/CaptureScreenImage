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
using System.IO;
using System.Windows.Forms;
using CaptureScreenImage.Properties;

namespace CaptureScreenImage
{
	/// <remarks>
	/// Some fields are not used yet, but are expected to be used in the future.
	/// </remarks>
	public partial class CaptureSettings : Form
	{
		public CaptureSettings()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			tempDir_edit.Text = Settings.Default.TempDirectory;

			keepTempFiles_cb.Visible = false;
			deleteTempFiles_btn.Visible = false;
			viewTempFiles_btn.Visible = false;
			confirmations_grp.Visible = false;
			checkBox1.Visible = false;
			checkBox2.Visible = false;

			base.OnLoad(e);
		}

		private void cancel_btn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void tempDir_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();

			if (Directory.Exists(tempDir_edit.Text)) {
				dialog.SelectedPath = tempDir_edit.Text;
			}
			else {
				dialog.SelectedPath = Path.GetTempPath();
			}

			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK) {
				tempDir_edit.Text = dialog.SelectedPath;
			}
		}

		private void ok_Click(object sender, EventArgs e)
		{
			Settings.Default.TempDirectory = tempDir_edit.Text;
			Settings.Default.Save();
			Close();
		}

		private void deleteTempFiles_Click(object sender, EventArgs e)
		{
			Utils.DeleteFiles(tempDir_edit.Text);
		}

		private void viewTempFiles_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(tempDir_edit.Text)) {
				try {
					Utils.Exec(tempDir_edit.Text);
				}
				catch (Exception ex) {
					Logger.Log(ex);
				}
			}
		}
	}
}
