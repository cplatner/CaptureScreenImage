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
using System.IO;
using System.Reflection;
using CaptureScreenImage.Properties;

namespace CaptureScreenImage
{
	public class Utils
	{
		public static bool Exec(string commandline)
		{
			string[] parts = commandline.Split(' ', '\t');

			string command = parts[0];
			string args = parts.Length >= 2 ? commandline.Substring(command.Length) : string.Empty;
			args = args.Trim();

			return Exec(command, args);
		}

		public static bool Exec(string command, string arguments)
		{
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = command;
			psi.Arguments = arguments;
			psi.WorkingDirectory = Path.GetDirectoryName(command);

			//* Run as Admin so that the app can access all parts of the OS.
			psi.UseShellExecute = false;
			psi.Verb = "runas";

			Process process = new Process();
			process.StartInfo = psi;
			//StreamReader reader = process.StandardOutput;
			bool didStart = process.Start();
			//output = reader.ReadToEnd();

			return didStart;
		}

		public static string ProgramDirectory()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		public static void DeleteFiles(string directory)
		{
			if (Directory.Exists(directory)) {
				DirectoryInfo di = new DirectoryInfo(directory);
				FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
				foreach (FileInfo fi in files) {
					try {
						fi.Delete();
					}
					catch (Exception e) {
						Logger.Log(e);
					}
				}
			}
		}

		/// <summary>Get an image name with a timestamp
		/// </summary>
		public static string GetDefaultImageName()
		{
			DateTime now = DateTime.Now;
			string timestamp = now.ToString("yyyy-MM-dd-HH-mm-ss");

			string name = Path.Combine(Settings.Default.TempDirectory, "region-" + timestamp + ".png");

			return name;
		}

	}
}
