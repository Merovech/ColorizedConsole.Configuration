﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ColorizedConsole.Configuration
{
	public class ColorSettings
	{
		/// <summary>
		/// The color for the Debug methods (WriteDebug, WriteDebugLine).  Defaults to ColorConsole.Yellow.
		/// </summary>
		[JsonPropertyName("debugColor")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ConsoleColor DebugColor { get; set; } = Defaults.DebugColor;

		/// <summary>
		/// The color for the Error methods (WriteError, WriteErrorLine).  Defaults to ConsoleColor.Red.
		/// </summary>
		[JsonPropertyName("errorColor")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ConsoleColor ErrorColor { get; set; } = Defaults.ErrorColor;

		/// <summary>
		/// The color for the Info methods (WriteInfo, WriteInfoLine).  Defaults to ConsoleColor.Green.
		/// </summary>
		[JsonPropertyName("infoColor")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ConsoleColor InfoColor { get; set; } = Defaults.InfoColor;
	}
}
