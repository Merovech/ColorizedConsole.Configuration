﻿using System.Text.Json;

namespace ColorizedConsole.Configuration
{
	public static class FileExtensions
	{
		private static Settings? _settings;
		private static JsonSerializerOptions _serializerOptions = new()
		{
			WriteIndented = true
		};

		public static Settings? GetSettings(this ConsoleEx console)
		{
			return _settings;
		}

		public static void CreateSettings(this ConsoleEx console)
		{
			_settings = new();
		}

		public static void WriteSettingsToFile(string? filename)
		{
			_ = _settings ?? throw new InvalidOperationException("No available settings to save.");
			filename ??= Defaults.ConfigFileName;

			File.WriteAllText(filename, JsonSerializer.Serialize(_settings, _serializerOptions));
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
				ConsoleEx.DebugColor = _settings.Colors.DebugColor;
				ConsoleEx.ErrorColor = _settings.Colors.ErrorColor;
				ConsoleEx.InfoColor = _settings.Colors.InfoColor;
			}
		}
	}
}
