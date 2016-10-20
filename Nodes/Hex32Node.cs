﻿using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace ReClassNET.Nodes
{
	class Hex32Node : BaseHexCommentNode
	{
		[StructLayout(LayoutKind.Explicit)]
		struct UInt32FloatData
		{
			[FieldOffset(0)]
			public int IntValue;

			public IntPtr IntPtr => unchecked((IntPtr)IntValue);

			[FieldOffset(0)]
			public uint UIntValue;

			public UIntPtr UIntPtr => unchecked((UIntPtr)UIntValue);

			[FieldOffset(0)]
			public float FloatValue;
		}

		public override int MemorySize => 4;

		public Hex32Node()
		{
			buffer = new byte[4];
		}

		public override string GetToolTipText(HotSpot spot, Memory memory)
		{
			Contract.Requires(spot != null);
			Contract.Requires(memory != null);

			var value = memory.ReadObject<UInt32FloatData>(Offset);

			return $"Int32: {value.IntValue}\nUInt32: 0x{value.UIntValue:X08}\nFloat: {value.FloatValue:0.000}";
		}

		public override int Draw(ViewInfo view, int x, int y)
		{
			Contract.Requires(view != null);

			return Draw(view, x, y, Program.Settings.ShowText ? view.Memory.ReadPrintableASCIIString(Offset, 4) + "     " : null, 4);
		}

		public override void Update(HotSpot spot)
		{
			Contract.Requires(spot != null);

			Update(spot, 4);
		}

		protected override int AddComment(ViewInfo view, int x, int y)
		{
			Contract.Requires(view != null);

			x = base.AddComment(view, x, y);

			var value = view.Memory.ReadObject<UInt32FloatData>(Offset);

			x = AddComment(view, x, y, value.FloatValue, value.IntPtr, value.UIntPtr);

			return x;
		}
	}
}
