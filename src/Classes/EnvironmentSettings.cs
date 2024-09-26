namespace ColorizedConsole.Configuration
{
	public class EnvironmentSettings
	{
		public string DebugVarName { get; set; }
		public string ErrorVarName { get; set; }
		public string InfoVarName { get; set; }

		public EnvironmentSettings()
		{
			DebugVarName = Defaults.DebugEnvironmentVarName;
			ErrorVarName = Defaults.ErrorEnvironmentVarName;
			InfoVarName = Defaults.InfoEnvironmentVarName;
		}
	}
}
