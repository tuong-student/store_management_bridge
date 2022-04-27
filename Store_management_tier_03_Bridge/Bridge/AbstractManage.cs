using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using Store_management_tier_03_Bridge.BSLayer;

namespace Store_management_tier_03_Bridge.Bridge
{
    public class AbstractManage
    {
        BusinessLayer BSLayer;

        public AbstractManage(BusinessLayer bslayer)
        {
            this.BSLayer = bslayer;
        }

        public DataSet Select()
        {
            return BSLayer.Select();
        }

        public void Insert(InfoHolder info)
        {
            BSLayer.Insert(info);
        }

        public void Update(InfoHolder info)
        {
            BSLayer.Update(info);
        }

        public void Delete(InfoHolder info)
        {
            BSLayer.Delete(info);
        }

        public void LoadData(DataGridView dgv)
        {
            BSLayer.LoadData(dgv);
        }
    }
}
