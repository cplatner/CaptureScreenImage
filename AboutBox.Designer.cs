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
	partial class AboutBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
			this.logoPictureBox = new System.Windows.Forms.PictureBox();
			this.okButton = new System.Windows.Forms.Button();
			this.productName_lb = new System.Windows.Forms.Label();
			this.copyright_lb = new System.Windows.Forms.Label();
			this.companyName_llb = new System.Windows.Forms.LinkLabel();
			this.description_lb = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
			this.SuspendLayout();
			//
			// logoPictureBox
			//
			this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
			this.logoPictureBox.Location = new System.Drawing.Point(8, 8);
			this.logoPictureBox.Name = "logoPictureBox";
			this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
			this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.logoPictureBox.TabIndex = 13;
			this.logoPictureBox.TabStop = false;
			//
			// okButton
			//
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(356, 241);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 25;
			this.okButton.Text = "&OK";
			//
			// productName_lb
			//
			this.productName_lb.AutoSize = true;
			this.productName_lb.Location = new System.Drawing.Point(148, 9);
			this.productName_lb.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.productName_lb.MaximumSize = new System.Drawing.Size(0, 17);
			this.productName_lb.Name = "productName_lb";
			this.productName_lb.Size = new System.Drawing.Size(75, 13);
			this.productName_lb.TabIndex = 26;
			this.productName_lb.Text = "Product Name";
			this.productName_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// copyright_lb
			//
			this.copyright_lb.AutoSize = true;
			this.copyright_lb.Location = new System.Drawing.Point(148, 34);
			this.copyright_lb.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.copyright_lb.MaximumSize = new System.Drawing.Size(0, 17);
			this.copyright_lb.Name = "copyright_lb";
			this.copyright_lb.Size = new System.Drawing.Size(51, 13);
			this.copyright_lb.TabIndex = 27;
			this.copyright_lb.Text = "Copyright";
			this.copyright_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// companyName_llb
			//
			this.companyName_llb.AutoSize = true;
			this.companyName_llb.Location = new System.Drawing.Point(148, 60);
			this.companyName_llb.Name = "companyName_llb";
			this.companyName_llb.Size = new System.Drawing.Size(82, 13);
			this.companyName_llb.TabIndex = 29;
			this.companyName_llb.TabStop = true;
			this.companyName_llb.Text = "Company Name";
			this.companyName_llb.VisitedLinkColor = System.Drawing.Color.Blue;
			this.companyName_llb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.companyName_llb_LinkClicked);
			//
			// description_lb
			//
			this.description_lb.BackColor = System.Drawing.SystemColors.Control;
			this.description_lb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.description_lb.Location = new System.Drawing.Point(151, 93);
			this.description_lb.Multiline = true;
			this.description_lb.Name = "description_lb";
			this.description_lb.Size = new System.Drawing.Size(264, 85);
			this.description_lb.TabIndex = 30;
			this.description_lb.Text = "Quickly and easily capture the whole screen, an active window, or a specific regi" +
    "on using hotkeys.";
			//
			// AboutBox
			//
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(443, 276);
			this.Controls.Add(this.description_lb);
			this.Controls.Add(this.companyName_llb);
			this.Controls.Add(this.copyright_lb);
			this.Controls.Add(this.productName_lb);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.logoPictureBox);
			this.Name = "AboutBox";
			this.Text = "AboutBox";
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label productName_lb;
		private System.Windows.Forms.Label copyright_lb;
		private System.Windows.Forms.LinkLabel companyName_llb;
		private System.Windows.Forms.TextBox description_lb;
	}
}