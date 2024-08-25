using System.Runtime.CompilerServices;
namespace Logging
{
    public class DebugHelper
    {
        // Log vị trí gọi hàm này bên trong các hàm khác (caller).
        public static void LogCaller(
              [CallerLineNumber] int line = 0
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string filePath = ""
        )
        {
            // Thay bằng bất cứ API log nào khác
            UnityEngine.Debug.Log($"{line} :: {memberName} :: {filePath}");
        }
    }
}