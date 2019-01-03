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

namespace CaptureScreenImage
{
	partial class Capture
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
				_keyboard.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Capture));
			this.notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.settings_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.capture_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.help_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.about_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exit_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.captureScreen_btn = new System.Windows.Forms.Button();
			this.captureVideo_btn = new System.Windows.Forms.Button();
			this.stopVideoCap_btn = new System.Windows.Forms.Button();
			this.crosshair_btn = new System.Windows.Forms.Button();
			this.dialog_btn = new System.Windows.Forms.Button();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			//
			// notify
			//
			this.notify.ContextMenuStrip = this.contextMenu;
			this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
			this.notify.Text = "Capture Screen Image";
			this.notify.Visible = true;
			this.notify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseClick);
			//
			// contextMenu
			//
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settings_MenuItem,
            this.capture_MenuItem,
            this.help_MenuItem,
            this.about_MenuItem,
            this.toolStripSeparator1,
            this.exit_MenuItem});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.Size = new System.Drawing.Size(120, 120);
			this.contextMenu.MouseLeave += new System.EventHandler(this.contextMenu_MouseLeave);
			//
			// settings_MenuItem
			//
			this.settings_MenuItem.Name = "settings_MenuItem";
			this.settings_MenuItem.Size = new System.Drawing.Size(119, 22);
			this.settings_MenuItem.Text = "&Settings";
			this.settings_MenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
			//
			// capture_MenuItem
			//
			this.capture_MenuItem.Name = "capture_MenuItem";
			this.capture_MenuItem.Size = new System.Drawing.Size(119, 22);
			this.capture_MenuItem.Text = "&Capture!";
			this.capture_MenuItem.Click += new System.EventHandler(this.captureMenuItem_Click);
			//
			// help_MenuItem
			//
			this.help_MenuItem.Name = "help_MenuItem";
			this.help_MenuItem.Size = new System.Drawing.Size(119, 22);
			this.help_MenuItem.Text = "&Help";
			this.help_MenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
			//
			// about_MenuItem
			//
			this.about_MenuItem.Name = "about_MenuItem";
			this.about_MenuItem.Size = new System.Drawing.Size(119, 22);
			this.about_MenuItem.Text = "&About";
			this.about_MenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(116, 6);
			//
			// exit_MenuItem
			//
			this.exit_MenuItem.Name = "exit_MenuItem";
			this.exit_MenuItem.Size = new System.Drawing.Size(119, 22);
			this.exit_MenuItem.Text = "E&xit";
			this.exit_MenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			//
			// captureScreen_btn
			//
			this.captureScreen_btn.Location = new System.Drawing.Point(94, 9);
			this.captureScreen_btn.Name = "captureScreen_btn";
			this.captureScreen_btn.Size = new System.Drawing.Size(108, 23);
			this.captureScreen_btn.TabIndex = 0;
			this.captureScreen_btn.Text = "Capture Screen";
			this.captureScreen_btn.UseVisualStyleBackColor = true;
			this.captureScreen_btn.Click += new System.EventHandler(this.captureScreen_Click);
			//
			// captureVideo_btn
			//
			this.captureVideo_btn.Location = new System.Drawing.Point(94, 38);
			this.captureVideo_btn.Name = "captureVideo_btn";
			this.captureVideo_btn.Size = new System.Drawing.Size(108, 23);
			this.captureVideo_btn.TabIndex = 1;
			this.captureVideo_btn.Text = "Capture Video";
			this.captureVideo_btn.UseVisualStyleBackColor = true;
			//
			// stopVideoCap_btn
			//
			this.stopVideoCap_btn.Location = new System.Drawing.Point(94, 67);
			this.stopVideoCap_btn.Name = "stopVideoCap_btn";
			this.stopVideoCap_btn.Size = new System.Drawing.Size(108, 23);
			this.stopVideoCap_btn.TabIndex = 2;
			this.stopVideoCap_btn.Text = "Stop VideoCap";
			this.stopVideoCap_btn.UseVisualStyleBackColor = true;
			//
			// crosshair_btn
			//
			this.crosshair_btn.Location = new System.Drawing.Point(94, 96);
			this.crosshair_btn.Name = "crosshair_btn";
			this.crosshair_btn.Size = new System.Drawing.Size(108, 23);
			this.crosshair_btn.TabIndex = 3;
			this.crosshair_btn.Text = "crosshair";
			this.crosshair_btn.UseVisualStyleBackColor = true;
			this.crosshair_btn.Click += new System.EventHandler(this.button4_Click);
			//
			// dialog_btn
			//
			this.dialog_btn.Location = new System.Drawing.Point(94, 125);
			this.dialog_btn.Name = "dialog_btn";
			this.dialog_btn.Size = new System.Drawing.Size(108, 23);
			this.dialog_btn.TabIndex = 4;
			this.dialog_btn.Text = "dialog";
			this.dialog_btn.UseVisualStyleBackColor = true;
			this.dialog_btn.Click += new System.EventHandler(this.button5_Click);
			//
			// Capture
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.captureVideo_btn);
			this.Controls.Add(this.stopVideoCap_btn);
			this.Controls.Add(this.dialog_btn);
			this.Controls.Add(this.crosshair_btn);
			this.Controls.Add(this.captureScreen_btn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Capture";
			this.Text = "Capture Testing";
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notify;
		private System.Windows.Forms.Button captureScreen_btn;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem settings_MenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exit_MenuItem;
		private System.Windows.Forms.ToolStripMenuItem capture_MenuItem;
		private System.Windows.Forms.Button captureVideo_btn;
		private System.Windows.Forms.Button stopVideoCap_btn;
		private System.Windows.Forms.Button crosshair_btn;
		private System.Windows.Forms.Button dialog_btn;
		private System.Windows.Forms.ToolStripMenuItem help_MenuItem;
		private System.Windows.Forms.ToolStripMenuItem about_MenuItem;
	}
}

