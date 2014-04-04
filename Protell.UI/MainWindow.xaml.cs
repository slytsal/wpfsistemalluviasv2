using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace Protell.UI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dTimerUploadProcess;
        Storyboard _ImgSync;
        public DispatcherTimer DTimerUploadProcess
        {
            get { return dTimerUploadProcess; }
            set { dTimerUploadProcess = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            int SyncTime = Int32.Parse(ConfigurationManager.AppSettings["SyncTime"].ToString());
            int SyncTimeSol = Int32.Parse(ConfigurationManager.AppSettings["SyncTimeSol"].ToString());
            this._ImgSync = (Storyboard)this.FindResource("rotateImg");

            DTimerUploadProcess = new DispatcherTimer();
            DTimerUploadProcess.Tick += new EventHandler(DTimerUploadProcess_Tick);
            DTimerUploadProcess.Interval = new TimeSpan(0, 0, SyncTime);
            DTimerUploadProcess.Start();
            NameUser();
            GetVersion();
        }

        private void imgSyncFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DTimerUploadProcess_Tick(null, new EventArgs());
        }

        void DTimerUploadProcess_Tick(object sender, EventArgs e)
        {
            //Condicionar catsync
            try
            {
                //if (!UploadProcessViewModel.IsRunning)
                //{
                //    UploadProcessViewModel vm = new UploadProcessViewModel();
                //    vm.PropertyChanged += delegate(object sndr, PropertyChangedEventArgs args)
                //    {
                //        if (args.PropertyName.ToLower() == "jobdone")
                //        {
                //            if (!((UploadProcessViewModel)sndr).JobDone)
                //            {
                //                Action a = () => this.ShowImgSync();
                //                this.Dispatcher.BeginInvoke(a);
                //            }
                //            else
                //            {
                //                Action a = () => this.HideImgSync();
                //                this.Dispatcher.BeginInvoke(a);
                //            }
                //        }

                //        if (args.PropertyName.ToLower() == "message")
                //        {
                //            Action a = () => this.SetImgSyncMsg(((UploadProcessViewModel)sndr).Message);
                //            this.Dispatcher.BeginInvoke(a);
                //        }
                //    };
                //    vm.start();
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        public void ShowImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Visible;
            this.cnvTmpRot2.Visibility = Visibility.Collapsed;
            _ImgSync.Begin(this);
        }

        public void HideImgSync()
        {
            this.cnvTmpRot.Visibility = Visibility.Collapsed;
            this.cnvTmpRot2.Visibility = Visibility.Visible;
        }

        public void SetImgSyncMsg(string msg)
        {
            this.imgSyncFiles.ToolTip = msg;
            this.imgSyncFiles2.ToolTip = msg;
        }

        public void CallNew()
        {
            ContentControl cc = null;
            try
            {
                
                cc = this.FindName("ContentPaneSubMenu") as ContentControl;
                IContentControl view = cc.Content as IContentControl;
                view.Nuevo();
            }
            catch (Exception)
            {

                ;
            }

        }

        public void NameUser()
        {
            //NOMBRE DE LA MAQUINA
            System.Security.Principal.WindowsIdentity usr = System.Security.Principal.WindowsIdentity.GetCurrent();
            this.nameUser.Content = usr.Name;
        }

        public void GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            this.txtBlockVer.Text = fvi.ProductVersion.ToString();
        }
    }
}
