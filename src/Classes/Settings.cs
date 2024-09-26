using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColorizedConsole.Configuration
{
	public class Settings
	{
		private static readonly JsonSerializerOptions _serializerOptions = new()
		{ 
			WriteIndented = true
		};

		[JsonPropertyName("colors")]
		public ColorSettings Colors { get; set; }

		[JsonPropertyName("environment")]
		public EnvironmentSettings Environment { get; set; }

		/// <summary>
		/// Initializes a new instance of the Settings class with default values.
		/// </summary>
		public Settings()
		{
			// Use defaults
			Colors = new();
			Environment = new();
		}

		/// <summary>
		/// Initializes a new instance of the Settings class with customized values.
		/// </summary>
		/// <param name="debugColor">The color for the Debug methods.  If null, uses the default.</param>
		/// <param name="errorColor">The color for the Error methods.  If null, uses the default.</param>
		/// <param name="consoleColor">The color for the Info methods.  If null, uses the default.</param>
		public Settings(ConsoleColor? debugColor, ConsoleColor? errorColor, ConsoleColor? consoleColor) : this()
		{
			Colors.DebugColor = debugColor ?? Defaults.DebugColor;
			Colors.ErrorColor = errorColor ?? Defaults.ErrorColor;
			Colors.InfoColor = consoleColor ?? Defaults.InfoColor;
		}

		/// <summary>
		/// Attempts to get an instance of the Settings class from the config file.
		/// </summary>
		/// <param name="settings">An out parameter representing the Settings instance.</param>
		/// <returns>True if the file exists and can be successfully parsed; otherwise false.</returns>
		/// <remarks>The config file is cc.config.json.</remarks>
		public static bool TryGetFromFile(out Settings settings, string? filename)
		{
			filename ??= Defaults.ConfigFileName;
			settings = new();
			try
			{
				if (!File.Exists(Defaults.ConfigFileName))
				{
					return false;
				}

				using FileStream fileStream = File.OpenRead(filename);
				Settings? parsedSettings = JsonSerializer.Deserialize<Settings>(fileStream);

				if (parsedSettings == null)
				{
					// No valid JSON
					return false;
				}

				settings.Colors.DebugColor = parsedSettings.Colors.DebugColor;
				settings.Colors.ErrorColor = parsedSettings.Colors.ErrorColor;
				settings.Colors.InfoColor = parsedSettings.Colors.InfoColor;

				return true;
			}
			catch (Exception)
			{
				// This could happen in the ConsoleEx constructor, so use Console here instead so the user has at least some idea
				// that something happened.
				Console.WriteLine("Unable to parse cc.config.json due to a parse error.");

				// Do nothing
				return false;
			}
		}

		/// <summary>
		/// Attempts to get an instance of the Settings class from environment variables.
		/// </summary>
		/// <param name="settings">An out parameter representing the Settings instance.</param>
		/// <returns>True if one or more variables exists and can be successfully parsed; otherwise false.</returns>
		public static bool TryGetFromEnvironment(out Settings settings, string? debugVarName, string? errorVarName, string? infoVarName)
		{
			debugVarName ??= Defaults.DebugEnvironmentVarName;
			errorVarName ??= Defaults.ErrorEnvironmentVarName;
			infoVarName ??= Defaults.InfoEnvironmentVarName;

			settings = new();

			// If none of the vars exist, return false.  Otherwise, go ahead and set what we can.
			// If any fail to parse, skip it.
			var debugStr = System.Environment.GetEnvironmentVariable(debugVarName);
			var errorStr = System.Environment.GetEnvironmentVariable(errorVarName);
			var infoStr = System.Environment.GetEnvironmentVariable(infoVarName);

			if (debugStr == null && errorStr == null && infoStr == null)
			{
				return false;
			}

			settings.Colors.DebugColor = Enum.TryParse(debugStr, out ConsoleColor color) ? color : Defaults.DebugColor;
			settings.Colors.ErrorColor = Enum.TryParse(errorStr, out color) ? color : Defaults.DebugColor;
			settings.Colors.InfoColor = Enum.TryParse(infoStr, out color) ? color : Defaults.DebugColor;
			return true;
		}
	}
}
