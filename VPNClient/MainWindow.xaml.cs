using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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

namespace VPNClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrintPortInfos();
            onPortsUpdate += PrintPortInfos;
            thread = new Thread(CheckPortChanges);
            thread.Start();
        }

        private void CheckPortChanges()
        {
            while(true)
            {
                Thread.Sleep(4000);
                Dispatcher.BeginInvoke(onPortsUpdate);
            }  
        }

        private void PrintPortInfos()
        {
            listview_scaner.ItemsSource = GetOpenPort();
        }

        private delegate void PrintPointer();
        private event PrintPointer onPortsUpdate;

        private static List<PortInfo> GetOpenPort()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            IPEndPoint[] udpEndPoints = properties.GetActiveUdpListeners();

            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

            
            UdpStatistics udpStatistics = properties.GetUdpIPv4Statistics();
            List<PortInfo> portInfos = tcpConnections.Select(p =>
            {
                return new TcpPortInfo(
                    p.LocalEndPoint.Port,
                    p.LocalEndPoint.Address.ToString(),
                    p.RemoteEndPoint.Address + ":" + p.RemoteEndPoint.Port,
                    p.State.ToString());
            }).Cast<PortInfo>().ToList();

            portInfos.AddRange(udpEndPoints.Select(p =>
            {
                return new UdpPortInfo(p.Port);
            }));

            return portInfos;
        }
        
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            thread.Abort();
            thread.Join(500);
        }

        private void GridTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
