using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemInfo
{
    class GetMainInfo
    {
        public static void GetHardWareInfo(string key, ListView list)
        {
            list.Items.Clear();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {key}");

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    ListViewGroup listViewGroup;

                    try
                    {
                        listViewGroup = list.Groups.Add(obj["Name"].ToString(), obj["Name"].ToString());
                    }
                    catch
                    {
                        listViewGroup = list.Groups.Add(obj.ToString(), obj.ToString());
                    }

                    if (obj.Properties.Count == 0)
                    {
                        MessageBox.Show("Не удалось получить информацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    foreach (PropertyData data in obj.Properties)
                    {
                        ListViewItem item = new ListViewItem(listViewGroup);

                        item.Text = data.Name;

                        if (data.Value != null && !string.IsNullOrEmpty(data.Value.ToString()))
                        {
                            item.SubItems.Add(data.Value.ToString());
                            list.Items.Add(item);
                        }
                    }


                }
            }
            catch { }
        }



    }
}
