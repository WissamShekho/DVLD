using DVLD.People.PersonControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class ShowPersonInfoForm : Form
    {
        public static Action DataUpdated;
        private int _PersonID = -1;
        public ShowPersonInfoForm(int PersonID)
        {
            _PersonID = PersonID;
            InitializeComponent();
        }
        private void ShowPersonDetailsForm_Load(object sender, EventArgs e)
        {
            ctrlPersonCard.DataUpdated += _dataBack;
            ctrlPersonCard1.LoadPersonInfo(_PersonID);
        }

        private void _dataBack()
        {
            DataUpdated?.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
            {
                this.Close();
            }
    }
}
