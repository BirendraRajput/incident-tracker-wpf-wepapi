using IncidentTrackerWPF.BusinessObject;
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

namespace IncidentTrackerWPF.UserControls
{
    /// <summary>
    /// Interaction logic for ucIncidentGrid.xaml
    /// </summary>
    public partial class ucIncidentGrid : UserControl
    {
        private TrackerProxy _proxy = new TrackerProxy();
        public ucIncidentGrid()
        {
            InitializeComponent();
            BindDataGrid();
        }

        private void BindDataGrid()
        {
            var _incidents = Task.Run(() => _proxy.GetAllIncident()).Result;
            var _locations = Task.Run(() => _proxy.GetAllLocation()).Result;

            var incidents = from inc in _incidents
                            join loc in _locations on inc.LocationID equals loc.LocationID
                            into t
                            from incGrp in t.DefaultIfEmpty()
                            select new
                            {
                                IncidentID = inc.IncidentID,
                                Title = inc.Title,
                                Detail = inc.Detail,
                                IncidentDateTime = inc.IncidentDateTime,
                                LocationName = incGrp != null ? incGrp?.LocationName : "Unknown"
                            };

            dataGrid.ItemsSource = incidents;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(Convert.ToString(((Button)sender).CommandParameter), out int _IncID);

            ((StackPanel)this.Parent).Children.Add(new usCreateIncident(_IncID));
            ((StackPanel)this.Parent).Children.Remove(this);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(Convert.ToString(((Button)sender).CommandParameter), out int _IncID);
            
            if (MessageBox.Show("Are you sure", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                bool _status = Task.Run(() => {
                  return  _proxy.DeleteIncident(_IncID); }).Result;

                BindDataGrid();

                if (_status)
                    MessageBox.Show($"Incident Id: {_IncID} deleted Successfully!");
                else
                    MessageBox.Show($"Incident Id: {_IncID} not deleted!");
            }          
        }       

        private void btnAddIncident_Click(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Add(new usCreateIncident());
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
