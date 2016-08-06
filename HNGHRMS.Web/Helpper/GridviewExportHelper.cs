using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
namespace HNGHRMS.Web.Helpper
{
    public class GridviewExportHelper
    {
     
        public static GridViewSettings NewEmployeeGridViewExcelExportSettings(string CompanyName)
        {
            GridViewSettings gridviewSettings = new GridViewSettings();
            gridviewSettings.Name = "NewEmployeeReport";
            // gridviewSettings.CallbackRouteValues = new { Controller = "Home", Action = "NewEmployeeReportGridView" };
            gridviewSettings.KeyFieldName = "Id";
            gridviewSettings.Columns.Add("Id").Caption = "Mã Nhân viên";
            gridviewSettings.Columns.Add("FullName").Caption = "Họ tên";
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Gender";
                col.Caption = "Giới tính";
                col.ColumnType = MVCxGridViewColumnType.ComboBox;
                ComboBoxProperties comboBoxPro = col.PropertiesEdit as ComboBoxProperties;
                comboBoxPro.DataSource = new SelectList(new[]{
                        new SelectListItem{Text = "Nam",Value=HNGHRMS.Model.Enums.Gender.Male.ToString()},
                        new SelectListItem{Text = "Nữ", Value=HNGHRMS.Model.Enums.Gender.Female.ToString()}
                    }, "Value", "Text");
            });
            gridviewSettings.Columns.Add("Nationality").Caption ="Quốc tịch";
            gridviewSettings.Columns.Add("CompanyName").Caption = "Công ty";
            gridviewSettings.Columns.Add("PositionName").Caption = "Chức vụ";
            gridviewSettings.Columns.Add("Departement").Caption = "Phòng ban";
            gridviewSettings.Columns.Add(col => {
                col.FieldName = "JoinedDate";
                col.Caption = "Ngày vào làm";                
                col.ColumnType = MVCxGridViewColumnType.DateEdit;
                col.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });
            gridviewSettings.Columns.Add(col => {
                col.FieldName = "Salary";
                col.Caption = "Mức lương";
                col.PropertiesEdit.DisplayFormatString = "c0";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Status";
                col.Caption = "Trạng thái";
                col.ColumnType = MVCxGridViewColumnType.ComboBox;
                ComboBoxProperties comboBoxPro = col.PropertiesEdit as ComboBoxProperties;
                comboBoxPro.DataSource = new SelectList(new[]{
                        new SelectListItem{Text = "Tuyển dụng",Value=HNGHRMS.Model.Enums.EmployeeStatus.Present.ToString()},
                        new SelectListItem{Text = "Điều chuyển", Value=HNGHRMS.Model.Enums.EmployeeStatus.Transfer.ToString()}
                    }, "Value", "Text");
            });
            return gridviewSettings;
        }

        public static GridViewSettings TerminatedEmployeeGridViewExcelExportSettings(string CompanyName)
        {
            GridViewSettings gridviewSettings = new GridViewSettings();
            gridviewSettings.Name = "TerminatedEmployeeReport";
            // gridviewSettings.CallbackRouteValues = new { Controller = "Home", Action = "NewEmployeeReportGridView" };
            gridviewSettings.KeyFieldName = "EmployeeId";
            gridviewSettings.Columns.Add("EmployeeId").Caption = "Mã Nhân viên";
            gridviewSettings.Columns.Add("FullName").Caption = "Họ tên";
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Gender";
                col.Caption = "Giới tính";
                col.ColumnType = MVCxGridViewColumnType.ComboBox;
                ComboBoxProperties comboBoxPro = col.PropertiesEdit as ComboBoxProperties;
                comboBoxPro.DataSource = new SelectList(new[]{
                        new SelectListItem{Text = "Nam",Value=HNGHRMS.Model.Enums.Gender.Male.ToString()},
                        new SelectListItem{Text = "Nữ", Value=HNGHRMS.Model.Enums.Gender.Female.ToString()}
                    }, "Value", "Text");
            });
            gridviewSettings.Columns.Add("Nationality").Caption = "Quốc tịch";
            gridviewSettings.Columns.Add("CompanyName").Caption = "Công ty";
            gridviewSettings.Columns.Add("PositionName").Caption = "Chức vụ";
            gridviewSettings.Columns.Add("Departement").Caption = "Phòng ban";
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "JoinedDate";
                col.Caption = "Ngày vào làm";
                col.ColumnType = MVCxGridViewColumnType.DateEdit;
                col.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "TerminationDate";
                col.Caption = "Ngày thôi việc";
                col.ColumnType = MVCxGridViewColumnType.DateEdit;
                col.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Salary";
                col.Caption = "Mức lương";
                col.PropertiesEdit.DisplayFormatString = "c0";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Reason";
                col.Caption = "Lý do thôi việc";             
            });
            return gridviewSettings;
        }

        public static GridViewSettings TransferEmployeeGridViewExcelExportSettings(string CompanyName)
        {
            GridViewSettings gridviewSettings = new GridViewSettings();
            gridviewSettings.Name = "TransferEmployeeReport";
            // gridviewSettings.CallbackRouteValues = new { Controller = "Home", Action = "NewEmployeeReportGridView" };
            gridviewSettings.KeyFieldName = "Id";
            gridviewSettings.Columns.Add("EmployeeName").Caption = "Nhân viên";            
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Gender";
                col.Caption = "Giới tính";
                col.ColumnType = MVCxGridViewColumnType.ComboBox;
                ComboBoxProperties comboBoxPro = col.PropertiesEdit as ComboBoxProperties;
                comboBoxPro.DataSource = new SelectList(new[]{
                        new SelectListItem{Text = "Nam",Value=HNGHRMS.Model.Enums.Gender.Male.ToString()},
                        new SelectListItem{Text = "Nữ", Value=HNGHRMS.Model.Enums.Gender.Female.ToString()}
                    }, "Value", "Text");
            });
            gridviewSettings.Columns.Add("Nationality").Caption = "Quốc tịch";
            gridviewSettings.Columns.Add("CompanyName").Caption = "Công ty";
            gridviewSettings.Columns.Add("PositionName").Caption = "Chức vụ";
            gridviewSettings.Columns.Add("DepartementName").Caption = "Phòng ban";
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "TransferDate";
                col.Caption = "Ngày điều chuyển";
                col.ColumnType = MVCxGridViewColumnType.DateEdit;
                col.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });
            gridviewSettings.Columns.Add("OldCompanyName").Caption = "Công ty cũ";
            gridviewSettings.Columns.Add("OldPositionName").Caption = "Chức vụ cũ";
            gridviewSettings.Columns.Add("OldDepartement").Caption = "Phòng ban cũ";
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Salary";
                col.Caption = "Mức lương";
                col.PropertiesEdit.DisplayFormatString = "c0";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "OldSalary";
                col.Caption = "Mức lương cũ";
                col.PropertiesEdit.DisplayFormatString = "c0";
            });
            gridviewSettings.Columns.Add(col =>
            {
                col.FieldName = "Reason";
                col.Caption = "Lý do thôi việc";
            });
            return gridviewSettings;
        }
    }
}