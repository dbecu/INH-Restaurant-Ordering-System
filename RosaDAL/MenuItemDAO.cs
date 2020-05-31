﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosaModel;
using System.Configuration;
namespace RosaDAL
{
    public class MenuItemDAO : Base
    {
        public List<MenuItem> Db_Get_AllOrders()
        {
            string query = "SELECT [orderDate], [table_id], [amount], [itemName], [status], [notes],  [menuCategory_id], [orderitems].[order_id] FROM [orderItems] JOIN [order] ON orderItems.order_id = [order].order_id JOIN [menuItem] ON orderItems.menuItem_id = menuItem.menuItem_id;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public MenuItem GetFromTableTheStatus(int status)
        {
            SqlCommand cmd = new SqlCommand("SELECT [orderDate], [table_id], [amount], [itemName], [status], [notes], [menuCategory_id], [orderitems].[order_id] FROM [orderitems] JOIN [order] ON orderItems.order_id = [order].order_id JOIN [menuItem] ON orderItems.menuItem_id = menuItem.menuItem_id WHERE [status] = @status", conn);
            cmd.Parameters.AddWithValue("@status", status);
            SqlDataReader reader = cmd.ExecuteReader();
            MenuItem temp = null;
            if (reader.Read())
            {
                temp = ReadTable(reader);
            }
            return temp;
        }
        public void UpdateTableOrder(int id, int status)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [orderItems] SET status = @status WHERE order_id = @orderid;", conn);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@orderid", id);
            cmd.ExecuteReader();
        }
        private MenuItem ReadTable(SqlDataReader reader)
        {
            MenuItem itemtemp = new MenuItem()
            {
                // MenuItem Class
                Name = (string)reader["itemName"],
                menuCat = (int)reader["menuCategory_id"],

                //Order class
                order = new Order()
                {
                    table = (int)reader["table_id"],
                    dateTime = DateTime.Parse(reader["orderDate"].ToString()),
                    notes = (string)reader["notes"]
                },

                //OrderItem class
                orderItem = new OrderItem()
                {
                    amount = (int)reader["amount"],
                    orderID = (int)reader["order_id"],
                    status = (StatusEnum)(int)reader["status"]
                }
            };
            return itemtemp;
        }
        private List<MenuItem> ReadTables(DataTable dataTable)
        {
            List<MenuItem> menuItem = new List<MenuItem>();

            foreach (DataRow dr in dataTable.Rows)
            {
                MenuItem temp = new MenuItem()
                {
                    // MenuItem Class
                    Name = (string)dr["itemName"],
                    menuCat = (int)dr["menuCategory_id"],

                    //Order class
                    order = new Order()
                    {
                        table = (int)dr["table_id"],
                        dateTime = DateTime.Parse(dr["orderDate"].ToString())
                    },

                    //OrderItem class
                    orderItem = new OrderItem()
                    {
                        amount = (int)dr["amount"],
                        orderID = (int)dr["order_id"],
                        status = (StatusEnum)(int)dr["status"]
                    }

                };
                if (dr.IsNull("notes"))
                {
                    temp.order.notes = "none";
                }
                else
                {
                    temp.order.notes = (string)dr["notes"];
                }

                menuItem.Add(temp);
            }

            return menuItem;
        }
    }
}
