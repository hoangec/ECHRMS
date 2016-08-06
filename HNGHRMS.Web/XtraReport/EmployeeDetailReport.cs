using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HNGHRMS.Model.Models;
/// <summary>
/// Summary description for EmployeeDetailReport
/// </summary>
public class EmployeeDetailReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox xrPictureBoxEmpPhoto;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCellEmpFullName;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCellEmpCode;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCellEmpIdentity;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCellEmpGender;
    private XRLabel xrLabel1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public EmployeeDetailReport(Employee Employee)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        this.xrTableCellEmpFullName.DataBindings.Add("Text", Employee,"FullName");
        this.xrTableCellEmpCode.DataBindings.Add("Text", Employee, "EmployeeNo");
        this.xrTableCellEmpIdentity.DataBindings.Add("Text", Employee, ".Identity.IdentityNo");
        this.xrTableCellEmpGender.Text = (Employee.Gender == HNGHRMS.Model.Enums.Gender.Female) ? "Nữ" : "Nam";
        this.xrPictureBoxEmpPhoto.DataBindings.Add("Image", Employee, "Photo");
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeDetailReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPictureBoxEmpPhoto = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpFullName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpIdentity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellEmpGender = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBoxEmpPhoto,
            this.xrTable1});
            this.Detail.HeightF = 578.9166F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBoxEmpPhoto
            // 
            this.xrPictureBoxEmpPhoto.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBoxEmpPhoto.Image")));
            this.xrPictureBoxEmpPhoto.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 21.45831F);
            this.xrPictureBoxEmpPhoto.Name = "xrPictureBoxEmpPhoto";
            this.xrPictureBoxEmpPhoto.SizeF = new System.Drawing.SizeF(138.5417F, 127.1667F);
            this.xrPictureBoxEmpPhoto.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(173.9584F, 21.45831F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
            this.xrTable1.SizeF = new System.Drawing.SizeF(438.9583F, 127.1667F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCellEmpFullName});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.447872694203568D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Họ tên: ";
            this.xrTableCell1.Weight = 0.64167069782601049D;
            // 
            // xrTableCellEmpFullName
            // 
            this.xrTableCellEmpFullName.Name = "xrTableCellEmpFullName";
            this.xrTableCellEmpFullName.Text = "xrTableCellEmpFullName";
            this.xrTableCellEmpFullName.Weight = 1.3583293021739895D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCellEmpCode});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.61026325317053709D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Mã nhân viên:";
            this.xrTableCell4.Weight = 0.641670350212385D;
            // 
            // xrTableCellEmpCode
            // 
            this.xrTableCellEmpCode.Name = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.Text = "xrTableCellEmpCode";
            this.xrTableCellEmpCode.Weight = 1.358329649787615D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCellEmpIdentity});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.39915582385910864D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "CMND/Hộ chiếu:";
            this.xrTableCell7.Weight = 0.64167007212148452D;
            // 
            // xrTableCellEmpIdentity
            // 
            this.xrTableCellEmpIdentity.Name = "xrTableCellEmpIdentity";
            this.xrTableCellEmpIdentity.Text = "xrTableCellEmpIdentity";
            this.xrTableCellEmpIdentity.Weight = 1.3583299278785155D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCellEmpGender});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.52517054556024911D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Giới tính:";
            this.xrTableCell10.Weight = 0.64167062830328536D;
            // 
            // xrTableCellEmpGender
            // 
            this.xrTableCellEmpGender.Name = "xrTableCellEmpGender";
            this.xrTableCellEmpGender.Text = "xrTableCellEmpGender";
            this.xrTableCellEmpGender.Weight = 1.3583293716967146D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            this.TopMargin.HeightF = 46.62501F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(26.04167F, 10.00001F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(433.3333F, 23F);
            this.xrLabel1.Text = "CTY CP NÔNG NGHIỆP QUỐC TẾ AGRICO";
            // 
            // EmployeeDetailReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 47, 100);
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
