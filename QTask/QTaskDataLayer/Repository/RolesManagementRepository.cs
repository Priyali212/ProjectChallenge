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
using System.Xml;

namespace QTaskDataLayer.Repository
{
    public class RolesManagementRepository
    {
        DB objDB;
        CommonRepository objComm;

        public RolesManagementRepository(IConfiguration _config)
        {
            objDB = new DB(_config);
            objComm = new CommonRepository(_config);

        }

        public List<RolesDBModel> GetRolesList()
        {
            DataTable dt = new DataTable();
            List<RolesDBModel> objLstRoles = new List<RolesDBModel>();
            try
            {
                dt = objDB.getDataFromDBToDataSet("Q_Pr_GetRoles", null).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RolesDBModel objRoles = new RolesDBModel();
                        objRoles.RolesId = dr["id"].ToString().Trim();
                        objRoles.DateEntered = Convert.ToDateTime(dr["CreateDate"].ToString().Trim());
                        //objRoles.DateModified = (dr["UpdatedDate"] != null && dr["UpdatedDate"].ToString().Trim() != "") ? Convert.ToDateTime(dr["UpdatedDate"].ToString().Trim()) : null;
                        objRoles.ModifiedBy = dr["UpdatedBy"].ToString().Trim();
                        objRoles.CreatedBy = dr["CreatedBy"].ToString().Trim();
                        objRoles.RoleName = dr["GroupName"].ToString().Trim();
                        objRoles.RoleDescriptioin = dr["Description"].ToString().Trim();
                        objRoles.Deleted = (dr["IsActive"] != null && dr["IsActive"].ToString().Trim() == "1") ? false : true;

                        objLstRoles.Add(objRoles);
                    }
                }
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "GetRolesList", ex.Message, "");
            }

            return objLstRoles;
        }

        public XmlDocument GetRoleWiseAccess(string RoleId)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNodeType xt = xDoc.NodeType;
            XmlElement SubRoot = xDoc.CreateElement("Roles");
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleId",RoleId)
                };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_RolesWiseAccessList", param);

                if (ds != null)
                {
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        XmlElement elmAccess = xDoc.CreateElement("AccessNames");
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            XmlElement elm = xDoc.CreateElement("Names");
                            elm.InnerText = dr["NAME"].ToString().Trim();
                            elmAccess.AppendChild(elm);
                        }
                        SubRoot.AppendChild(elmAccess);
                    }

                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        XmlElement elmAccess = xDoc.CreateElement("AccessTypes");
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            XmlElement elm = xDoc.CreateElement("Access");
                            elm.SetAttribute("accessid", dr["Accessid"].ToString().Trim());
                            if (dr["AccessName"].ToString().Trim().ToLower() == "enabled" || dr["AccessName"].ToString().Trim().ToLower() == "not set" || dr["AccessName"].ToString().Trim().ToLower() == "disabled")
                            {
                                elm.SetAttribute("type", "access");
                            }
                            else
                            {
                                elm.SetAttribute("type", "other");
                            }
                            elm.InnerText = dr["AccessName"].ToString().Trim();
                            elmAccess.AppendChild(elm);
                        }
                        SubRoot.AppendChild(elmAccess);
                    }

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        XmlElement elmAccess = xDoc.CreateElement("CategoryType");
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            XmlElement elm = xDoc.CreateElement("Category");
                            elm.InnerText = dr["Category"].ToString().Trim();
                            elmAccess.AppendChild(elm);
                        }
                        SubRoot.AppendChild(elmAccess);
                    }

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataView dv = new DataView(ds.Tables[0]);

                        DataTable dtDistinct = dv.ToTable(true, new string[] { "category" });

                        if (dtDistinct.Rows.Count > 0)
                        {
                            XmlElement elmAccess = xDoc.CreateElement("RoleWiseAccessType");
                            foreach (DataRow dr in dtDistinct.Rows)
                            {
                                XmlElement elmCategory = xDoc.CreateElement(dr["category"].ToString());

                                DataRow[] drCategory = ds.Tables[0].Select("category='" + dr["category"].ToString() + "'");
                                foreach (DataRow drSingle in drCategory)
                                {
                                    XmlElement elmAccessname = xDoc.CreateElement(drSingle["AccessName"].ToString());
                                    elmAccessname.SetAttribute("AccessId", drSingle["AccessId"].ToString());
                                    elmAccessname.InnerText = drSingle["Access"].ToString();
                                    elmCategory.AppendChild(elmAccessname);
                                }
                                elmAccess.AppendChild(elmCategory);
                            }
                            SubRoot.AppendChild(elmAccess);
                        }
                    }

                    xDoc.AppendChild(SubRoot);
                }
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "GetRoleWiseAccess", ex.Message, "");
            }

            return xDoc;
        }

        public RoleDBModalaccess GetRoleWiseAccessData(string RoleId)
        {
            RoleDBModalaccess objRoleModalaccess = new RoleDBModalaccess();

            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleId",RoleId)
                };
                DataSet ds = objDB.getDataFromDBToDataSet("Q_Pr_RolesWiseAccessList", param);

                if (ds != null)
                {
                    objRoleModalaccess.RoleId = Convert.ToInt32(RoleId);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objRoleModalaccess.lstRoleWiseAccessData = new List<RoleDBWiseAccessData>();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            RoleDBWiseAccessData objRoleWiseAccessData = new RoleDBWiseAccessData();
                            objRoleWiseAccessData.RoleId = dr["RoleID"].ToString();
                            objRoleWiseAccessData.ActionId = dr["AccessStatusId"].ToString();
                            objRoleWiseAccessData.RoleName = dr["RoleName"].ToString();
                            //objRoleWiseAccessData.accessOverride = dr["access_override"].ToString();
                            objRoleWiseAccessData.AccessName = dr["AccessName"].ToString();
                            objRoleWiseAccessData.AccessCategory = dr["PageName"].ToString();
                            //objRoleWiseAccessData.AclType = dr["Acc"].ToString();
                            objRoleWiseAccessData.Access = dr["Access"].ToString();
                            objRoleWiseAccessData.AccessId = dr["AccessId"].ToString();
                            objRoleWiseAccessData.PageId = dr["PageId"].ToString();

                            objRoleModalaccess.lstRoleWiseAccessData.Add(objRoleWiseAccessData);
                        }
                    }

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        List<RoleDBCategory> lstObjCategory = new List<RoleDBCategory>();
                        //objRoleModalaccess.lstCategory = new List<string>();

                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            RoleDBCategory objRoleDBCategory = new RoleDBCategory();

                            objRoleDBCategory.Id = Convert.ToInt32(dr["id"].ToString());
                            objRoleDBCategory.CategoryName = dr["category"].ToString();
                            lstObjCategory.Add(objRoleDBCategory);
                        }
                        objRoleModalaccess.lstCategory = lstObjCategory;
                    }

                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        objRoleModalaccess.lstRoleAccess = new List<RoleDBModalAccessName>();

                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            RoleDBModalAccessName objRoleModalAccessName = new RoleDBModalAccessName();
                            objRoleModalAccessName.AccessId = Convert.ToInt32(dr["id"].ToString());
                            objRoleModalAccessName.AccessName = dr["AccessStatusName"].ToString();

                            //if(Convert.ToInt32(dr["Accessid"].ToString())==0 || Convert.ToInt32(dr["Accessid"].ToString())==89 || Convert.ToInt32(dr["Accessid"].ToString())==-98)
                            //{
                            //    objRoleModalAccessName.AccessFor = "access";
                            //}
                            //else
                            //{
                            //    objRoleModalAccessName.AccessFor = "other";
                            //}

                            objRoleModalaccess.lstRoleAccess.Add(objRoleModalAccessName);
                        }

                    }

                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        List<RoleDBAccessType> lstAccess = new List<RoleDBAccessType>();
                        // objRoleModalaccess.lstAccessType = new List<RoleDBCategory>();
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            RoleDBAccessType objroleDBAccess = new RoleDBAccessType();
                            objroleDBAccess.Id = Convert.ToInt32(dr["id"].ToString());
                            objroleDBAccess.AccessTypeName = dr["AccessName"].ToString();

                            lstAccess.Add(objroleDBAccess);
                        }
                        objRoleModalaccess.lstAccessType = lstAccess;

                    }
                }
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "GetRoleWiseAccessData", ex.Message, "");
            }

            return objRoleModalaccess;
        }

        public List<RolesWiseUserListDBModel> GetRoleWiseUserList(string RoleName = null)
        {
            List<RolesWiseUserListDBModel> lstRoleWiseAccess = new List<RolesWiseUserListDBModel>();
            DataTable dt = null;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleName",RoleName)
                };

                dt = objDB.getDataFromDBToDataSet("Q_Pr_GetRolesUserList", param).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RolesWiseUserListDBModel objRoleWise = new RolesWiseUserListDBModel();

                        objRoleWise.RoleId = dr["RoleId"].ToString().Trim();
                        objRoleWise.Userid = dr["UserId"].ToString().Trim();
                        objRoleWise.FirstName = dr["first_name"].ToString().Trim();
                        objRoleWise.LastName = dr["last_name"].ToString().Trim();
                        objRoleWise.RoleName = dr["name"].ToString().Trim();
                        objRoleWise.RoleDescriptioin = dr["RoleDescription"].ToString().Trim();

                        lstRoleWiseAccess.Add(objRoleWise);
                    }
                }
            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "GetRoleWiseUserList", ex.Message, "");
            }

            return lstRoleWiseAccess;
        }

        public bool SaveRoleWiseAccess(RoleWiseAccessesDB objRoleWiseAccess, string IPAddress, string BrowserDetails)
        {
            bool status = false;

            try
            {
                if (objRoleWiseAccess != null)
                {


                    if (objRoleWiseAccess != null && objRoleWiseAccess.lstRoleDBWiseAccess != null && objRoleWiseAccess.lstRoleDBWiseAccess.Count > 0)
                    {
                        string UserName = objRoleWiseAccess.UserName;
                        string RoleId = objRoleWiseAccess.RoleId;

                        string AccessDetails = string.Empty;

                        foreach (RoleDBWiseAccessData objRoleData in objRoleWiseAccess.lstRoleDBWiseAccess)
                        {
                            AccessDetails += objRoleData.RoleId.ToString() + "|*accessid*|" + objRoleData.AccessId.ToString() + "|*statusid*|" + objRoleData.ActionId.ToString() + "|*pageid*|" + objRoleData.PageId.ToString() + "|*NXT*|";
                        }


                        SqlParameter[] param = new SqlParameter[]
                        {
                        new SqlParameter("@RoleId",objRoleWiseAccess.RoleId),
                        new SqlParameter("@UserName",objRoleWiseAccess.UserName),
                        new SqlParameter("@AccessDetails",AccessDetails),
                        new SqlParameter("@IPAddress",IPAddress),
                        new SqlParameter("@BrowserDetails",BrowserDetails)
                        };
                        int Res = 0;

                        Res = objDB.InsertData("Q_Pr_RoleWiseAccess", param);

                        if (Res > 0)
                            status = true;
                    }
                }

            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "SaveRoleWiseAccess", ex.Message, "");
            }
            return status;
        }

        public bool SaveRole(RoleDB objRoleDB, string UserName, string IPAddress, string BrowserDetails)
        {
            bool Status = false;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleName",objRoleDB.RoleName),
                    new SqlParameter("@RoleDescription",objRoleDB.Description),
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@IPAddress",IPAddress),
                    new SqlParameter("@BrowserDetails",BrowserDetails)
                };

                int Res = objDB.InsertData("Q_Pr_SaveGroupDetails", param);
                if (Res > 0)
                    Status = true;
                else
                    Status = false;

            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "SaveRole", ex.Message, "");
            }
            return Status;
        }

        public bool DeleteRole(string RoleId, string UserName, string IPAddress, string BrowserDetails)
        {
            bool Status = false;
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@IPAddress",IPAddress),
                    new SqlParameter("@BrowserDetails",BrowserDetails)
                };

                int Res = objDB.DeleteData("Q_Pr_DeleteRole", param);
                if (Res > 0)
                    Status = true;
                else
                    Status = false;

            }
            catch (Exception ex)
            {
                objComm.SaveErrorLog("RolesManagementRepository", "DeleteRole", ex.Message, "");
            }
            return Status;
        }
    }
}
