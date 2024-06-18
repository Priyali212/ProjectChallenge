using Microsoft.Extensions.Configuration;
using QTaskDataLayer.DBModel;
using QTaskDataLayer.DBOperations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QTaskDataLayer.Repository
{
    public class ContactRepository
    {
        DB objDB;
        CommonRepository objComm;
        public ContactRepository(IConfiguration _config)
        {
            objDB = new DB(_config);
            objComm = new CommonRepository(_config);
		}

        public ContactListDBModel GetUserList(string Name, int PageIndex = 0, int PageSize = 10)
        {
            ContactListDBModel objContact = new ContactListDBModel();
            objContact.contactLists = new List<ContactListDB>();
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@Name",Name),
                    new SqlParameter("@PageIndex",PageIndex),
                    new SqlParameter("@PageSize",PageSize)
                };

                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_GetQTaskUser", param);

                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objContact.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().Trim());
                        objContact.PageIndex = PageIndex;

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                ContactListDB objlstContact = new ContactListDB();

                                objlstContact.ID = dr["ID"].ToString().Trim();
                                objlstContact.UserName = dr["UserName"].ToString().Trim();
                                objlstContact.UserFullName = dr["UserFullName"].ToString().Trim();
                                objlstContact.IsAdmin = (dr["IsAdmin"] != null && dr["IsAdmin"].ToString().Trim() == "1") ? true : false;
                                objlstContact.GroupName = dr["GroupName"].ToString().Trim();

                                objContact.contactLists.Add(objlstContact);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("ContactRepository", "GetUserList", ex.Message, "");
            }

            return objContact;
        }

        public bool DeleteUsers(int Id, String DeletedBy)
        {
            bool result = false;
            try
            {


                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@UserId",Id),
                        new SqlParameter("@deletedBy",DeletedBy)
                };

                int Res = objDB.DeleteData("Q_Pr_Task_DeactivateUser", param);

                if (Res > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("ContactRepository", "GetUserList", ex.Message, DeletedBy);
            }

            return result;
        }

        public bool CreateUsers(CreateUserDBModal objCreateUser, string AddedBy, string IPAddress, string BrowserDetails)
        {
            bool result = false;
            try
            {

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserId",objCreateUser.ID),
                    new SqlParameter("@UserName",objCreateUser.UserName),
                    new SqlParameter("@UserFullName",objCreateUser.UserFullName),
                    new SqlParameter("@GroupId",objCreateUser.GroupId),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@IPAddress",IPAddress),
                    new SqlParameter("@BrowserDetails",BrowserDetails)
                };

                int Count = objDB.InsertData("Q_Pr_SaveTaskUserDetails", param);

                if (Count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("ContactRepository", "CreateUsers", ex.Message, "");
            }
            return result;
        }

        public CreateUserDBModal GetUserDetails(string Id)
        {
            CreateUserDBModal objCreateUser = new CreateUserDBModal();
            DataSet ds = null;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@Id",Id)
                };

                ds = objDB.getDataFromDBToDataSet("Q_Pr_GetQTaskUser", param);

                if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        objCreateUser.ID = dr["id"].ToString().Trim();
                        objCreateUser.UserName = dr["UserName"].ToString().Trim();
                        objCreateUser.UserFullName = dr["UserFullName"].ToString().Trim();
                        objCreateUser.IsAdmin = (dr["IsAdmin"] != null && dr["IsAdmin"].ToString().Trim() == "1") ? true : false;
                        objCreateUser.GroupId = (dr["GroupId"] != null && dr["GroupId"].ToString().Trim() != "") ? Convert.ToInt32(dr["GroupId"].ToString().Trim()) : 0;
                    }
                }


            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("ContactRepository", "GetUserDetails", ex.Message, "");
            }
            return objCreateUser;
        }

        public bool UpdateUsers(CreateUserDBModal objCreateUser, string AddedBy, string IPAddress, string BrowserDetails)
        {
            bool result = false;
            try
            {

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserId",objCreateUser.ID),
                    new SqlParameter("@UserName",objCreateUser.UserName),
                    new SqlParameter("@UserFullName",objCreateUser.UserFullName),
                    new SqlParameter("@GroupId",objCreateUser.GroupId),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@IPAddress",IPAddress),
                    new SqlParameter("@BrowserDetails",BrowserDetails)
                };

                int Count = objDB.InsertData("Q_Pr_SaveTaskUserDetails", param);

                if (Count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("ContactRepository", "UpdateUsers", ex.Message, "");
            }
            return result;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)


          .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
