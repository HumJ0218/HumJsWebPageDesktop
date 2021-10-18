using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows;

namespace HumJsWebPageDesktop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ScriptJs = "script.js";
        public const string MessageDirectory = "./Message";

        public MainWindow()
        {
            Directory.CreateDirectory(MessageDirectory);
            InitializeComponent();
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
        }

        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            Title = webView.CoreWebView2.DocumentTitle;
        }

        /// <summary>
        /// 设置状态栏文字
        /// </summary>
        public void SetStatus(string content)
        {
            StatusShadow1.Content = content;
            StatusShadow2.Content = content;
            StatusShadow3.Content = content;
            StatusShadow4.Content = content;
            StatusShadow5.Content = content;
            StatusShadow6.Content = content;
            StatusShadow7.Content = content;
            StatusShadow8.Content = content;
            Status.Content = content;
        }

        /// <summary>
        /// WebView2 开始导航，导航结果为网络请求。在事件期间，主机可能会禁止该请求。
        /// </summary>
        private void WebView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            SetStatus(nameof(WebView_NavigationStarting));

            webView.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// WebView2 的源将更改到新的 URL。该事件可能由不会导致网络请求（如片段导航）的导航操作导致。
        /// </summary>
        private void WebView_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            SetStatus(nameof(WebView_SourceChanged));
        }

        /// <summary>
        /// WebView 开始加载新页面的内容。
        /// </summary>
        private void WebView_ContentLoading(object sender, CoreWebView2ContentLoadingEventArgs e)
        {
            SetStatus(nameof(WebView_ContentLoading));
        }

        /// <summary>
        /// WebView2 完成新页面上的内容加载。
        /// </summary>
        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            SetStatus(nameof(WebView_NavigationCompleted));

            if (File.Exists(ScriptJs))
            {
                webView.CoreWebView2.ExecuteScriptAsync(File.ReadAllText(ScriptJs));
            }

            webView.Visibility = Visibility.Visible;
        }

        private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var now = DateTime.Now;
            var source = e.Source;
            var json = e.WebMessageAsJson;
            var message = e.TryGetWebMessageAsString();

            File.AppendAllLines($"{MessageDirectory}{now:yyyyMMdd}.log", new[] {
                now.ToString().PadRight(32, '-'),
                source,
                json,
                message,
                "".PadRight(32, '-')
            });
        }
    }
}