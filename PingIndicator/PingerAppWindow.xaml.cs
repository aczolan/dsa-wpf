using System;
using System.Collections.Generic;
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
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Threading;

namespace PingIndicator
{
    /// <summary>
    /// Interaction logic for PingIndicatorWindow.xaml
    /// </summary>
    public partial class PingerAppWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region ScrollViewer behavior
        private bool m_autoScroll = true;
        private void MainScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scr = sender as ScrollViewer;

            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (scr.VerticalOffset == scr.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    m_autoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    m_autoScroll = false;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (m_autoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                scr.ScrollToVerticalOffset(scr.ExtentHeight);
            }
        }
        #endregion

        private string m_outputString = "Begin\n";
        public string OutputString
        {
            get => m_outputString;
            set
            {
                m_outputString = value;
                OnPropertyChanged("OutputString");
            }
        }
        
        private void indicatePingSuccess()
        {
            PingIndicator.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void printMessage(string msg)
        {
            OutputString += msg + "\n";
        }

        private void pingAddrWithMessage(string address, string messagePrefix = "")
        {
            long ms = -1;
            Ping pinger = null;
            string message;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(address);

                ms = reply.RoundtripTime;

                //Successful
                indicatePingSuccess();
                string timestamp = DateTime.Now.ToLongTimeString();
                message = "[" + timestamp + "]: " + messagePrefix + "Pinged " + address + ", response " + ms + " ms";
            }
            catch (Exception ex)
            {
                //Unsuccessful
                string timestamp = DateTime.Now.ToLongTimeString();
                if (address == "")
                {
                    address = "[n/a]";
                }
                message = "[" + timestamp + "]: " + messagePrefix + "Could not ping " + address;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            printMessage(message);
        }

        public PingerAppWindow()
        {
            InitializeComponent();
            DataContext = _this;
        }

        private void PingOnce_Click(object sender, RoutedEventArgs e)
        {
            string address = AddressBox.Text;
            pingAddrWithMessage(address);
        }

        private void PingXTimes_Click(object sender, RoutedEventArgs e)
        {
            string address = AddressBox.Text;
            if (
                Int32.TryParse(PingNumberBox.Text, out int numPings) &&
                Int32.TryParse(PingIntervalBox.Text, out int pingInterval)
                )
            {
                for (int i = 0; i < numPings; i++)
                {
                    string prefix = "(" + (i + 1) + "/" + numPings + ")";
                    pingAddrWithMessage(address, prefix);
                    Thread.Sleep(pingInterval);
                }
            }
            else
            {
                printMessage("Error: invalid ping interval or number of pings");
            }
        }
    }
}
