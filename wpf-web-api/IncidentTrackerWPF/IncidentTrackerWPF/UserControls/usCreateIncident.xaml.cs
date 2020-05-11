using IncidentTrackerWPF.BusinessObject;
using IncidentTrackerWPF.EntityObject;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for usCreateIncident.xaml
    /// </summary>
    public partial class usCreateIncident : UserControl
    {
        private TrackerProxy _proxy = new TrackerProxy();
        private int IncidentID = 0;

        public usCreateIncident()
        {
            InitializeComponent();
            BindLocation();
            txtAddEditIncident.Text = "Add Incident";
        }
        public usCreateIncident(int _IncID)
        {
            InitializeComponent();
            txtAddEditIncident.Text = "Edit Incident";
            IncidentID = _IncID;
            BindLocation();
            FillIncident(_IncID);
        }

        private void BindLocation()
        {
            var _locations = Task.Run(() => _proxy.GetAllLocation()).Result;

            cmbLocation.ItemsSource = _locations;
            cmbLocation.DisplayMemberPath = "LocationName";
            cmbLocation.SelectedValuePath = "LocationID";
        }

        private void FillIncident(int _IncID)
        {
            var result = Task.Run(() => {return _proxy.GetIncident(_IncID); }).Result;

            txtTitle.Text = result.Title;
            txtDetail.Text = result.Detail;
            txtDate.Text = Convert.ToString(result.IncidentDateTime);
            cmbLocation.SelectedValue = result.LocationID;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackToGrid();
        }

        private void BackToGrid()
        {
            ((StackPanel)this.Parent).Children.Add(new ucIncidentGrid());
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtDetail.Text) || string.IsNullOrEmpty(txtDate.Text))
                MessageBox.Show("Fill required data!");
            else
            {
                var _incident = new Incident();
                _incident.Title = txtTitle.Text;
                _incident.Detail = txtDetail.Text;
                _incident.IncidentDateTime = Convert.ToDateTime(txtDate.Text);
                _incident.LocationID = Convert.ToInt32(cmbLocation.SelectedValue);

                if (IncidentID > 0)
                {
                    _incident.IncidentID = IncidentID;
                    var result = Task.Run(() => { return _proxy.UpdateIncident(_incident); }).Result;
                    if (result)
                        MessageBox.Show("Incident updated successfully!");
                }
                else
                {
                    var result = Task.Run(() => { return _proxy.AddIncident(_incident); }).Result;
                    if (result)
                        MessageBox.Show("Incident added successfully!");
                }
                BackToGrid();
            }

        }
    }
}
