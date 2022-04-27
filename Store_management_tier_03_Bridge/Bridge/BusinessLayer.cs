using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using Store_management_tier_03_Bridge.BSLayer;

namespace Store_management_tier_03_Bridge.Bridge
{
    public interface BusinessLayer
    {
        public DataSet Select();
        public void Insert(InfoHolder info);
        public void Update(InfoHolder info);
        public void Delete(InfoHolder info);
        public void LoadData(DataGridView dgv);
    }
}
