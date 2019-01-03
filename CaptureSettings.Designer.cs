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
	partial class CaptureSettings
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
			this.tempDir_grp = new System.Windows.Forms.GroupBox();
			this.deleteTempFiles_btn = new System.Windows.Forms.Button();
			this.viewTempFiles_btn = new System.Windows.Forms.Button();
			this.keepTempFiles_cb = new System.Windows.Forms.CheckBox();
			this.tempDir_btn = new System.Windows.Forms.Button();
			this.tempDir_edit = new System.Windows.Forms.TextBox();
			this.ok_btn = new System.Windows.Forms.Button();
			this.cancel_btn = new System.Windows.Forms.Button();
			this.confirmations_grp = new System.Windows.Forms.GroupBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tempDir_grp.SuspendLayout();
			this.confirmations_grp.SuspendLayout();
			this.SuspendLayout();
			//
			// tempDir_grp
			//
			this.tempDir_grp.Controls.Add(this.deleteTempFiles_btn);
			this.tempDir_grp.Controls.Add(this.viewTempFiles_btn);
			this.tempDir_grp.Controls.Add(this.keepTempFiles_cb);
			this.tempDir_grp.Controls.Add(this.tempDir_btn);
			this.tempDir_grp.Controls.Add(this.tempDir_edit);
			this.tempDir_grp.Location = new System.Drawing.Point(13, 13);
			this.tempDir_grp.Name = "tempDir_grp";
			this.tempDir_grp.Size = new System.Drawing.Size(303, 103);
			this.tempDir_grp.TabIndex = 0;
			this.tempDir_grp.TabStop = false;
			this.tempDir_grp.Text = "&Temporary Directory";
			//
			// deleteTempFiles_btn
			//
			this.deleteTempFiles_btn.Location = new System.Drawing.Point(137, 74);
			this.deleteTempFiles_btn.Name = "deleteTempFiles_btn";
			this.deleteTempFiles_btn.Size = new System.Drawing.Size(75, 23);
			this.deleteTempFiles_btn.TabIndex = 6;
			this.deleteTempFiles_btn.Text = "&Delete Files";
			this.deleteTempFiles_btn.UseVisualStyleBackColor = true;
			this.deleteTempFiles_btn.Click += new System.EventHandler(this.deleteTempFiles_Click);
			//
			// viewTempFiles_btn
			//
			this.viewTempFiles_btn.Location = new System.Drawing.Point(218, 74);
			this.viewTempFiles_btn.Name = "viewTempFiles_btn";
			this.viewTempFiles_btn.Size = new System.Drawing.Size(75, 23);
			this.viewTempFiles_btn.TabIndex = 5;
			this.viewTempFiles_btn.Text = "&View Files";
			this.viewTempFiles_btn.UseVisualStyleBackColor = true;
			this.viewTempFiles_btn.Click += new System.EventHandler(this.viewTempFiles_Click);
			//
			// keepTempFiles_cb
			//
			this.keepTempFiles_cb.AutoSize = true;
			this.keepTempFiles_cb.Location = new System.Drawing.Point(6, 46);
			this.keepTempFiles_cb.Name = "keepTempFiles_cb";
			this.keepTempFiles_cb.Size = new System.Drawing.Size(121, 17);
			this.keepTempFiles_cb.TabIndex = 2;
			this.keepTempFiles_cb.Text = "Keep temporary files";
			this.keepTempFiles_cb.UseVisualStyleBackColor = true;
			//
			// tempDir_btn
			//
			this.tempDir_btn.Location = new System.Drawing.Point(270, 20);
			this.tempDir_btn.Name = "tempDir_btn";
			this.tempDir_btn.Size = new System.Drawing.Size(23, 23);
			this.tempDir_btn.TabIndex = 1;
			this.tempDir_btn.Text = "...";
			this.tempDir_btn.UseVisualStyleBackColor = true;
			this.tempDir_btn.Click += new System.EventHandler(this.tempDir_Click);
			//
			// tempDir_edit
			//
			this.tempDir_edit.Location = new System.Drawing.Point(7, 20);
			this.tempDir_edit.Name = "tempDir_edit";
			this.tempDir_edit.Size = new System.Drawing.Size(257, 20);
			this.tempDir_edit.TabIndex = 0;
			//
			// ok_btn
			//
			this.ok_btn.Location = new System.Drawing.Point(156, 308);
			this.ok_btn.Name = "ok_btn";
			this.ok_btn.Size = new System.Drawing.Size(75, 23);
			this.ok_btn.TabIndex = 3;
			this.ok_btn.Text = "OK";
			this.ok_btn.UseVisualStyleBackColor = true;
			this.ok_btn.Click += new System.EventHandler(this.ok_Click);
			//
			// cancel_btn
			//
			this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel_btn.Location = new System.Drawing.Point(237, 308);
			this.cancel_btn.Name = "cancel_btn";
			this.cancel_btn.Size = new System.Drawing.Size(75, 23);
			this.cancel_btn.TabIndex = 4;
			this.cancel_btn.Text = "Cancel";
			this.cancel_btn.UseVisualStyleBackColor = true;
			this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
			//
			// confirmations_grp
			//
			this.confirmations_grp.Controls.Add(this.checkBox2);
			this.confirmations_grp.Controls.Add(this.checkBox1);
			this.confirmations_grp.Location = new System.Drawing.Point(13, 224);
			this.confirmations_grp.Name = "confirmations_grp";
			this.confirmations_grp.Size = new System.Drawing.Size(299, 78);
			this.confirmations_grp.TabIndex = 4;
			this.confirmations_grp.TabStop = false;
			this.confirmations_grp.Text = "Confirmations";
			//
			// checkBox2
			//
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(10, 43);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(111, 17);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Sound on capture";
			this.checkBox2.UseVisualStyleBackColor = true;
			//
			// checkBox1
			//
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(10, 20);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(129, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Prompt before posting";
			this.checkBox1.UseVisualStyleBackColor = true;
			//
			// CaptureSettings
			//
			this.AcceptButton = this.ok_btn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel_btn;
			this.ClientSize = new System.Drawing.Size(328, 343);
			this.Controls.Add(this.confirmations_grp);
			this.Controls.Add(this.cancel_btn);
			this.Controls.Add(this.ok_btn);
			this.Controls.Add(this.tempDir_grp);
			this.Name = "CaptureSettings";
			this.Text = "Settings";
			this.tempDir_grp.ResumeLayout(false);
			this.tempDir_grp.PerformLayout();
			this.confirmations_grp.ResumeLayout(false);
			this.confirmations_grp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox tempDir_grp;
		private System.Windows.Forms.Button tempDir_btn;
		private System.Windows.Forms.TextBox tempDir_edit;
		private System.Windows.Forms.Button ok_btn;
		private System.Windows.Forms.Button cancel_btn;
		private System.Windows.Forms.GroupBox confirmations_grp;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox keepTempFiles_cb;
		private System.Windows.Forms.Button deleteTempFiles_btn;
		private System.Windows.Forms.Button viewTempFiles_btn;
	}
}