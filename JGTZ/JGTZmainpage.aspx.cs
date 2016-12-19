using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JGTZ
{
    public partial class JGTZmainpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable table = TestTable();
                Bind_Tv(table, TreeView1.Nodes, null, "机构代码", "父机构", "机构名称");
            }
        }


        /// <summary>
        /// 绑定TreeView（利用TreeNodeCollection）
        /// </summary>
        /// <param name="tnc">TreeNodeCollection（TreeView的节点集合）</param>
        /// <param name="pid_val">父id的值</param>
        /// <param name="id">数据库 id 字段名</param>
        /// <param name="pid">数据库 父id 字段名</param>
        /// <param name="text">数据库 文本 字段值</param>
        private void Bind_Tv(DataTable dt, TreeNodeCollection tnc, string pid_val, string id, string pid, string text)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件
            string filter = string.IsNullOrEmpty(pid_val) ? pid + " is null" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView drv in dv)
            {
                tn = new TreeNode();//建立一个新节点（学名叫：一个实例）
                tn.Value = drv[id].ToString();//节点的Value值，一般为数据库的id值
                tn.Text = drv[text].ToString();//节点的Text，节点的文本显示
                tnc.Add(tn);//将该节点加入到TreeNodeCollection（节点集合）中
                Bind_Tv(dt, tn.ChildNodes, tn.Value, id, pid, text);//递归（反复调用这个方法，直到把数据取完为止）
            }
        }


        private DataTable TestTable()
        {
            string path = Server.MapPath("/App_Data/JGDB.accdb");
            string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {path};Persist Security Info=False;";
            OleDbConnection Conn = new OleDbConnection(ConnString);

            Conn.Open();
            string SQL = "select * from [JGXX]";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(SQL, Conn);
            adapter.SelectCommand = command;
            DataTable Dt = new DataTable();
            adapter.Fill(Dt);
            Conn.Close();

            DataColumn column = new DataColumn("父机构", typeof(string));
            Dt.Columns.Add(column);
            int count = Dt.Rows.Count;
            for(int i = 0; i < count; i++ )
            {
                switch(Dt.Rows[i]["机构代码"].ToString().Length)
                {
                    case 2:
                        break;
                    case 4:
                        Dt.Rows[i]["父机构"] = Dt.Rows[i]["机构代码"].ToString().Substring(0, 2);
                        break;
                    case 6:
                        Dt.Rows[i]["父机构"] = Dt.Rows[i]["机构代码"].ToString().Substring(0, 4);
                        break;
                    case 8:
                        Dt.Rows[i]["父机构"] = Dt.Rows[i]["机构代码"].ToString().Substring(0, 6);
                        break;

                    default:
                        break;
                }
            }

            return Dt;
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string jgdm = TreeView1.SelectedValue;
            string sql = $@"select RYMC from [RYXX] where JGDM = '{jgdm}'";

            string path = Server.MapPath("/App_Data/JGDB.accdb");
            string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {path};Persist Security Info=False;";
            OleDbConnection Conn = new OleDbConnection(ConnString);

            Conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(sql, Conn);
            adapter.SelectCommand = command;
            DataTable Dt = new DataTable();
            adapter.Fill(Dt);
            Conn.Close();

            DataView view = new DataView(Dt);
            ListBox1.DataSource = Dt;
            ListBox1.DataTextField = "RYMC";
            ListBox1.DataValueField = "RYMC";
            ListBox1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string rymc = ListBox1.SelectedItem.Value;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string sql = $@"select RYMC from [RYXX] where RYDM = '{TextBox1.Text}' or RYMC = '{TextBox2.Text}'";

            string path = Server.MapPath("/App_Data/JGDB.accdb");
            string ConnString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {path};Persist Security Info=False;";
            OleDbConnection Conn = new OleDbConnection(ConnString);

            Conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(sql, Conn);
            adapter.SelectCommand = command;
            DataTable Dt = new DataTable();
            adapter.Fill(Dt);
            Conn.Close();

            DataView view = new DataView(Dt);
            ListBox1.DataSource = Dt;
            ListBox1.DataTextField = "RYMC";
            ListBox1.DataValueField = "RYMC";
            ListBox1.DataBind();
        }
    }
}