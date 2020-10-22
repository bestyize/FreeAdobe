using FreeAdobe.src;
using FreeAdobe.src.model;
using FreeAdobe.src.view;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreeAdobe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<PatchInfo> patchInfos;
        private List<AdobeProductBean> adobeProductBeans = new List<AdobeProductBean>();
        public MainWindow()
        {
            InitializeComponent();
            initView();
            startCheck();
        }

        private void initView() {
            
            patchInfos = AdobePatchUtil.loadAllProduct();
            HashSet<string> productSet = new HashSet<string>();
            foreach (PatchInfo info in patchInfos)
            {
                if (!productSet.Contains(info.Product + info.Version)) {
                    productSet.Add(info.Product+info.Version);
                    adobeProductBeans.Add(new AdobeProductBean(info.Product, info.Version, "resources/" + info.ProductName + ".png", info.ProductName + " " + info.Version, "", "https://www.baidu.com",info.LaunchPath));
                }
                
            }
            lbProduct.ItemsSource = adobeProductBeans;
        }

       

        private void btnPatch_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            AdobeProductBean bean = btn.DataContext as AdobeProductBean;
            if (bean != null) {
                if (!File.Exists(bean.LaunchPath))
                {
                    HelpWindow helpWindow = new HelpWindow("提示", "软件未安装，或者未安装在默认目录，请按照以下提示使用\n\n" +
                        "1、去Adobe官网下载CreativeCloud并且安装\n" +
                "2、通过CreativeCloud下载你想要的应用比如Photoshop（需要登陆CreativeCloud）\n" +
                "3、在本软件内找到你下载的版本，点击优化即可使用\n" +
                "4、注意不要更改adobe系列软件的安装目录\n" +
                "5、点击确定按钮去官网下载CreativeCloud", new HelperHandler("help"));
                    helpWindow.Show();
                }
                else
                {
                    try
                    {
                        bool succ = AdobePatchUtil.patchProduct(bean.Product, bean.Version);
                        if (succ)
                        {
                            alert(bean.Name + "优化成功，已经可以正常使用");
                        }
                        else
                        {
                            alert(bean.Name + "优化失败,可能已经优化过，请启动尝试。如果未优化成功请联系开发者反馈");
                        }
                    }
                    catch(Exception e1) {
                        alert(bean.Name + "发生未知错误，请联系开发者反馈:"+e1.ToString());
                    }
                    
                    
                }
            }
            
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            AdobeProductBean bean = btn.DataContext as AdobeProductBean;
            
            if (bean != null) {
                if (!File.Exists(bean.LaunchPath))
                {
                    HelpWindow helpWindow = new HelpWindow("提示", "软件未安装，或者未安装在默认目录，请按照以下提示使用\n\n1、去Adobe官网下载CreativeCloud并且安装\n" +
                "2、通过CreativeCloud下载你想要的应用比如Photoshop（需要登陆CreativeCloud）\n" +
                "3、在本软件内找到你下载的版本，点击优化即可使用\n" +
                "4、注意不要更改adobe系列软件的安装目录\n" +
                "5、点击确定按钮去官网下载CreativeCloud", new HelperHandler("help"));
                    helpWindow.Show();
                }
                else {
                    Process.Start(bean.LaunchPath);
                }
                
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MoveMainWindow(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private static void alert(string notice) {
            MessageBox.Show(notice,"提醒");
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            
            HelpWindow helpWindow = new HelpWindow("帮助", "使用方式如下:\n1、去Adobe官网下载CreativeCloud并且安装\n" +
                "2、通过CreativeCloud下载你想要的应用比如Photoshop（需要登陆CreativeCloud）\n" +
                "3、在本软件内找到你下载的版本，点击优化即可使用\n" +
                "4、注意不要更改adobe系列软件的安装目录\n" +
                "5、点击确定按钮去官网下载CreativeCloud", new HelperHandler("help"));
            helpWindow.Show();
        }

        

        class HelperHandler : NotifyEventListener
        {
            string eventType;

            public HelperHandler(string eventType)
            {
                this.eventType = eventType;
            }

            public void onCancelClicked(object msg)
            {
                if (eventType.Equals("verify")||eventType.Equals("enable")||eventType.Equals("update")) {
                    Environment.Exit(0);
                }
            }

            public void onOkClicked(object msg)
            {
                if (eventType.Equals("help"))
                {
                    Process.Start("https://creativecloud.adobe.com/apps/download/creative-cloud?locale=zh-cn");
                }
                else if (eventType.Equals("update")) {
                    Process.Start(startConfigBean.Download);
                }
                else if (eventType.Equals("enable"))
                {
                    Environment.Exit(0);
                }
                else if (eventType.Equals("verify"))
                {
                    string code=(string)msg;
                    if (!code.Equals("从来不想")) {
                        Environment.Exit(0);
                    }

                }

            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://bestyize.github.io/adobe/freeadobe/");
        }
        private static StartConfigBean startConfigBean;
        private void startCheck() {
            Task updateTask = new Task(() =>
            {
                startConfigBean = StartCheck.doCheck();
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (startConfigBean != null)
                    {
                        HelperHandler handler = new HelperHandler("null");
                        bool show = false;
                        if (startConfigBean.Enable.Equals("0"))
                        {
                            //弹窗暂停使用
                            handler = new HelperHandler("enable");
                            show = true;
                        }
                        else if (startConfigBean.Verify.Equals("1"))
                        {
                            //弹窗提示用户输入密码
                            handler = new HelperHandler("verify");
                            show = true;
                        }
                        else if (startConfigBean.Version.CompareTo("1.0") > 0)
                        {
                            //弹窗提示更新
                            handler = new HelperHandler("update");
                            show = true;

                        }
                        if (show) {
                            HelpWindow helpWindow = new HelpWindow(startConfigBean.Title, startConfigBean.Notice, handler);
                            helpWindow.Show();
                        }

                    }
                    else
                    {

                    }
                }));

            });
            updateTask.Start();
        }


    }

    
}
