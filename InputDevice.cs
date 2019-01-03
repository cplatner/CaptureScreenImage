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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RawInput
{
	/// <summary>Handles raw input from keyboard devices.
	/// </summary>
	/// <remarks>
	/// Taken from: http://www.codeproject.com/KB/system/rawinput.aspx.  Also
	/// see http://msdn.microsoft.com/en-us/library/ms996387.aspx
	/// Currently, this only works with keyboard input devices, but it could be
	/// easily modified to work with others.
	/// </remarks>
	public sealed class InputDevice
	{
		#region structs & enums

		/// <summary>An enum representing the different types of input devices.
		/// </summary>
		public enum DeviceType { Unknown = 0, HID, Mouse, Keyboard }

		/// <summary>Class encapsulating the information about a keyboard device
		/// </summary>
		public class DeviceInfo
		{
			public string Name { get; internal set; }
			public DeviceType DType { get; internal set; }
			public string Descriptor { get; internal set; }
			public IntPtr Handle { get; internal set; }

			public override string ToString()
			{
				return Name + ";" + DType + ";" + Descriptor + ";" + Handle;
			}
		}

		/// <summary>Arguments provided by the handler for the KeyPressed event.
		/// </summary>
		/// <remarks>
		/// Need to define some flags so that we can capture the control keys in the app
		/// </remarks>
		public class RawKeyEventArgs : EventArgs
		{
			public DeviceInfo device { get; private set; }
			public Keys vKey { get; private set; }
			public string keyName { get; private set; }

			public RawKeyEventArgs(DeviceInfo dInfo, Keys vKey, string keyName)
			{
				this.device = dInfo;
				this.vKey = vKey;
				this.keyName = keyName;
			}
		}

		#endregion structs & enums

		#region published KeyPressed Event

		/// <summary>The delegate to handle KeyPressed events.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">A set of KeyControlEventArgs information about the
		/// key that was pressed and the device it was on.</param>
		public delegate void DeviceEventHandler(object sender, RawKeyEventArgs e);

		/// <summary>The event raised when InputDevice detects that a key was
		/// pressed.
		/// </summary>
		public event DeviceEventHandler KeyPressed;
		public event DeviceEventHandler KeyReleased;

		#endregion published KeyPressed Event

		#region data members

		/// <summary> List of keyboard devices.
		/// Key: the device handle
		/// Value: the device info class
		/// </summary>
		private Dictionary<IntPtr, DeviceInfo> _DeviceList;

		/// <summary> Count of devices currently being processed
		/// </summary>
		public int Count { get { return _DeviceList.Count; } }

		#endregion data members

		#region Windows.h const definitions

		// The following constants are defined in Windows.h

		/// <summary>If set, this enables the caller to receive the input even
		/// when the caller is not in the foreground. Note that hwndTarget
		/// must be specified. </summary>
		private const int RIDEV_INPUTSINK = 0x00000100;
		private const int RID_INPUT = 0x10000003;

		private const int FAPPCOMMAND_MASK = 0xF000;
		private const int FAPPCOMMAND_MOUSE = 0x8000;
		private const int FAPPCOMMAND_OEM = 0x1000;

		private const int RIM_TYPEMOUSE = 0;
		private const int RIM_TYPEKEYBOARD = 1;
		private const int RIM_TYPEHID = 2;

		private const int RIDI_DEVICENAME = 0x20000007;

		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		private const int WM_SYSKEYDOWN = 0x0104;
		private const int WM_SYSKEYUP = 0x0105;
		private const int WM_INPUT = 0x00FF;
		private const int VK_OEM_CLEAR = 0xFE;
		private const int VK_LAST_KEY = VK_OEM_CLEAR; // this is a made up value used as a sentinel


		#endregion Windows.h const definitions

		#region Windows.h structure declarations

		// The following structures are defined in Windows.h

		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWINPUTDEVICELIST
		{
			public IntPtr hDevice;
			[MarshalAs(UnmanagedType.U4)]
			public int dwType;
		}

		//* Code to make it work for x64 and x86
		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWINPUT
		{
			public RAWINPUTHEADER header;
			public RAWINPUTDATA data;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct RAWINPUTDATA
		{
			[FieldOffset(0)]
			private IntPtr _pad;
			[FieldOffset(0)]
			public RAWMOUSE mouse;
			[FieldOffset(0)]
			public RAWKEYBOARD keyboard;
			[FieldOffset(0)]
			public RAWHID hid;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWINPUTHEADER
		{
			[MarshalAs(UnmanagedType.U4)]
			public int dwType;
			[MarshalAs(UnmanagedType.U4)]
			public int dwSize;
			public IntPtr hDevice;
			public IntPtr wParam;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWHID
		{
			[MarshalAs(UnmanagedType.U4)]
			public int dwSizHid;
			[MarshalAs(UnmanagedType.U4)]
			public int dwCount;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct BUTTONSSTR
		{
			[MarshalAs(UnmanagedType.U2)]
			public ushort usButtonFlags;
			[MarshalAs(UnmanagedType.U2)]
			public ushort usButtonData;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct RAWMOUSE
		{
			[MarshalAs(UnmanagedType.U2)]
			[FieldOffset(0)]
			public ushort usFlags;
			[MarshalAs(UnmanagedType.U4)]
			[FieldOffset(4)]
			public uint ulButtons;
			[FieldOffset(4)]
			public BUTTONSSTR buttonsStr;
			[MarshalAs(UnmanagedType.U4)]
			[FieldOffset(8)]
			public uint ulRawButtons;
			[FieldOffset(12)]
			public int lLastX;
			[FieldOffset(16)]
			public int lLastY;
			[MarshalAs(UnmanagedType.U4)]
			[FieldOffset(20)]
			public uint ulExtraInformation;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWKEYBOARD
		{
			[MarshalAs(UnmanagedType.U2)]
			public ushort MakeCode;
			[MarshalAs(UnmanagedType.U2)]
			public ushort Flags;
			[MarshalAs(UnmanagedType.U2)]
			public ushort Reserved;
			[MarshalAs(UnmanagedType.U2)]
			public ushort VKey;
			[MarshalAs(UnmanagedType.U4)]
			public uint Message;
			[MarshalAs(UnmanagedType.U4)]
			public uint ExtraInformation;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RAWINPUTDEVICE
		{
			[MarshalAs(UnmanagedType.U2)]
			public ushort usUsagePage;
			[MarshalAs(UnmanagedType.U2)]
			public ushort usUsage;
			[MarshalAs(UnmanagedType.U4)]
			public int dwFlags;
			public IntPtr hwndTarget;
		}

		#endregion Windows.h structure declarations

		#region DllImports

		//* These imports are only used in this class - keep them here.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("user32.dll")]
		private extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

		#endregion DllImports

		#region construction and initialization

		/// <summary>InputDevice constructor - builds a list of eligible raw input devices
		/// </summary>
		/// <param name="deviceFilter">device type filter - when present, only process messages from devices of this type</param>
		public InputDevice(DeviceType? deviceFilter)
		{
			EnumerateDevices(deviceFilter, out _DeviceList);
		}

		/// <summary>Registers eligible raw input devices for the calling window.
		/// </summary>
		/// <param name="hwnd">Handle of the window listening for key presses</param>
		public void RegisterCallingWindow(IntPtr hwnd)
		{
			if (_DeviceList.Count < 1)
				throw new ApplicationException("No eligible raw input devices.");

			//Create an array of all the raw input devices we want to
			//listen to. In this case, only keyboard devices.
			//RIDEV_INPUTSINK determines that the window will continue
			//to receive messages even when it doesn't have the focus.
			RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];

			rid[0].usUsagePage = 0x01;
			rid[0].usUsage = 0x06;
			rid[0].dwFlags = RIDEV_INPUTSINK;
			rid[0].hwndTarget = hwnd;

			if (!RegisterRawInputDevices(rid, (uint) rid.Length, (uint) Marshal.SizeOf(rid[0]))) {
				throw new ApplicationException("Failed to register raw input device(s).");
			}
		}

		#endregion construction and initialization

		#region EnumerateDevices()

		/// <summary>Iterates through the list provided by GetRawInputDeviceList,
		/// adding eligible devices to deviceList.
		/// </summary>
		/// <param name="deviceFilter">device type filter - when present, only process messages from devices of this type</param>
		/// <returns>The number of keyboard devices found.</returns>
		private static void EnumerateDevices(DeviceType? deviceFilter, out Dictionary<IntPtr, DeviceInfo> deviceList)
		{
			deviceList = new Dictionary<IntPtr, DeviceInfo>();

			uint deviceCount = 0;

			// Get the number of raw input devices in the list,
			// then allocate sufficient memory and get the entire list
			int dwSize = (Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));
			if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint) dwSize) == 0) {
				IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int) (dwSize * deviceCount));
				GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint) dwSize);

				// Iterate through the list, discarding undesired items
				// and retrieving further information on keyboard devices
				for (int i = 0; i < deviceCount; i++) {
					RAWINPUTDEVICELIST rid = (RAWINPUTDEVICELIST) Marshal.PtrToStructure(
											   new IntPtr((pRawInputDeviceList.ToInt32() + (dwSize * i))),
											   typeof(RAWINPUTDEVICELIST));
					uint pcbSize = 0;
					GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

					if (pcbSize > 0) {
						IntPtr pData = Marshal.AllocHGlobal((int) pcbSize);
						GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, pData, ref pcbSize);

						string deviceDescriptor = (string) Marshal.PtrToStringAnsi(pData);

						// Drop the "root" keyboard and mouse devices used for Terminal
						// Services and the Remote Desktop
						if (deviceDescriptor.ToUpper().Contains("ROOT")) {
							continue;
						}

						// If the device is identified in the list as a keyboard or
						// HID device, create a DeviceInfo object to store information
						// about it
						if (rid.dwType == RIM_TYPEKEYBOARD || rid.dwType == RIM_TYPEHID) {

							DeviceInfo dInfo = new DeviceInfo();

							dInfo.Handle = rid.hDevice;
							dInfo.Descriptor = deviceDescriptor;
							dInfo.DType = GetDeviceType(rid.dwType, deviceDescriptor);

							// Check the Registry to see whether this is actually a
							// keyboard, and to retrieve a more friendly description.
							string deviceDesc;
							string deviceClass;
							bool isKeyboardDevice;

							ReadReg(deviceDescriptor, out deviceDesc, out deviceClass, out isKeyboardDevice);
							dInfo.Name = deviceDesc;

							// If it is a keyboard and it isn't already in the list
							// and matches the device type filter, if provided, then
							// add it to the deviceList.
							if (isKeyboardDevice && !deviceList.ContainsKey(rid.hDevice)) {
								if (deviceFilter == null || deviceFilter.Value == dInfo.DType) {
									deviceList.Add(dInfo.Handle, dInfo);
								}
							}
						}
						Marshal.FreeHGlobal(pData);
					}
				}


				Marshal.FreeHGlobal(pRawInputDeviceList);

			}
			else {
				throw new ApplicationException("An error occurred while retrieving the list of devices.");
			}

		}

		/// <summary>Categorizes a device as to type, given its type code (as returned
		/// by GetRawInputDeviceList) and Device Identification string.
		/// </summary>
		/// <param name="dwType">raw input device type value (RIM_TYPEMOUSE, RIM_TYPEKEYBOARD or RIM_TYPEHID)</param>
		/// <param name="descriptor">device descriptor - example: USB\VID_413C&PID_2003\6&25905D47&0&2</param>
		/// <returns>device type: e.g.: Mouse, Keyboard, generic HID</returns>
		private static DeviceType GetDeviceType(int dwType, string descriptor)
		{
			DeviceType deviceType;
			switch (dwType) {
				case RIM_TYPEMOUSE: deviceType = DeviceType.Mouse; break;
				case RIM_TYPEKEYBOARD: deviceType = DeviceType.Keyboard; break;
				case RIM_TYPEHID: deviceType = DeviceType.HID; break;
				default: deviceType = DeviceType.Unknown; break;
			}

			return deviceType;
		}

		/// <summary>Reads the Registry to retrieve a friendly description
		/// of the device, and determine whether it is a keyboard.
		/// </summary>
		/// <param name="descriptor">device name to search for, as provided by GetRawInputDeviceInfo.</param>
		/// <param name="deviceDesc">device description stored in the Registry entry's DeviceDesc value</param>
		/// <param name="deviceClass">device class stored in the Registry entry's Class value</param>
		/// <param name="isKeyboard">flag indicating whether the device's class is "Keyboard".</param>
		private static void ReadReg(string descriptor, out string deviceDesc, out string deviceClass, out bool isKeyboard)
		{
			// Example Device Identification string
			// @"\??\ACPI#PNP0303#3&13c0b0c5&0#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}";

			// remove the \??\
			string[] split = descriptor.Substring(4).Split('#');

			string id_01 = split[0];    // ACPI (Class code)
			string id_02 = split[1];    // PNP0303 (SubClass code)
			string id_03 = split[2];    // 3&13c0b0c5&0 (Protocol code)
			//The final part is the class GUID and is not needed here

			//Open the appropriate key as read-only so no permissions
			//are needed.
			RegistryKey OurKey = Registry.LocalMachine;

			string findme = string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", id_01, id_02, id_03);

			OurKey = OurKey.OpenSubKey(findme, false);

			//Retrieve the desired information and set isKeyboard
			deviceDesc = (string) OurKey.GetValue("DeviceDesc");
			deviceClass = (string) OurKey.GetValue("Class");
			//* Windows 8 does not define the 'Class' key.
			if (!string.IsNullOrEmpty(deviceClass)) {
				isKeyboard = deviceClass.ToUpper().Equals("KEYBOARD");
			}
			else if (!string.IsNullOrEmpty(deviceDesc)) {
				isKeyboard = deviceDesc.ToUpper().Contains("KEYBOARD");
			}
			else {
				isKeyboard = false;
			}
		}

		#endregion EnumerateDevices()

		#region ProcessMessage

		/// <summary> Filters Windows messages for WM_INPUT messages and calls
		/// ProcessInputCommand if necessary.
		/// </summary>
		/// <param name="message">The Windows message.</param>
		public void ProcessMessage(Message message)
		{
			// Proceed only if there is at least one event subscriber.
			if (KeyPressed == null)
				return;

			switch (message.Msg) {
				case WM_INPUT: {
						ProcessInputCommand(message);
					}
					break;
			}
		}

		/// <summary> Raises a KeyPressed event containing extended information for any
		/// keyboard events that occur.
		/// </summary>
		/// <param name="message">The WM_INPUT message to process.</param>
		private void ProcessInputCommand(Message message)
		{
			// First call to GetRawInputData sets the value of dwSize,
			// which can then be used to allocate the appropriate amount of memory,
			// storing the pointer in "buffer".
			uint dwSize = 0;
			GetRawInputData(message.LParam,
							 RID_INPUT, IntPtr.Zero,
							 ref dwSize,
							 (uint) Marshal.SizeOf(typeof(RAWINPUTHEADER)));

			IntPtr buffer = Marshal.AllocHGlobal((int) dwSize);
			try {
				// Call GetRawInputData again to fill the allocated memory
				// with information about the input
				if (buffer != IntPtr.Zero &&
					GetRawInputData(message.LParam,
									 RID_INPUT,
									 buffer,
									 ref dwSize,
									 (uint) Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize) {

					// Store the message information in "raw", then check
					// that the input comes from a keyboard device before
					// processing it to raise an appropriate KeyPressed event.

					RAWINPUT raw = (RAWINPUT) Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

					if (raw.header.dwType == RIM_TYPEKEYBOARD) {

						// Filter for Key Down events and then retrieve information
						// about the keystroke
						if (raw.data.keyboard.Message == WM_KEYDOWN || raw.data.keyboard.Message == WM_SYSKEYDOWN) {

							// On most keyboards, "extended" keys such as the arrow or
							// page keys return two codes - the key's own code, and an
							// "extended key" flag, which translates to 255. This flag
							// isn't useful to us, so it can be disregarded.
							ushort key = raw.data.keyboard.VKey;
							if (key > VK_LAST_KEY)
								return;

							// Retrieve the name of the key that was pressed.
							Keys vKey;
							string keyName;
							try {
								vKey = (Keys) key;
								keyName = Enum.GetName(typeof(Keys), key);
							}
							catch {
								return;
							}

							// Retrieve information about the device that generated the message.
							DeviceInfo dInfo;
							if (!_DeviceList.TryGetValue(raw.header.hDevice, out dInfo))
								return;

							// If the key that was pressed is valid and there
							// was no problem retrieving information on the device,
							// raise the KeyPressed event.
							if (KeyPressed != null) {
								KeyPressed(this, new RawKeyEventArgs(dInfo, vKey, keyName));
							}
						}
						else if (raw.data.keyboard.Message == WM_KEYUP || raw.data.keyboard.Message == WM_SYSKEYUP) {

							// On most keyboards, "extended" keys such as the arrow or
							// page keys return two codes - the key's own code, and an
							// "extended key" flag, which translates to 255. This flag
							// isn't useful to us, so it can be disregarded.
							ushort key = raw.data.keyboard.VKey;
							if (key > VK_LAST_KEY)
								return;

							// Retrieve the name of the key that was pressed.
							Keys vKey;
							string keyName;
							try {
								vKey = (Keys) key;
								keyName = Enum.GetName(typeof(Keys), key);
							}
							catch {
								return;
							}

							// Retrieve information about the device that generated the message.
							DeviceInfo dInfo;
							if (!_DeviceList.TryGetValue(raw.header.hDevice, out dInfo))
								return;

							// If the key that was released is valid and there
							// was no problem retrieving information on the device,
							// raise the KeyReleased event.
							if (KeyReleased != null) {
								KeyReleased(this, new RawKeyEventArgs(dInfo, vKey, keyName));
							}
						}
					}
				}
			}
			finally {
				Marshal.FreeHGlobal(buffer);
			}
		}

		#endregion ProcessMessage
	}
}
