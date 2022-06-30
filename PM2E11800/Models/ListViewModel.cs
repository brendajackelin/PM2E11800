using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E11800.Models
{
    internal class ListViewModel
    {
        #region Attributes
        public object listViewSource;
        #endregion

        #region Properties
        public object ListViewSource { get { return listViewSource; } set { listViewSource = value; } }
                                       //set { setValue(ref listViewSource, value);  } }
        #endregion

        #region Methods
        public async Task LoadData()
        {
            ListViewSource = await App.BD.ObtenerListaUbicaciones();
        }
        #endregion

        #region Constructor
        public ListViewModel()
        {
            LoadData();
        }
        #endregion
    }

}
