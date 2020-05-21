﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosaDAL;
using RosaModel;
namespace RosaLogic
{
    public class MenuItem_Service
    {
        MenuItemDAO menuItemDAO = new MenuItemDAO();
        public List<MenuItem> GetMenuItem()
        {
            try
            {
                List<MenuItem> bars = menuItemDAO.Db_Get_All_MenuItem();
                return bars;
            }
            catch
            {
                return null;
         
               
            }
        }
        public void UpdateTableOrder(MenuItem table, int status)
        {
            try
            {
                menuItemDAO.UpdateTableOrder(table, status);
            }
            catch
            {
                throw new Exception("Could not update TableOrder");
            }
        }
        public MenuItem GetFromTableTheStatus(int status)
        {
            try
            {
                return menuItemDAO.GetFromTableTheStatus(status);
            }

            catch
            {
                return null;
            }
        }
    }
}
