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
using System.Runtime.InteropServices;
using System.Text;

namespace CaptureScreenImage
{
	/// <summary>Pretty sure I got these from the ever-useful http://pinvoke.net/
	/// </summary>
	public static class NativeMethods
	{
		[DllImport("user32.dll")]
		internal static extern bool EnumWindows(EnumWindowsProc enumFunc, IntPtr lParam);

		public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		internal static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		internal static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetShellWindow();

		//[DllImport("user32.dll")]
		//internal static extern IntPtr WindowFromPoint(POINT Point);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

		public enum GetWindow_Cmd : uint
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
		public static bool GetWindowRect(IntPtr hWnd, out Rectangle rect)
		{
			RECT lpRect;
			rect = Rectangle.Empty;
			bool retval = GetWindowRect(hWnd, out lpRect);
			if (retval) {
				rect.X = lpRect.Left;
				rect.Y = lpRect.Top;
				rect.Width = lpRect.Right - lpRect.Left;
				rect.Height = lpRect.Bottom - lpRect.Top;
			}
			return retval;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out POINT lpPoint);
		public static bool GetCursorPos(out Point p)
		{
			POINT lpPoint;
			p = Point.Empty;
			bool retval = GetCursorPos(out lpPoint);
			if (retval) {
				p.X = lpPoint.X;
				p.Y = lpPoint.Y;
			}
			return retval;
		}

		[DllImport("user32.dll")]
		internal static extern IntPtr GetDesktopWindow();

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			public static implicit operator Point(POINT p)
			{
				return new Point(p.X, p.Y);
			}

			public static implicit operator POINT(Point p)
			{
				return new POINT(p.X, p.Y);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[DllImport("shell32.dll")]
		internal static extern void SHChangeNotify(HChangeNotifyEventID wEventId,
										   HChangeNotifyFlags uFlags,
										   IntPtr dwItem1,
										   IntPtr dwItem2);


		#region enum HChangeNotifyEventID
		/// <summary>
		/// Describes the event that has occurred.
		/// Typically, only one event is specified at a time.
		/// If more than one event is specified, the values contained
		/// in the <i>dwItem1</i> and <i>dwItem2</i>
		/// parameters must be the same, respectively, for all specified events.
		/// This parameter can be one or more of the following values.
		/// </summary>
		/// <remarks>
		/// <para><b>Windows NT/2000/XP:</b> <i>dwItem2</i> contains the index
		/// in the system image list that has changed.
		/// <i>dwItem1</i> is not used and should be <see langword="null"/>.</para>
		/// <para><b>Windows 95/98:</b> <i>dwItem1</i> contains the index
		/// in the system image list that has changed.
		/// <i>dwItem2</i> is not used and should be <see langword="null"/>.</para>
		/// </remarks>
		[Flags]
		public enum HChangeNotifyEventID
		{
			/// <summary>
			/// All events have occurred.
			/// </summary>
			SHCNE_ALLEVENTS = 0x7FFFFFFF,

			/// <summary>
			/// A file type association has changed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
			/// must be specified in the <i>uFlags</i> parameter.
			/// <i>dwItem1</i> and <i>dwItem2</i> are not used and must be <see langword="null"/>.
			/// </summary>
			SHCNE_ASSOCCHANGED = 0x08000000,

			/// <summary>
			/// The attributes of an item or folder have changed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the item or folder that has changed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_ATTRIBUTES = 0x00000800,

			/// <summary>
			/// A nonfolder item has been created.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the item that was created.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_CREATE = 0x00000002,

			/// <summary>
			/// A nonfolder item has been deleted.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the item that was deleted.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_DELETE = 0x00000004,

			/// <summary>
			/// A drive has been added.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive that was added.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_DRIVEADD = 0x00000100,

			/// <summary>
			/// A drive has been added and the Shell should create a new window for the drive.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive that was added.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_DRIVEADDGUI = 0x00010000,

			/// <summary>
			/// A drive has been removed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive that was removed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_DRIVEREMOVED = 0x00000080,

			/// <summary>
			/// Not currently used.
			/// </summary>
			SHCNE_EXTENDED_EVENT = 0x04000000,

			/// <summary>
			/// The amount of free space on a drive has changed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive on which the free space changed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_FREESPACE = 0x00040000,

			/// <summary>
			/// Storage media has been inserted into a drive.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive that contains the new media.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_MEDIAINSERTED = 0x00000020,

			/// <summary>
			/// Storage media has been removed from a drive.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the root of the drive from which the media was removed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_MEDIAREMOVED = 0x00000040,

			/// <summary>
			/// A folder has been created. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
			/// or <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the folder that was created.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_MKDIR = 0x00000008,

			/// <summary>
			/// A folder on the local computer is being shared via the network.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the folder that is being shared.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_NETSHARE = 0x00000200,

			/// <summary>
			/// A folder on the local computer is no longer being shared via the network.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the folder that is no longer being shared.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_NETUNSHARE = 0x00000400,

			/// <summary>
			/// The name of a folder has changed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the previous pointer to an item identifier list (PIDL) or name of the folder.
			/// <i>dwItem2</i> contains the new PIDL or name of the folder.
			/// </summary>
			SHCNE_RENAMEFOLDER = 0x00020000,

			/// <summary>
			/// The name of a nonfolder item has changed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the previous PIDL or name of the item.
			/// <i>dwItem2</i> contains the new PIDL or name of the item.
			/// </summary>
			SHCNE_RENAMEITEM = 0x00000001,

			/// <summary>
			/// A folder has been removed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the folder that was removed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_RMDIR = 0x00000010,

			/// <summary>
			/// The computer has disconnected from a server.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the server from which the computer was disconnected.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// </summary>
			SHCNE_SERVERDISCONNECT = 0x00004000,

			/// <summary>
			/// The contents of an existing folder have changed,
			/// but the folder still exists and has not been renamed.
			/// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
			/// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
			/// <i>dwItem1</i> contains the folder that has changed.
			/// <i>dwItem2</i> is not used and should be <see langword="null"/>.
			/// If a folder has been created, deleted, or renamed, use SHCNE_MKDIR, SHCNE_RMDIR, or
			/// SHCNE_RENAMEFOLDER, respectively, instead.
			/// </summary>
			SHCNE_UPDATEDIR = 0x00001000,

			/// <summary>
			/// An image in the system image list has changed.
			/// <see cref="HChangeNotifyFlags.SHCNF_DWORD"/> must be specified in <i>uFlags</i>.
			/// </summary>
			SHCNE_UPDATEIMAGE = 0x00008000,

		}
		#endregion // enum HChangeNotifyEventID

		#region public enum HChangeNotifyFlags
		/// <summary>
		/// Flags that indicate the meaning of the <i>dwItem1</i> and <i>dwItem2</i> parameters.
		/// The uFlags parameter must be one of the following values.
		/// </summary>
		[Flags]
		public enum HChangeNotifyFlags
		{
			/// <summary>
			/// The <i>dwItem1</i> and <i>dwItem2</i> parameters are DWORD values.
			/// </summary>
			SHCNF_DWORD = 0x0003,
			/// <summary>
			/// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of ITEMIDLIST structures that
			/// represent the item(s) affected by the change.
			/// Each ITEMIDLIST must be relative to the desktop folder.
			/// </summary>
			SHCNF_IDLIST = 0x0000,
			/// <summary>
			/// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
			/// maximum length MAX_PATH that contain the full path names
			/// of the items affected by the change.
			/// </summary>
			SHCNF_PATHA = 0x0001,
			/// <summary>
			/// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
			/// maximum length MAX_PATH that contain the full path names
			/// of the items affected by the change.
			/// </summary>
			SHCNF_PATHW = 0x0005,
			/// <summary>
			/// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
			/// represent the friendly names of the printer(s) affected by the change.
			/// </summary>
			SHCNF_PRINTERA = 0x0002,
			/// <summary>
			/// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
			/// represent the friendly names of the printer(s) affected by the change.
			/// </summary>
			SHCNF_PRINTERW = 0x0006,
			/// <summary>
			/// The function should not return until the notification
			/// has been delivered to all affected components.
			/// As this flag modifies other data-type flags, it cannot by used by itself.
			/// </summary>
			SHCNF_FLUSH = 0x1000,
			/// <summary>
			/// The function should begin delivering notifications to all affected components
			/// but should return as soon as the notification process has begun.
			/// As this flag modifies other data-type flags, it cannot by used by itself.
			/// </summary>
			SHCNF_FLUSHNOWAIT = 0x2000
		}
		#endregion // enum HChangeNotifyFlags

	}
}
