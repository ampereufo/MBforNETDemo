using System;
using System.Text;
using System.Runtime.InteropServices;

namespace MB
{
    #region 定义委托

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeTitleChangedCallback(IntPtr webView, IntPtr param, IntPtr title);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeMouseOverUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeURLChangedCallback2(IntPtr webView, IntPtr param, IntPtr frame, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkePaintUpdatedCallback(IntPtr webView, IntPtr param, IntPtr buffer, int x, int y, int cx, int cy);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkePaintBitUpdatedCallback(IntPtr webView, IntPtr param, IntPtr hdc, ref wkeRect r, int width, int height);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeAlertBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeConfirmBoxCallback(IntPtr webView, IntPtr param, IntPtr msg);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkePromptBoxCallback(IntPtr webView, IntPtr param, IntPtr msg, IntPtr defaultResult, IntPtr result);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeNavigationCallback(IntPtr webView, IntPtr param, wkeNavigationType navigationType, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr wkeCreateViewCallback(IntPtr webView, IntPtr param, wkeNavigationType navigationType, IntPtr url, IntPtr windowFeatures);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeDocumentReadyCallback(IntPtr webView, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeDocumentReady2Callback(IntPtr webView, IntPtr param, IntPtr frame);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeLoadingFinishCallback(IntPtr webView, IntPtr param, IntPtr url, wkeLoadingResult result, IntPtr failedReason);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeDownloadCallback(IntPtr webView, IntPtr param, IntPtr url);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeDownload2Callback(IntPtr webView, IntPtr param, uint expectedContentLength, IntPtr url, IntPtr mime, IntPtr disposition, IntPtr job, IntPtr dataBind);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeConsoleCallback(IntPtr webView, IntPtr param, wkeConsoleLevel level, IntPtr message, IntPtr sourceName, uint sourceLine, IntPtr stackTrace);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeLoadUrlBeginCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeLoadUrlEndCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job, IntPtr buf, int len);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeLoadUrlFailCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeDidCreateScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frame, IntPtr context, int extensionGroup, int worldId);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeWillReleaseScriptContextCallback(IntPtr webView, IntPtr param, IntPtr frame, IntPtr context, int worldId);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte wkeNetResponseCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr job);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeWillMediaLoadCallback(IntPtr webView, IntPtr param, IntPtr url, IntPtr info);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnOtherLoadCallback(IntPtr webView, IntPtr param, wkeOtherLoadType type, IntPtr info);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long wkeJsNativeFunction(IntPtr jsExecState, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnShowDevtoolsCallback(IntPtr webView, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnNetGetFaviconCallback(IntPtr webView, IntPtr param, IntPtr utf8Url, ref wkeMemBuf buf);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeNetJobDataRecvCallback(IntPtr ptr, IntPtr job, IntPtr data, int length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeNetJobDataFinishCallback(IntPtr ptr, IntPtr job, wkeLoadingResult result);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate long jsGetPropertyCallback(IntPtr es, long obj, string propertyName);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate byte jsSetPropertyCallback(IntPtr es, long obj, string propertyName, long value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long jsCallAsFunctionCallback(IntPtr es, long obj, IntPtr args, int argCount);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void jsFinalizeCallback(IntPtr data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnUrlRequestWillRedirectCallback(IntPtr webView, IntPtr param, IntPtr oldRequest, IntPtr request, IntPtr redirectResponse);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnUrlRequestDidReceiveResponseCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr response);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnUrlRequestDidReceiveDataCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr data, int dataLength);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnUrlRequestDidFailCallback(IntPtr webView, IntPtr param, IntPtr request, IntPtr error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void wkeOnUrlRequestDidFinishLoadingCallback(IntPtr webView, IntPtr param, IntPtr request, long finishTime);

    /// <summary>
    /// 访问Cookie回调
    /// </summary>
    /// <param name="userData">用户数据</param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="domain">域名</param>
    /// <param name="path">路径</param>
    /// <param name="secure">安全，如果非0则仅发送到https请求</param>
    /// <param name="httpOnly">如果非0则仅发送到http请求</param>
    /// <param name="expires">过期时间 The cookie expiration date is only valid if |has_expires| is true.</param>
    /// <returns>返回true 则应用程序自己处理miniblink不处理</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool wkeCookieVisitor(IntPtr userData, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string value, [MarshalAs(UnmanagedType.LPStr)]string domain, [MarshalAs(UnmanagedType.LPStr)]string path, int secure, int httpOnly, ref int expires);

    #endregion

    #region 枚举

    public enum wkeMouseFlags
    {
        WKE_LBUTTON = 0x01,
        WKE_RBUTTON = 0x02,
        WKE_SHIFT = 0x04,
        WKE_CONTROL = 0x08,
        WKE_MBUTTON = 0x10,
    }

    public enum wkeKeyFlags
    {
        WKE_EXTENDED = 0x0100,
        WKE_REPEAT = 0x4000,
    }

    public enum jsType
    {
        NUMBER,
        STRING,
        BOOLEAN,
        OBJECT,
        FUNCTION,
        UNDEFINED,
        ARRAY,
        NULL
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct jsKeys
    {
        public int length;
        public IntPtr keys;
    }

    public enum wkeConsoleLevel
    {
        Debug = 4,
        Log = 1,
        Info = 5,
        Warning = 2,
        Error = 3,
        RevokedError = 6,
    }

    public enum wkeLoadingResult
    {
        Succeeded,
        Failed,
        Canceled
    }

    public enum wkeNavigationType
    {
        LinkClick,
        FormSubmit,
        BackForward,
        ReLoad,
        FormReSubmit,
        Other
    }

    public enum wkeCursorStyle
    {
        Pointer,
        Cross,
        Hand,
        IBeam,
        Wait,
        Help,
        EastResize,
        NorthResize,
        NorthEastResize,
        NorthWestResize,
        SouthResize,
        SouthEastResize,
        SouthWestResize,
        WestResize,
        NorthSouthResize,
        EastWestResize,
        NorthEastSouthWestResize,
        NorthWestSouthEastResize,
        ColumnResize,
        RowResize,
        MiddlePanning,
        EastPanning,
        NorthPanning,
        NorthEastPanning,
        NorthWestPanning,
        SouthPanning,
        SouthEastPanning,
        SouthWestPanning,
        WestPanning,
        Move,
        VerticalText,
        Cell,
        ContextMenu,
        Alias,
        Progress,
        NoDrop,
        Copy,
        None,
        NotAllowed,
        ZoomIn,
        ZoomOut,
        Grab,
        Grabbing,
        Custom
    }

    public enum wkeCookieCommand
    {
        ClearAllCookies,
        ClearSessionCookies,
        FlushCookiesToFile,
        ReloadCookiesFromFile
    }

    public enum wkeProxyType
    {
        NONE,
        HTTP,
        SOCKS4,
        SOCKS4A,
        SOCKS5,
        SOCKS5HOSTNAME
    }

    public enum wkeSettingMask
    {
        PROXY = 1,
        PAINTCALLBACK_IN_OTHER_THREAD = 4,
    }

    public enum wkeOtherLoadType
    {
        WKE_DID_START_LOADING,
        WKE_DID_STOP_LOADING,
        WKE_DID_NAVIGATE,
        WKE_DID_NAVIGATE_IN_PAGE,
        WKE_DID_GET_RESPONSE_DETAILS,
        WKE_DID_GET_REDIRECT_REQUEST
    }

    public enum wkeResourceType
    {
        MAIN_FRAME = 0,       // top level page
        SUB_FRAME = 1,        // frame or iframe
        STYLESHEET = 2,       // a CSS stylesheet
        SCRIPT = 3,           // an external script
        IMAGE = 4,            // an image (jpg/gif/png/etc)
        FONT_RESOURCE = 5,    // a font
        SUB_RESOURCE = 6,     // an "other" subresource.
        OBJECT = 7,           // an object (or embed) tag for a plugin, or a resource that a plugin requested.
        MEDIA = 8,            // a media resource.
        WORKER = 9,           // the main resource of a dedicated worker.
        SHARED_WORKER = 10,   // the main resource of a shared worker.
        PREFETCH = 11,        // an explicitly requested prefetch
        FAVICON = 12,         // a favicon
        XHR = 13,             // a XMLHttpRequest
        PING = 14,            // a ping request for <a ping>
        SERVICE_WORKER = 15,  // the main resource of a service worker.
    }

    public enum wkeMenuItemId
    {
        kWkeMenuSelectedAllId = 1 << 1,
        kWkeMenuSelectedTextId = 1 << 2,
        kWkeMenuUndoId = 1 << 3,
        kWkeMenuCopyImageId = 1 << 4,
        kWkeMenuInspectElementAtId = 1 << 5,
        kWkeMenuCutId = 1 << 6,
        kWkeMenuPasteId = 1 << 7,
        kWkeMenuPrintId = 1 << 8,
        kWkeMenuGoForwardId = 1 << 9,
        kWkeMenuGoBackId = 1 << 10,
        kWkeMenuReloadId = 1 << 11,
    }

    public enum wkeRequestType
    {
        Invalidation,
        Get,
        Post,
        Put,
    }

    public enum wkeHttBodyElementType
    {
        wkeHttBodyElementTypeData,
        wkeHttBodyElementTypeFile
    }

    #endregion

    #region 结构体

    public struct jsData
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string typeName;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public jsGetPropertyCallback propertyGet;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public jsSetPropertyCallback propertySet;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public jsFinalizeCallback finalize;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public jsCallAsFunctionCallback callAsFunction;
    }

    public struct wkeNetJobDataBind
    {
        IntPtr param;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public wkeNetJobDataRecvCallback recvCallback;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public wkeNetJobDataFinishCallback finishCallback;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeRect
    {
        public int x;
        public int y;
        public int w;
        public int h;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct wkeProxy
    {
        public wkeProxyType Type;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string HostName;

        public ushort Port;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string UserName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string Password;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeSettings
    {
        public wkeProxy Proxy;
        public wkeSettingMask Mask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeWindowFeatures
    {
        public int x;
        public int y;
        public int width;
        public int height;

        [MarshalAs(UnmanagedType.I1)]
        public bool menuBarVisible;

        [MarshalAs(UnmanagedType.I1)]
        public bool statusBarVisible;

        [MarshalAs(UnmanagedType.I1)]
        public bool toolBarVisible;

        [MarshalAs(UnmanagedType.I1)]
        public bool locationBarVisible;

        [MarshalAs(UnmanagedType.I1)]
        public bool scrollbarsVisible;

        [MarshalAs(UnmanagedType.I1)]
        public bool resizable;

        [MarshalAs(UnmanagedType.I1)]
        public bool fullscreen;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeMediaLoadInfo
    {
        public int size;
        public int width;
        public int height;
        public double duration;
    }

    public struct wkeWillSendRequestInfo
    {
        public bool isHolded;
        public string url;
        public string newUrl;
        public wkeResourceType resourceType;
        public int httpResponseCode;
        public string method;
        public string referrer;
        public IntPtr headers;
    }

    public struct wkeTempCallbackInfo
    {
        public int size;
        public IntPtr frame;
        public IntPtr willSendRequestInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeMemBuf
    {
        public int size;
        public IntPtr data;
        public int length;
    }

    public struct jsExceptionInfo
    {
        public string Message;
        public string SourceLine;
        public string ScriptResourceName;
        public int LineNumber;
        public int StartPosition;
        public int EndPosition;
        public int StartColumn;
        public int EndColoumn;
        public string CallStackString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeViewSettings
    {
        public int size;
        public uint bgColor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeSlist
    {
        public IntPtr str;
        public IntPtr next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkePostBodyElement
    {
        public int size;
        public wkeHttBodyElementType type;
        public IntPtr data;
        public string filePath;
        public long fileStart;
        public long fileLength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkePostBodyElements
    {
        public int size;
        public IntPtr element;
        public int elementSize;
        public bool isDirty;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct wkeUrlRequestCallbacks
    {
        wkeOnUrlRequestWillRedirectCallback willRedirectCallback;
        wkeOnUrlRequestDidReceiveResponseCallback didReceiveResponseCallback;
        wkeOnUrlRequestDidReceiveDataCallback didReceiveDataCallback;
        wkeOnUrlRequestDidFailCallback didFailCallback;
        wkeOnUrlRequestDidFinishLoadingCallback didFinishLoadingCallback;
    }

    #endregion

    public class MBApi
    {
        [DllImport("node.dll", EntryPoint = "wkeIsInitialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsInitialize();

        [DllImport("node.dll", EntryPoint = "wkeInitialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeInitialize();

        [DllImport("node.dll", EntryPoint = "wkeInitializeEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeInitializeEx(wkeSettings settings);

        [DllImport("node.dll", EntryPoint = "wkeSetViewSettings", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetViewSettings(IntPtr webView, wkeViewSettings settings);

        [DllImport("node.dll", EntryPoint = "wkeConfigure", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeConfigure(wkeSettings settings);

        [DllImport("node.dll", EntryPoint = "wkeSetDebugConfig", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeSetDebugConfig(IntPtr webView, string debugString, [MarshalAs(UnmanagedType.LPArray)]byte[] param);

        [DllImport("node.dll", EntryPoint = "wkeGetDebugConfig", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeGetDebugConfig(IntPtr webView, string debugString);

        [DllImport("node.dll", EntryPoint = "wkeGetVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint wkeGetVersion();

        [DllImport("node.dll", EntryPoint = "wkeGetVersionString", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetVersionString();

        [DllImport("node.dll", EntryPoint = "wkeGC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeGC(IntPtr webView, int delayMs);

        [DllImport("node.dll", EntryPoint = "wkeCreateWebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeCreateWebView();

        [DllImport("node.dll", EntryPoint = "wkeGetWebView", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeGetWebView(string name);

        [DllImport("node.dll", EntryPoint = "wkeDestroyWebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeDestroyWebView(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetMemoryCacheEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetMemoryCacheEnable(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetTouchEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetTouchEnabled(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetNavigationToNewWindowEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetNavigationToNewWindowEnable(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetCspCheckEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetCspCheckEnable(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetNpapiPluginsEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetNpapiPluginsEnabled(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetHeadlessEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetHeadlessEnabled(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetMouseEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetMouseEnabled(IntPtr webView, [MarshalAs(UnmanagedType.I1)] bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetDragEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetDragEnable(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetDragDropEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetDragDropEnable(IntPtr WebView, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetContextMenuItemShow", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetContextMenuItemShow(IntPtr WebView, wkeMenuItemId item, [MarshalAs(UnmanagedType.I1)]bool b);

        [DllImport("node.dll", EntryPoint = "wkeSetLanguage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeSetLanguage(IntPtr WebView, string language);

        [DllImport("node.dll", EntryPoint = "wkeSetViewNetInterface", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeSetViewNetInterface(IntPtr webView, string netInterface);

        [DllImport("node.dll", EntryPoint = "wkeSetProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetProxy(ref wkeProxy proxy);

        [DllImport("node.dll", EntryPoint = "wkeSetViewProxy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetViewProxy(IntPtr webView, ref wkeProxy proxy);

        [DllImport("node.dll", EntryPoint = "wkeGetName", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetName(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetName", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeSetName(IntPtr webView, string name);

        [DllImport("node.dll", EntryPoint = "wkeSetHandle", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetHandle(IntPtr webView, IntPtr wndHandle);

        [DllImport("node.dll", EntryPoint = "wkeSetHandleOffset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetHandleOffset(IntPtr webView, int x, int y);

        [DllImport("node.dll", EntryPoint = "wkeIsTransparent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsTransparent(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetTransparent", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetTransparent(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool transparent);

        [DllImport("node.dll", EntryPoint = "wkeSetUserAgentW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeSetUserAgentW(IntPtr webView, string userAgent);

        [DllImport("node.dll", EntryPoint = "wkeLoadW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeLoadW(IntPtr webView, string url);

        [DllImport("node.dll", EntryPoint = "wkeLoadURLW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeLoadURLW(IntPtr webView, string url);

        [DllImport("node.dll", EntryPoint = "wkePostURLW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkePostURLW(IntPtr webView, string url, [MarshalAs(UnmanagedType.LPArray)]byte[] postData, int postLen);

        [DllImport("node.dll", EntryPoint = "wkeLoadHTMLW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeLoadHTMLW(IntPtr webView, string html);

        [DllImport("node.dll", EntryPoint = "wkeLoadFileW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeLoadFileW(IntPtr webView, string fileName);

        [DllImport("node.dll", EntryPoint = "wkeGetURL", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetURL(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsLoading", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsLoading(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsLoadingFailed", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsLoadingFailed(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsLoadingCompleted", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsLoadingCompleted(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsDocumentReady", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsDocumentReady(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeStopLoading", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeStopLoading(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeReload", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeReload(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGoToOffset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeGoToOffset(IntPtr webView, int offset);

        [DllImport("node.dll", EntryPoint = "wkeGoToIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeGoToIndex(IntPtr webView, int index);

        [DllImport("node.dll", EntryPoint = "wkeGetWebviewId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeGetWebviewId(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsWebviewAlive", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsWebviewAlive(IntPtr webView, int id);

        [DllImport("node.dll", EntryPoint = "wkeGetDocumentCompleteURL", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetDocumentCompleteURL(IntPtr webView, IntPtr frameId, [MarshalAs(UnmanagedType.LPArray)]byte[] partialURL);

        [DllImport("node.dll", EntryPoint = "wkeCreateMemBuf", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeCreateMemBuf(IntPtr webView, [MarshalAs(UnmanagedType.LPArray)]byte[] buff, int length);

        [DllImport("node.dll", EntryPoint = "wkeFreeMemBuf", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeFreeMemBuf(IntPtr buf);

        [DllImport("node.dll", EntryPoint = "wkeGetTitleW", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetTitleW(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeResize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeResize(IntPtr webView, int w, int h);

        [DllImport("node.dll", EntryPoint = "wkeGetWidth", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeGetWidth(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetHeight", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeGetHeight(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetContentWidth", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeGetContentWidth(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetContentHeight", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeGetContentHeight(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkePaint2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkePaint2(IntPtr webView, IntPtr bits, int bufWid, int bufHei, int xDst, int yDst, int w, int h, int xSrc, int ySrc, [MarshalAs(UnmanagedType.I1)]bool bCopyAlpha);

        [DllImport("node.dll", EntryPoint = "wkePaint", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkePaint(IntPtr webView, IntPtr bits, int pitch);

        [DllImport("node.dll", EntryPoint = "wkeGetViewDC", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetViewDC(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetHostHWND", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetHostHWND(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeCanGoBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeCanGoBack(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGoBack", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeGoBack(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeCanGoForward", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeCanGoForward(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGoForward", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeGoForward(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorSelectAll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeEditorSelectAll(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorUnSelect", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeEditorUnSelect(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorCopy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorCopy(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorCut", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorCut(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorPaste", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorPaste(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorDelete", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorDelete(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorUndo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorUndo(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeEditorRedo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeEditorRedo(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetCookieW", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetCookieW(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetCookie(IntPtr webView, [MarshalAs(UnmanagedType.LPArray)]byte[] url, [MarshalAs(UnmanagedType.LPArray)]byte[] cookie);

        [DllImport("node.dll", EntryPoint = "wkeVisitAllCookie", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeVisitAllCookie(IntPtr webView, IntPtr usetData, wkeCookieVisitor visitor);

        [DllImport("node.dll", EntryPoint = "wkePerformCookieCommand", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkePerformCookieCommand(wkeCookieCommand command);

        [DllImport("node.dll", EntryPoint = "wkeSetCookieEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetCookieEnabled(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool enable);

        [DllImport("node.dll", EntryPoint = "wkeIsCookieEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsCookieEnabled(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetCookieJarPath", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeSetCookieJarPath(IntPtr webView, string path);

        [DllImport("node.dll", EntryPoint = "wkeSetCookieJarFullPath", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeSetCookieJarFullPath(IntPtr webView, string path);

        [DllImport("node.dll", EntryPoint = "wkeSetLocalStorageFullPath", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeSetLocalStorageFullPath(IntPtr webView, string path);

        [DllImport("node.dll", EntryPoint = "wkeSetMediaVolume", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetMediaVolume(IntPtr webView, float volume);

        [DllImport("node.dll", EntryPoint = "wkeGetMediaVolume", CallingConvention = CallingConvention.Cdecl)]
        public static extern float wkeGetMediaVolume(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeFireMouseEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireMouseEvent(IntPtr webView, uint message, int x, int y, uint flags);

        [DllImport("node.dll", EntryPoint = "wkeFireContextMenuEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireContextMenuEvent(IntPtr webView, int x, int y, uint flags);

        [DllImport("node.dll", EntryPoint = "wkeFireMouseWheelEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireMouseWheelEvent(IntPtr webView, int x, int y, int delta, uint flags);

        [DllImport("node.dll", EntryPoint = "wkeFireKeyUpEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireKeyUpEvent(IntPtr webView, int virtualKeyCode, uint flags, [MarshalAs(UnmanagedType.I1)]bool systemKey);

        [DllImport("node.dll", EntryPoint = "wkeFireKeyDownEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireKeyDownEvent(IntPtr webView, int virtualKeyCode, uint flags, [MarshalAs(UnmanagedType.I1)]bool systemKey);

        [DllImport("node.dll", EntryPoint = "wkeFireKeyPressEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireKeyPressEvent(IntPtr webView, int charCode, uint flags, [MarshalAs(UnmanagedType.I1)]bool systemKey);

        [DllImport("node.dll", EntryPoint = "wkeFireWindowsMessage", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeFireWindowsMessage(IntPtr webView, IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, IntPtr result);

        [DllImport("node.dll", EntryPoint = "wkeSetFocus", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeSetFocus(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeKillFocus", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeKillFocus(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetCaretRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern wkeRect wkeGetCaretRect(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeRunJSW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern long wkeRunJSW(IntPtr webView, string script);

        [DllImport("node.dll", EntryPoint = "wkeGlobalExec", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGlobalExec(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSleep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSleep(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeWake", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeWake(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeIsAwake", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsAwake(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetZoomFactor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetZoomFactor(IntPtr webView, float factor);

        [DllImport("node.dll", EntryPoint = "wkeGetZoomFactor", CallingConvention = CallingConvention.Cdecl)]
        public static extern float wkeGetZoomFactor(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetEditable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetEditable(IntPtr webView, [MarshalAs(UnmanagedType.I1)]bool editable);

        [DllImport("node.dll", EntryPoint = "wkeGetStringW", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetStringW(IntPtr wkeString);

        [DllImport("node.dll", EntryPoint = "wkeSetStringW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeSetStringW(IntPtr wkeString, string str, int len);

        [DllImport("node.dll", EntryPoint = "wkeCreateStringW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr wkeCreateStringW(string str, int len);

        [DllImport("node.dll", EntryPoint = "wkeDeleteString", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeDeleteString(IntPtr wkeString);

        [DllImport("node.dll", EntryPoint = "wkeGetWebViewForCurrentContext", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetWebViewForCurrentContext();

        [DllImport("node.dll", EntryPoint = "wkeSetUserKeyValue", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeSetUserKeyValue(IntPtr webView, string key, IntPtr value);

        [DllImport("node.dll", EntryPoint = "wkeGetUserKeyValue", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeGetUserKeyValue(IntPtr webView, string key);

        [DllImport("node.dll", EntryPoint = "wkeGetCursorInfoType", CallingConvention = CallingConvention.Cdecl)]
        public static extern wkeCursorStyle wkeGetCursorInfoType(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeSetCursorInfoType", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetCursorInfoType(IntPtr webView, wkeCursorStyle type);

        [DllImport("node.dll", EntryPoint = "wkeSetDragFiles", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetDragFiles(IntPtr webView, IntPtr clintPos, IntPtr screenPos, [MarshalAs(UnmanagedType.LPArray)]IntPtr[] files, int filesCount);

        [DllImport("node.dll", EntryPoint = "wkeOnMouseOverUrlChanged", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnMouseOverUrlChanged(IntPtr webView, wkeMouseOverUrlChangedCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnTitleChanged", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnTitleChanged(IntPtr webView, wkeTitleChangedCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnURLChanged2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnURLChanged2(IntPtr webView, wkeURLChangedCallback2 callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnPaintUpdated", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnPaintUpdated(IntPtr webView, wkePaintUpdatedCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnPaintBitUpdated", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnPaintBitUpdated(IntPtr webView, wkePaintBitUpdatedCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnAlertBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnAlertBox(IntPtr webView, wkeAlertBoxCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnConfirmBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnConfirmBox(IntPtr webView, wkeConfirmBoxCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnPromptBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnPromptBox(IntPtr webView, wkePromptBoxCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnNavigation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnNavigation(IntPtr webView, wkeNavigationCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnCreateView", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnCreateView(IntPtr webView, wkeCreateViewCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnDocumentReady", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnDocumentReady(IntPtr webView, wkeDocumentReadyCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnDocumentReady2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnDocumentReady2(IntPtr webView, wkeDocumentReady2Callback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnLoadingFinish", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnLoadingFinish(IntPtr webView, wkeLoadingFinishCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnDownload", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnDownload(IntPtr webView, wkeDownloadCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnDownload2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnDownload2(IntPtr webView, wkeDownload2Callback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnConsole", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnConsole(IntPtr webView, wkeConsoleCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnDidCreateScriptContext", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnDidCreateScriptContext(IntPtr webView, wkeDidCreateScriptContextCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnWillReleaseScriptContext", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnWillReleaseScriptContext(IntPtr webView, wkeWillReleaseScriptContextCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnLoadUrlBegin", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnLoadUrlBegin(IntPtr webView, wkeLoadUrlBeginCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnLoadUrlEnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnLoadUrlEnd(IntPtr webView, wkeLoadUrlEndCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeOnLoadUrlFail", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnLoadUrlFail(IntPtr webView, wkeLoadUrlFailCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeNetOnResponse", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetOnResponse(IntPtr webView, wkeNetResponseCallback callback, IntPtr callbackParam);

        [DllImport("node.dll", EntryPoint = "wkeNetSetMIMEType", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeNetSetMIMEType(IntPtr job, string type);

        [DllImport("node.dll", EntryPoint = "wkeNetSetHTTPHeaderField", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeNetSetHTTPHeaderField(IntPtr job, string key, string value, [MarshalAs(UnmanagedType.I1)]bool response);

        [DllImport("node.dll", EntryPoint = "wkeNetGetHTTPHeaderField", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeNetGetHTTPHeaderField(IntPtr job, string key);

        [DllImport("node.dll", EntryPoint = "wkeNetGetHTTPHeaderFieldFromResponse", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr wkeNetGetHTTPHeaderFieldFromResponse(IntPtr job, string key);

        [DllImport("node.dll", EntryPoint = "wkeNetSetData", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeNetSetData(IntPtr job, [MarshalAs(UnmanagedType.LPArray)]byte[] buf, int len);

        [DllImport("node.dll", EntryPoint = "wkeNetHookRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetHookRequest(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetGetRequestMethod", CallingConvention = CallingConvention.Cdecl)]
        public static extern wkeRequestType wkeNetGetRequestMethod(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetGetMIMEType", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetMIMEType(IntPtr job, IntPtr mime);

        [DllImport("node.dll", EntryPoint = "wkeNetContinueJob", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetContinueJob(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetGetUrlByJob", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetUrlByJob(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetGetRawHttpHead", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetRawHttpHead(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetGetRawResponseHead", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetRawResponseHead(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetCancelRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetCancelRequest(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetHoldJobToAsynCommit", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeNetHoldJobToAsynCommit(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetChangeRequestUrl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern byte wkeNetChangeRequestUrl(IntPtr job, string url);

        [DllImport("node.dll", EntryPoint = "wkeNetCreateWebUrlRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetCreateWebUrlRequest(IntPtr url, IntPtr method, IntPtr mime);

        [DllImport("node.dll", EntryPoint = "wkeNetCreateWebUrlRequest2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetCreateWebUrlRequest2(IntPtr request);

        [DllImport("node.dll", EntryPoint = "wkeNetCopyWebUrlRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetCopyWebUrlRequest(IntPtr job, [MarshalAs(UnmanagedType.I1)]bool needExtraData);

        [DllImport("node.dll", EntryPoint = "wkeNetDeleteBlinkWebURLRequestPtr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetDeleteBlinkWebURLRequestPtr(IntPtr request);

        [DllImport("node.dll", EntryPoint = "wkeNetAddHTTPHeaderFieldToUrlRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetAddHTTPHeaderFieldToUrlRequest(IntPtr request, IntPtr name, IntPtr value);

        [DllImport("node.dll", EntryPoint = "wkeNetStartUrlRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeNetStartUrlRequest(IntPtr webView, IntPtr request, IntPtr param, wkeUrlRequestCallbacks callback);

        [DllImport("node.dll", EntryPoint = "wkeNetGetHttpStatusCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeNetGetHttpStatusCode(IntPtr response);

        [DllImport("node.dll", EntryPoint = "wkeNetGetExpectedContentLength", CallingConvention = CallingConvention.Cdecl)]
        public static extern long wkeNetGetExpectedContentLength(IntPtr response);

        [DllImport("node.dll", EntryPoint = "wkeNetGetResponseUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetResponseUrl(IntPtr response);

        [DllImport("node.dll", EntryPoint = "wkeNetCancelWebUrlRequest", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetCancelWebUrlRequest(int requestId);

        [DllImport("node.dll", EntryPoint = "wkeNetGetPostBody", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetGetPostBody(IntPtr job);

        [DllImport("node.dll", EntryPoint = "wkeNetCreatePostBodyElements", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetCreatePostBodyElements(IntPtr webView, long length);

        [DllImport("node.dll", EntryPoint = "wkeNetFreePostBodyElements", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetFreePostBodyElements(IntPtr elements);

        [DllImport("node.dll", EntryPoint = "wkeNetCreatePostBodyElement", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeNetCreatePostBodyElement(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeNetFreePostBodyElement", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeNetFreePostBodyElement(IntPtr element);

        [DllImport("node.dll", EntryPoint = "wkeIsMainFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsMainFrame(IntPtr webFrame);

        [DllImport("node.dll", EntryPoint = "wkeIsWebRemoteFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsWebRemoteFrame(IntPtr webFrame);

        [DllImport("node.dll", EntryPoint = "wkeWebFrameGetMainFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeWebFrameGetMainFrame(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeWebFrameGetMainWorldScriptContext", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeWebFrameGetMainWorldScriptContext(IntPtr webFrame, ref IntPtr contextOut);

        [DllImport("node.dll", EntryPoint = "wkeGetBlinkMainThreadIsolate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetBlinkMainThreadIsolate();

        [DllImport("node.dll", EntryPoint = "wkeRunJsByFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern long wkeRunJsByFrame(IntPtr webView, IntPtr frameId, [MarshalAs(UnmanagedType.LPArray)]byte[] script, [MarshalAs(UnmanagedType.I1)]bool isInClosure);

        [DllImport("node.dll", EntryPoint = "wkeGetWindowHandle", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetWindowHandle(IntPtr WebView);

        [DllImport("node.dll", EntryPoint = "wkeOnWillMediaLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnWillMediaLoad(IntPtr WebView, wkeWillMediaLoadCallback callback, IntPtr param);

        [DllImport("node.dll", EntryPoint = "wkeDeleteWillSendRequestInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeDeleteWillSendRequestInfo(IntPtr WebView, IntPtr info);

        [DllImport("node.dll", EntryPoint = "wkeOnOtherLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeOnOtherLoad(IntPtr WebView, wkeOnOtherLoadCallback callback, IntPtr param);

        [DllImport("node.dll", EntryPoint = "wkeSetDeviceParameter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeSetDeviceParameter(IntPtr WebView, string device, string paramStr, int paramInt, float paramFloat);

        [DllImport("node.dll", EntryPoint = "wkeAddPluginDirectory", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeAddPluginDirectory(IntPtr WebView, string path);

        [DllImport("node.dll", EntryPoint = "wkeGetGlobalExecByFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetGlobalExecByFrame(IntPtr WebView, IntPtr frameId);

        [DllImport("node.dll", EntryPoint = "wkeShowDevtools", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void wkeShowDevtools(IntPtr WebView, string path, wkeOnShowDevtoolsCallback callback, IntPtr param);

        [DllImport("node.dll", EntryPoint = "wkeInsertCSSByFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeInsertCSSByFrame(IntPtr WebView, IntPtr frameId, [MarshalAs(UnmanagedType.LPArray)]byte[] utf8css);

        [DllImport("node.dll", EntryPoint = "wkeSetResourceGc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeSetResourceGc(IntPtr WebView, int intervalSec);

        [DllImport("node.dll", EntryPoint = "wkeLoadHtmlWithBaseUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wkeLoadHtmlWithBaseUrl(IntPtr WebView, [MarshalAs(UnmanagedType.LPArray)]byte[] utf8html, [MarshalAs(UnmanagedType.LPArray)]byte[] baseUrl);

        [DllImport("node.dll", EntryPoint = "wkeGetUserAgent", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetUserAgent(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetFrameUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetFrameUrl(IntPtr webView, IntPtr frameId);

        [DllImport("node.dll", EntryPoint = "wkeNetGetFavicon", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wkeNetGetFavicon(IntPtr webView, wkeNetResponseCallback callback, IntPtr param);

        [DllImport("node.dll", EntryPoint = "wkeIsProcessingUserGesture", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte wkeIsProcessingUserGesture(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeUtilSerializeToMHTML", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeUtilSerializeToMHTML(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeGetSource", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wkeGetSource(IntPtr webView);

        [DllImport("node.dll", EntryPoint = "wkeJsBindFunction", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeJsBindFunction(string name, wkeJsNativeFunction fn, IntPtr param, uint argCount);

        [DllImport("node.dll", EntryPoint = "wkeJsBindGetter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeJsBindGetter(string name, wkeJsNativeFunction fn, IntPtr param);

        [DllImport("node.dll", EntryPoint = "wkeJsBindSetter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void wkeJsBindSetter(string name, wkeJsNativeFunction fn, IntPtr param);

        [DllImport("node.dll", EntryPoint = "jsArgCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int jsArgCount(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsArgType", CallingConvention = CallingConvention.Cdecl)]
        public static extern jsType jsArgType(IntPtr es, int argIdx);

        [DllImport("node.dll", EntryPoint = "jsArg", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsArg(IntPtr es, int argIdx);

        [DllImport("node.dll", EntryPoint = "jsTypeOf", CallingConvention = CallingConvention.Cdecl)]
        public static extern jsType jsTypeOf(long v);

        [DllImport("node.dll", EntryPoint = "jsIsNumber", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsNumber(long v);

        [DllImport("node.dll", EntryPoint = "jsIsString", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsString(long v);

        [DllImport("node.dll", EntryPoint = "jsIsBoolean", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsBoolean(long v);

        [DllImport("node.dll", EntryPoint = "jsIsObject", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsObject(long v);

        [DllImport("node.dll", EntryPoint = "jsIsFunction", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsFunction(long v);

        [DllImport("node.dll", EntryPoint = "jsIsUndefined", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsUndefined(long v);

        [DllImport("node.dll", EntryPoint = "jsIsNull", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsNull(long v);

        [DllImport("node.dll", EntryPoint = "jsIsArray", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsArray(long v);

        [DllImport("node.dll", EntryPoint = "jsIsTrue", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsTrue(long v);

        [DllImport("node.dll", EntryPoint = "jsIsFalse", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsFalse(long v);

        [DllImport("node.dll", EntryPoint = "jsToInt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int jsToInt(IntPtr es, long v);

        [DllImport("node.dll", EntryPoint = "jsToFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern float jsToFloat(IntPtr es, long v);

        [DllImport("node.dll", EntryPoint = "jsToDouble", CallingConvention = CallingConvention.Cdecl)]
        public static extern double jsToDouble(IntPtr es, long v);

        [DllImport("node.dll", EntryPoint = "jsToBoolean", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsToBoolean(IntPtr es, long v);

        [DllImport("node.dll", EntryPoint = "jsToTempStringW", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsToTempStringW(IntPtr es, long v);

        [DllImport("node.dll", EntryPoint = "jsInt", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsInt(int n);

        [DllImport("node.dll", EntryPoint = "jsFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsFloat(float f);

        [DllImport("node.dll", EntryPoint = "jsDouble", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsDouble(double d);

        [DllImport("node.dll", EntryPoint = "jsBoolean", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsBoolean(bool b);

        [DllImport("node.dll", EntryPoint = "jsUndefined", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsUndefined();

        [DllImport("node.dll", EntryPoint = "jsNull", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsNull();

        [DllImport("node.dll", EntryPoint = "jsTrue", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsTrue();

        [DllImport("node.dll", EntryPoint = "jsFalse", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsFalse();

        [DllImport("node.dll", EntryPoint = "jsStringW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern long jsStringW(IntPtr es, string str);

        [DllImport("node.dll", EntryPoint = "jsEmptyObject", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsEmptyObject(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsEmptyArray", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsEmptyArray(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsArrayBuffer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long jsArrayBuffer(IntPtr es, StringBuilder buffer, int size);

        [DllImport("node.dll", EntryPoint = "jsObject", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsObject(IntPtr es, IntPtr obj);

        [DllImport("node.dll", EntryPoint = "jsFunction", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsFunction(IntPtr es, IntPtr obj);

        [DllImport("node.dll", EntryPoint = "jsGetData", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsGetData(IntPtr es, long jsValue);

        [DllImport("node.dll", EntryPoint = "jsGet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long jsGet(IntPtr es, long jsValue, string prop);

        [DllImport("node.dll", EntryPoint = "jsSet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void jsSet(IntPtr es, long jsValue, string prop, long v);

        [DllImport("node.dll", EntryPoint = "jsGetAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsGetAt(IntPtr es, long jsValue, int index);

        [DllImport("node.dll", EntryPoint = "jsSetAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern void jsSetAt(IntPtr es, long jsValue, int index, long v);

        [DllImport("node.dll", EntryPoint = "jsGetLength", CallingConvention = CallingConvention.Cdecl)]
        public static extern int jsGetLength(IntPtr es, long jsValue);

        [DllImport("node.dll", EntryPoint = "jsSetLength", CallingConvention = CallingConvention.Cdecl)]
        public static extern void jsSetLength(IntPtr es, long jsValue, int length);

        [DllImport("node.dll", EntryPoint = "jsGlobalObject", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsGlobalObject(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsGetWebView", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsGetWebView(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsEvalW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern long jsEvalW(IntPtr es, string str);

        [DllImport("node.dll", EntryPoint = "jsEvalExW", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern long jsEvalExW(IntPtr es, string str, [MarshalAs(UnmanagedType.I1)]bool isInClosure);

        [DllImport("node.dll", EntryPoint = "jsCall", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsCall(IntPtr es, long func, long thisObject, [MarshalAs(UnmanagedType.LPArray)]Int64[] args, int argCount);

        [DllImport("node.dll", EntryPoint = "jsCallGlobal", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsCallGlobal(IntPtr es, long func, [MarshalAs(UnmanagedType.LPArray)]Int64[] args, int argCount);

        [DllImport("node.dll", EntryPoint = "jsGetGlobal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern long jsGetGlobal(IntPtr es, string prop);

        [DllImport("node.dll", EntryPoint = "jsSetGlobal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void jsSetGlobal(IntPtr es, string prop, long jsValue);

        [DllImport("node.dll", EntryPoint = "jsGC", CallingConvention = CallingConvention.Cdecl)]
        public static extern void jsGC();

        [DllImport("node.dll", EntryPoint = "jsIsJsValueValid", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsJsValueValid(IntPtr es, long jsValue);

        [DllImport("node.dll", EntryPoint = "jsIsValidExecState", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte jsIsValidExecState(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsDeleteObjectProp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void jsDeleteObjectProp(IntPtr es, long jsValue, string prop);

        [DllImport("node.dll", EntryPoint = "jsGetArrayBuffer", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsGetArrayBuffer(IntPtr es, long jsValue);

        [DllImport("node.dll", EntryPoint = "jsGetLastErrorIfException", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsGetLastErrorIfException(IntPtr es);

        [DllImport("node.dll", EntryPoint = "jsThrowException", CallingConvention = CallingConvention.Cdecl)]
        public static extern long jsThrowException(IntPtr es, [MarshalAs(UnmanagedType.LPArray)]byte[] utf8exception);

        [DllImport("node.dll", EntryPoint = "jsGetKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jsGetKeys(IntPtr es, long jsValue);
    }
}
