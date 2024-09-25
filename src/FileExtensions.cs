namespace ColorizedConsole.Configuration
{
	public static class FileExtensions
	{
		private static Settings? _settings;

		public static Settings? GetSettings(this ConsoleEx console)
		{
			return _settings;
		}

		public static void CreateSettings(this ConsoleEx console)
		{
			_settings = new();
		}

		public static bool ApplySettingsFromFile(this ConsoleEx console, string? configFilePath = null)
		{
			if (Settings.TryGetFromFile(out Settings fileSettings, configFilePath))
			{
				_settings = fileSettings;
				ApplySettings();
				return true;
			}

			return false;
		}

		public static bool ApplySettingsFromEnvironment(this ConsoleEx console, string? debugVarName = null, string? errorVarName = null, string? infoVarName = null)
		{
			if (Settings.TryGetFromEnvironment(out Settings settings, debugVarName, errorVarName, infoVarName))
			{
				_settings = settings;
				ApplySettings();
				return true;
			}

			return false;
		}

		private static void ApplySettings()
		{
			if (_settings != null)
			{
				ConsoleEx.DebugColor = _settings.DebugColor;
				ConsoleEx.ErrorColor = _settings.ErrorColor;
				ConsoleEx.InfoColor = _settings.InfoColor;
			}
		}
	}
}
