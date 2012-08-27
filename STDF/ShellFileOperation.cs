using System;
using System.Runtime.InteropServices;

namespace ShellLib
{
    public class ShellFileOperation
    {
        public ShellApi.FileOperations Operation;
        public IntPtr OwnerWindow;
        public ShellApi.ShellFileOperationFlags OperationFlags;
        public String ProgressTitle;
        public String[] SourceFiles;
        public String[] DestFiles;

        /// <summary>
        /// Initialize reasonable defaults.
        /// </summary>
        public ShellFileOperation()
        {
            Operation = ShellApi.FileOperations.FO_COPY;
            OwnerWindow = IntPtr.Zero;
            OperationFlags = ShellApi.ShellFileOperationFlags.FOF_ALLOWUNDO
                | ShellApi.ShellFileOperationFlags.FOF_MULTIDESTFILES
                | ShellApi.ShellFileOperationFlags.FOF_NO_CONNECTED_ELEMENTS
                | ShellApi.ShellFileOperationFlags.FOF_WANTNUKEWARNING;
            ProgressTitle = "";
        }

        /// <summary>
        /// Invokes SHFileOperation in managed code. Refer to https://dev.winodws.com or the Windows SDK for more information.
        /// </summary>
        /// <example>
        /// ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();
        /// string[] source = new string[nCount];
        /// string[] dest = new string[nCount];
        /// 
        /// .. add full paths to source and dest ...
        /// 
        /// fo.Operation = ShellLib.ShellApi.FileOperations.FO_COPY;
        /// fo.OwnerWindow = this.Handle;
        /// fo.ProgressTitle = "Copying Checked Files";
        /// fo.SourceFiles = source;
        /// fo.DestFiles = dest;
        /// 
        /// bool RetVal = fo.DoOperation();
        /// </example>
        /// <returns>Indicates if the operation was successful.</returns>
        public bool DoOperation()
        {
            ShellApi.SHFILEOPSTRUCT FileOpStruct = new ShellApi.SHFILEOPSTRUCT();

            FileOpStruct.hwnd = OwnerWindow;
            FileOpStruct.wFunc = (uint)Operation;

            String multiSource = StringArrayToMultiString(SourceFiles);
            String multiDest = StringArrayToMultiString(DestFiles);
            FileOpStruct.pFrom = Marshal.StringToHGlobalUni(multiSource);
            FileOpStruct.pTo = Marshal.StringToHGlobalUni(multiDest);

            FileOpStruct.fFlags = (ushort)OperationFlags;
            FileOpStruct.lpszProgressTitle = ProgressTitle;
            FileOpStruct.fAnyOperationsAborted = 0;
            FileOpStruct.hNameMappings = IntPtr.Zero;

            int RetVal;
            RetVal = ShellApi.SHFileOperation(ref FileOpStruct);

            ShellApi.SHChangeNotify(
                (uint)ShellApi.ShellChangeNotificationEvents.SHCNE_ALLEVENTS,
                (uint)ShellApi.ShellChangeNotificationFlags.SHCNF_DWORD,
                IntPtr.Zero,
                IntPtr.Zero);

            if (RetVal != 0)
                return false;

            if (FileOpStruct.fAnyOperationsAborted != 0)
                return false;

            return true;
        }

        /// <summary>
        /// Used by DoOperation to properly build parameters. Refer to https://dev.winodws.com or the Windows SDK for more information.
        /// </summary>
        /// <param name="stringArray">array of strings to process</param>
        /// <returns>A multi-string string where each string is null terminated and the entire string is doublly null terminated.</returns>
        private String StringArrayToMultiString(String[] stringArray)
        {
            String multiString = "";

            if (stringArray == null)
                return "";

            for (int i = 0; i < stringArray.Length; i++)
                multiString += stringArray[i] + '\0';

            multiString += '\0';

            return multiString;
        }
    }
}
