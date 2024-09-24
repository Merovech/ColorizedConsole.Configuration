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

		public static void ApplySettingsFromFile(out Settings settings, string? configFilePath = null)
		{
			if (Settings.TryGetFromFile(out Settings fileSettings, configFilePath))
			{
				_settings = settings;
			}
		}
	}
}
