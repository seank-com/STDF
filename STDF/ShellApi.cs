﻿//
// Exposes the Shell API to managed code.
//
// Plase refer to https://dev.winodws.com or the Windows SDK for more information
//

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ShellLib
{
    public class ShellApi
    {
        public delegate Int32 BrowseCallbackProc(IntPtr hwnd, UInt32 uMsg, Int32 lParam, Int32 lpData);

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            [MarshalAs(UnmanagedType.LPStr)]
            public String pszDisplayName;
            [MarshalAs(UnmanagedType.LPStr)]
            public String lpszTitle;
            public UInt32 ulFlags;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public BrowseCallbackProc lpfn;
            public Int32 lParam;
            public Int32 iImage;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct STRRET
        {
            [FieldOffset(0)]
            public UInt32 uType;
            [FieldOffset(4)]
            public IntPtr pOleStr;
            [FieldOffset(4)]
            public IntPtr pStr;
            [FieldOffset(4)]
            public UInt32 uOffset;
            [FieldOffset(4)]
            public IntPtr cStr;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public UInt32 cbSize;
            public UInt32 fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpVerb;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpParameters;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpDirectory;
            public Int32 nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpClass;
            public IntPtr hkeyClass;
            public UInt32 dwHotKey;
            public IntPtr hIconMonitor;
            public IntPtr hProcess;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public UInt32 wFunc;
            public IntPtr pFrom;
            public IntPtr pTo;
            public UInt16 fFlags;
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpszProgressTitle;
        }


        [DllImport("shell32.dll")]
        public static extern Int32 SHGetMalloc(
            out IntPtr hObject);

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderLocation(
            IntPtr hwndOwner,
            Int32 nFolder,
            IntPtr hToken,
            UInt32 dwReserved,
            out IntPtr ppidl);

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetPathFromIDList(
            IntPtr pidl,
            StringBuilder pszPath);

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderPath(
            IntPtr hwndOwner,
            Int32 nFolder,
            IntPtr hToken,
            UInt32 dwFlags,
            StringBuilder pszPath);

        [DllImport("shell32.dll")]
        public static extern Int32 SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)]
			String pszName,
            IntPtr pbc,
            out IntPtr ppidl,
            UInt32 sfgaoIn,
            out UInt32 psfgaoOut);

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetDesktopFolder(
            out IntPtr ppshf);

        [DllImport("shell32.dll")]
        public static extern Int32 SHBindToParent(
            IntPtr pidl,
            [MarshalAs(UnmanagedType.LPStruct)]
			Guid riid,
            out IntPtr ppv,
            ref IntPtr ppidlLast);

        [DllImport("shlwapi.dll")]
        public static extern Int32 StrRetToBSTR(
            ref STRRET pstr,
            IntPtr pidl,
            [MarshalAs(UnmanagedType.BStr)]
			out String pbstr);

        [DllImport("shlwapi.dll")]
        public static extern Int32 StrRetToBuf(
            ref STRRET pstr,
            IntPtr pidl,
            StringBuilder pszBuf,
            UInt32 cchBuf);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHBrowseForFolder(
            ref BROWSEINFO lbpi);
        
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPStr)]
			String lpOperation,
            [MarshalAs(UnmanagedType.LPStr)]
			String lpFile,
            [MarshalAs(UnmanagedType.LPStr)]
			String lpParameters,
            [MarshalAs(UnmanagedType.LPStr)]
			String lpDirectory,
            Int32 nShowCmd);

        [DllImport("shell32.dll")]
        public static extern Int32 ShellExecuteEx(
            ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 SHFileOperation(
            ref SHFILEOPSTRUCT lpFileOp);

        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(
            UInt32 wEventId,
            UInt32 uFlags,
            IntPtr dwItem1,
            IntPtr dwItem2);

        [DllImport("shell32.dll")]
        public static extern void SHAddToRecentDocs(
            UInt32 uFlags,
            IntPtr pv);

        [DllImport("shell32.dll")]
        public static extern void SHAddToRecentDocs(
            UInt32 uFlags,
            [MarshalAs(UnmanagedType.LPWStr)]
			String pv);

        [DllImport("shell32.dll")]
        public static extern Int32 SHInvokePrinterCommand(
            IntPtr hwnd,
            UInt32 uAction,
            [MarshalAs(UnmanagedType.LPWStr)]
			String lpBuf1,
            [MarshalAs(UnmanagedType.LPWStr)]	
			String lpBuf2,
            Int32 fModal);

        public static Int16 GetHResultCode(Int32 hr)
        {
            hr = hr & 0x0000ffff;
            return (Int16)hr;
        }

        public enum CSIDL
        {
            CSIDL_FLAG_CREATE = (0x8000),
            CSIDL_ADMINTOOLS = (0x0030),
            CSIDL_ALTSTARTUP = (0x001d),
            CSIDL_APPDATA = (0x001a),
            CSIDL_BITBUCKET = (0x000a),
            CSIDL_CDBURN_AREA = (0x003b),
            CSIDL_COMMON_ADMINTOOLS = (0x002f),
            CSIDL_COMMON_ALTSTARTUP = (0x001e),
            CSIDL_COMMON_APPDATA = (0x0023),
            CSIDL_COMMON_DESKTOPDIRECTORY = (0x0019),
            CSIDL_COMMON_DOCUMENTS = (0x002e),
            CSIDL_COMMON_FAVORITES = (0x001f),
            CSIDL_COMMON_MUSIC = (0x0035),
            CSIDL_COMMON_PICTURES = (0x0036),
            CSIDL_COMMON_PROGRAMS = (0x0017),
            CSIDL_COMMON_STARTMENU = (0x0016),
            CSIDL_COMMON_STARTUP = (0x0018),
            CSIDL_COMMON_TEMPLATES = (0x002d),
            CSIDL_COMMON_VIDEO = (0x0037),
            CSIDL_CONTROLS = (0x0003),
            CSIDL_COOKIES = (0x0021),
            CSIDL_DESKTOP = (0x0000),
            CSIDL_DESKTOPDIRECTORY = (0x0010),
            CSIDL_DRIVES = (0x0011),
            CSIDL_FAVORITES = (0x0006),
            CSIDL_FONTS = (0x0014),
            CSIDL_HISTORY = (0x0022),
            CSIDL_INTERNET = (0x0001),
            CSIDL_INTERNET_CACHE = (0x0020),
            CSIDL_LOCAL_APPDATA = (0x001c),
            CSIDL_MYDOCUMENTS = (0x000c),
            CSIDL_MYMUSIC = (0x000d),
            CSIDL_MYPICTURES = (0x0027),
            CSIDL_MYVIDEO = (0x000e),
            CSIDL_NETHOOD = (0x0013),
            CSIDL_NETWORK = (0x0012),
            CSIDL_PERSONAL = (0x0005),
            CSIDL_PRINTERS = (0x0004), 
            CSIDL_PRINTHOOD = (0x001b),
            CSIDL_PROFILE = (0x0028),
            CSIDL_PROFILES = (0x003e),
            CSIDL_PROGRAM_FILES = (0x0026),
            CSIDL_PROGRAM_FILES_COMMON = (0x002b),
            CSIDL_PROGRAMS = (0x0002),
            CSIDL_RECENT = (0x0008),
            CSIDL_SENDTO = (0x0009),
            CSIDL_STARTMENU = (0x000b),
            CSIDL_STARTUP = (0x0007),
            CSIDL_SYSTEM = (0x0025),
            CSIDL_TEMPLATES = (0x0015),
            CSIDL_WINDOWS = (0x0024),
        }

        public enum SHGFP_TYPE
        {
            SHGFP_TYPE_CURRENT = 0,
            SHGFP_TYPE_DEFAULT = 1,
        }

        public enum SFGAO : uint
        {
            SFGAO_CANCOPY = 0x00000001,
            SFGAO_CANMOVE = 0x00000002,
            SFGAO_CANLINK = 0x00000004,
            SFGAO_STORAGE = 0x00000008,
            SFGAO_CANRENAME = 0x00000010,
            SFGAO_CANDELETE = 0x00000020,
            SFGAO_HASPROPSHEET = 0x00000040,
            SFGAO_DROPTARGET = 0x00000100,
            SFGAO_CAPABILITYMASK = 0x00000177,
            SFGAO_ENCRYPTED = 0x00002000,
            SFGAO_ISSLOW = 0x00004000,
            SFGAO_GHOSTED = 0x00008000,
            SFGAO_LINK = 0x00010000,
            SFGAO_SHARE = 0x00020000,
            SFGAO_READONLY = 0x00040000,
            SFGAO_HIDDEN = 0x00080000,
            SFGAO_DISPLAYATTRMASK = 0x000FC000,
            SFGAO_FILESYSANCESTOR = 0x10000000,
            SFGAO_FOLDER = 0x20000000,
            SFGAO_FILESYSTEM = 0x40000000,
            SFGAO_HASSUBFOLDER = 0x80000000,
            SFGAO_CONTENTSMASK = 0x80000000,
            SFGAO_VALIDATE = 0x01000000,
            SFGAO_REMOVABLE = 0x02000000,
            SFGAO_COMPRESSED = 0x04000000,
            SFGAO_BROWSABLE = 0x08000000,
            SFGAO_NONENUMERATED = 0x00100000,
            SFGAO_NEWCONTENT = 0x00200000,
            SFGAO_CANMONIKER = 0x00400000,
            SFGAO_HASSTORAGE = 0x00400000,
            SFGAO_STREAM = 0x00400000,
            SFGAO_STORAGEANCESTOR = 0x00800000,
            SFGAO_STORAGECAPMASK = 0x70C50008,
        }

        public enum SHCONTF
        {
            SHCONTF_FOLDERS = 0x0020,
            SHCONTF_NONFOLDERS = 0x0040,
            SHCONTF_INCLUDEHIDDEN = 0x0080,
            SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,
            SHCONTF_NETPRINTERSRCH = 0x0200,
            SHCONTF_SHAREABLE = 0x0400,
            SHCONTF_STORAGE = 0x0800,
        }

        public enum SHCIDS : uint
        {
            SHCIDS_ALLFIELDS = 0x80000000,
            SHCIDS_CANONICALONLY = 0x10000000,
            SHCIDS_BITMASK = 0xFFFF0000,
            SHCIDS_COLUMNMASK = 0x0000FFFF
        }

        public enum SHGNO
        {
            SHGDN_NORMAL = 0x0000,
            SHGDN_INFOLDER = 0x0001,
            SHGDN_FOREDITING = 0x1000,
            SHGDN_FORADDRESSBAR = 0x4000,
            SHGDN_FORPARSING = 0x8000,
        }

        public enum STRRET_TYPE
        {
            STRRET_WSTR = 0x0000,
            STRRET_OFFSET = 0x0001,
            STRRET_CSTR = 0x0002,
        }
        
        public enum PrinterActions
        {
            PRINTACTION_OPEN = 0,
            PRINTACTION_PROPERTIES = 1,
            PRINTACTION_NETINSTALL = 2,
            PRINTACTION_NETINSTALLLINK = 3,
            PRINTACTION_TESTPAGE = 4,
            PRINTACTION_OPENNETPRN = 5,
            PRINTACTION_DOCUMENTDEFAULTS = 6,
            PRINTACTION_SERVERPROPERTIES = 7,
        }

        public enum FileOperations
        {
            FO_MOVE = 0x0001,
            FO_COPY = 0x0002,
            FO_DELETE = 0x0003,
            FO_RENAME = 0x0004
        }

        [Flags]
        public enum ShellFileOperationFlags
        {
            FOF_MULTIDESTFILES = 0x0001,
            FOF_CONFIRMMOUSE = 0x0002,
            FOF_SILENT = 0x0004,
            FOF_RENAMEONCOLLISION = 0x0008,
            FOF_NOCONFIRMATION = 0x0010,
            FOF_WANTMAPPINGHANDLE = 0x0020,
            FOF_ALLOWUNDO = 0x0040,
            FOF_FILESONLY = 0x0080,
            FOF_SIMPLEPROGRESS = 0x0100,
            FOF_NOCONFIRMMKDIR = 0x0200,
            FOF_NOERRORUI = 0x0400,
            FOF_NOCOPYSECURITYATTRIBS = 0x0800,
            FOF_NORECURSION = 0x1000,
            FOF_NO_CONNECTED_ELEMENTS = 0x2000,
            FOF_WANTNUKEWARNING = 0x4000,
            FOF_NORECURSEREPARSE = 0x8000
        }

        [Flags]
        public enum ShellChangeNotificationEvents : uint
        {
            SHCNE_RENAMEITEM = 0x00000001,
            SHCNE_CREATE = 0x00000002,
            SHCNE_DELETE = 0x00000004,
            SHCNE_MKDIR = 0x00000008,
            SHCNE_RMDIR = 0x00000010,
            SHCNE_MEDIAINSERTED = 0x00000020,
            SHCNE_MEDIAREMOVED = 0x00000040,
            SHCNE_DRIVEREMOVED = 0x00000080,
            SHCNE_DRIVEADD = 0x00000100,
            SHCNE_NETSHARE = 0x00000200,
            SHCNE_NETUNSHARE = 0x00000400,
            SHCNE_ATTRIBUTES = 0x00000800,
            SHCNE_UPDATEDIR = 0x00001000,
            SHCNE_UPDATEITEM = 0x00002000,
            SHCNE_SERVERDISCONNECT = 0x00004000,
            SHCNE_UPDATEIMAGE = 0x00008000,
            SHCNE_DRIVEADDGUI = 0x00010000,
            SHCNE_RENAMEFOLDER = 0x00020000,
            SHCNE_FREESPACE = 0x00040000,
            SHCNE_EXTENDED_EVENT = 0x04000000,
            SHCNE_ASSOCCHANGED = 0x08000000,
            SHCNE_DISKEVENTS = 0x0002381F,
            SHCNE_GLOBALEVENTS = 0x0C0581E0,
            SHCNE_ALLEVENTS = 0x7FFFFFFF,
            SHCNE_INTERRUPT = 0x80000000,
        }

        public enum ShellChangeNotificationFlags
        {
            SHCNF_IDLIST = 0x0000,
            SHCNF_PATHA = 0x0001,
            SHCNF_PRINTERA = 0x0002,
            SHCNF_DWORD = 0x0003,
            SHCNF_PATHW = 0x0005,
            SHCNF_PRINTERW = 0x0006,
            SHCNF_TYPE = 0x00FF,
            SHCNF_FLUSH = 0x1000,
            SHCNF_FLUSHNOWAIT = 0x2000,
        }
    }
}
