namespace ConsoleMenu
{
    internal static class Extensions
    {
        internal static string[] GetParameters(this string[] commandTokens)
        {
            var parameterCount = commandTokens.Length > 0 ? commandTokens.Length - 1 : 0;
            var parameters = new string[parameterCount];

            if (parameterCount > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    parameters[i] = commandTokens[i + 1];
                }
            }

            return parameters;
        }
    }
}