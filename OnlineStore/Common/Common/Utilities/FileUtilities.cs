namespace Common.Utilities
{
    public class FileUtilities
    {
        public static string FileFormat(string fileName)
        {
            string result = string.Empty;
            if (!fileName.Contains('.'))
                return result;

            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName[i] == '.')
                    break;

                result = fileName[i] + result;
            }

            return result.ToLower();
        }

    }
}
