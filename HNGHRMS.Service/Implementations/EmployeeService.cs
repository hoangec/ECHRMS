using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Infrastructure.Extensions;
using HNGHRMS.Infrastructure.Domain;
using HNGHRMS.Data.Repository;
using HNGHRMS.Model.Enums;
using HNGHRMS.Service.Messaging;
using System.Data;
using System.Data.OleDb;
using HNGHRMS.Service.Interface;
using HNGHRMS.Infrastructure.Logs;

namespace HNGHRMS.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeSalaryComponentRepository empSalaryComponentRepository;
        private readonly IInsuranceRepository insuranceRepository;
        private readonly IPositionRepository positionRepository;
        private IUnitOfWork unitOfWork;
        public static int WriteExcelToDBProcess =0;
        public EmployeeService(IPositionRepository positionRepository,IInsuranceRepository insuranceRepository,IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IEmployeeSalaryComponentRepository empSalaryComponentRepository,ICompanyRepository companyRepository)
        {
            this.employeeRepository = employeeRepository;
            this.empSalaryComponentRepository = empSalaryComponentRepository;
            this.companyRepository = companyRepository;
            this.insuranceRepository = insuranceRepository;
            this.positionRepository = positionRepository;
            this.unitOfWork = unitOfWork;
        }

        #region EmployeeService Members

        public Employee GetEmployee(int id)
        {                        
            var employee = employeeRepository.GetById(id);            
            return employee;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = employeeRepository.GetAll();
            return employees;
            
        }

        public IEnumerable<Employee> GetEmployeesByUser(ApplicationUser User)
        {
            IEnumerable<Employee> employees;
            if (User != null & !User.CompaniesRole.Contains("null"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<int> companiesList = js.Deserialize<List<int>>(User.CompaniesRole);

                employees = from e in employeeRepository.GetMany(co => companiesList.Contains(co.CompanyId))
                            select e;
                return employees;

            }
            return employees = employeeRepository.GetAll();
        }
        public IEnumerable<Employee> GetCurrentEmployeesWorking()
        {
            var employees = employeeRepository.GetMany(em => em.Status != EmployeeStatus.Terminated).OrderByDescending(emp => emp.Id);
            return employees;
        }

        public IEnumerable<Employee> GetCurrentEmployeesWorkingByUser(ApplicationUser User)
        {
            IEnumerable<Employee> employees;
            if (User != null & !User.CompaniesRole.Contains("null"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<int> companiesList = js.Deserialize<List<int>>(User.CompaniesRole);

                employees = from e in employeeRepository.GetMany(em => companiesList.Contains(em.CompanyId) && em.Status != EmployeeStatus.Terminated)
                            select e;
                return employees;

            }
            return employees = employeeRepository.GetAll();
        }
        public IEnumerable<Employee> GetNewEmployeesCompanyByDate(int CompanyId,DateTime StartDate,DateTime EndDate)
        {
            var employees = employeeRepository.GetMany(em =>em.Company.Id == CompanyId && em.Status != EmployeeStatus.Terminated)
                .Where(em=> em.JoinedDate.CompareBetweenDateByMonthAndYear(StartDate,EndDate));
            return employees;
        }


        public IEnumerable<Employee> GetCurrentEmployeesByCompany(int companyId,DateTime? date)
        {
            IEnumerable<Employee> employees;
            if (date.HasValue)
            {
                employees = from e in employeeRepository.GetMany(em => em.Company.Id == companyId && em.Status == EmployeeStatus.Present)
                                    where e.JoinedDate.CompareDateByMonthAndYear(date.Value) == true
                                select e;
            }
            else
            {
                employees = from e in employeeRepository.GetMany(em => em.Company.Id == companyId && em.Status == EmployeeStatus.Present)
                                select e;
            }
            
            return employees;
        }

        public IEnumerable<Employee> GetCurrentEmployeesBySpecifyCompany(List<int> companyIdList, DateTime? date)
        {
            IEnumerable<Employee> employees;
            if (date.HasValue)
            {
                employees = from e in employeeRepository.GetMany(em => companyIdList.Contains(em.CompanyId) && em.JoinedDate.CompareDateByMonthAndYear(date.Value) && em.Status == EmployeeStatus.Present)                       
                                select e;
            }
            else
            {
                employees = from e in employeeRepository.GetMany(em => companyIdList.Contains(em.CompanyId) && em.Status == EmployeeStatus.Present)
                                select e;
            }

            return employees;
        }

        public IEnumerable<Employee> GetTerminatedEmployeesByCompany(int companyId,DateTime date)
        {
            var employees = from e in employeeRepository.GetMany(em => em.Company.Id == companyId && em.Status == EmployeeStatus.Terminated)
                            where(e.Termination.TerminationDate.CompareDateByMonthAndYear(date))
                            select e;
            return employees;
        }

        public CreateEmployeeResponse CreateEmployee(Employee employee)
        {
            CreateEmployeeResponse response = new CreateEmployeeResponse();
            try
            {
                
                //employee.Validate();

                if (employee.GetBrokenRules().Count() == 0)
                {
                    EmployeeSalaryComponents empSalaryComponet = new EmployeeSalaryComponents()
                    {
                        SalaryComponentName = "Lương cơ bản",
                        IsSalary = true,
                        IsExtra = true,
                        IsMainSalary = true,
                        StartApplyDate = employee.JoinedDate,
                        SalaryPayFrequency = SalaryPayFerequency.Monthly,
                        EndApplyDate = DateTime.MaxValue,
                        Amount = employee.Salary,
                        EmployeeId = employee.Id,
                    };
                    employeeRepository.Add(employee);
                    empSalaryComponentRepository.Add(empSalaryComponet);
                    SaveEmployee();
                    response.Status = true;
                    response.Message = employee.Id.ToString();
                }
                else {
                    response.Status = false;
                    string errors = "";
                    foreach (BrokenRule rule in employee.GetBrokenRules())
                    {
                        errors += rule.Rule + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Errors";
            }
            return response;
        }

        public void EditEmployee(Employee employee)
        {
            employeeRepository.Update(employee);
            SaveEmployee();
        }
        public void DeleteEmployee(Employee employee)
        {
            employeeRepository.Delete(employee);
            SaveEmployee();
        }

        public void SaveEmployee()
        {
            unitOfWork.Commit();
        }
        #endregion

        public GetEmployeesImportReadExcelFileResponse GetEmployeesImportExcelFile(string FilePath,int CompanyId,int PositionId)
        {
            GetEmployeesImportReadExcelFileResponse response = new GetEmployeesImportReadExcelFileResponse();
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=\"Excel 8.0;\"", FilePath);
            //string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties='Excel 8.0;HDR=Yes'", FilePath);
           
            DataTable table = new DataTable();
            table.Columns.Add("EMPLOYEECODE", typeof(string));
            table.Columns.Add("LASTNAME", typeof(string));
            table.Columns.Add("FIRSTNAME", typeof(string));            
            table.Columns.Add("GENDER", typeof(bool));
            table.Columns.Add("BIRTHDAY", typeof(DateTime));
            table.Columns.Add("IDENTITYNO", typeof(string));
            table.Columns.Add("ISSUEDATE", typeof(DateTime));
            table.Columns.Add("NATIONAL", typeof(string));
            table.Columns.Add("ADDRESS", typeof(string));
            table.Columns.Add("DEPARTMENT", typeof(string));
            table.Columns.Add("POSITION", typeof(int));
            table.Columns.Add("COMPANY", typeof(int));
            table.Columns.Add("JOINDATE", typeof(DateTime));
            table.Columns.Add("SALARY", typeof(Double));
            table.Columns.Add("INSURANCE", typeof(DateTime));
            table.Columns.Add("INVALID", typeof(bool));
            //
            int invalidCount = 0;
            double totalSalary = 0;
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                adapter.Fill(table);                      
                foreach (DataRow row in table.Rows)
                {
                    row["POSITION"] = PositionId;
                    row["COMPANY"] = CompanyId;
                    if (!CheckRowInValid(row))
                    {
                        invalidCount++;                       
                        row["INVALID"] = true;
                    }
                    else 
                    {                       
                        row["INVALID"] = false;
                        totalSalary += double.Parse(row["SALARY"].ToString());
                    }                   
                }
                response.Status = true;
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                Log.WriteError(ex.Message);
            }               
            response.TableFormExcelFile = table;
            response.TotalInValidRecord = invalidCount;
            response.TotalRecord = table.Rows.Count;
            response.TotalSalary = totalSalary;
          
            return response;
        }

        public CreateEmployeesImportExcelToDBResponse CreateEmployeesFromImportExcelToDB(string FilePath, int CompanyId, int PositionId)
        {
            CreateEmployeesImportExcelToDBResponse response = new CreateEmployeesImportExcelToDBResponse();
            GetEmployeesImportReadExcelFileResponse employeeExcel = GetEmployeesImportExcelFile(FilePath,CompanyId,PositionId);
            int inserted = 0;
            Double totalSalary = 0;
            WriteExcelToDBProcess = 0;
            int totalRows = employeeExcel.TableFormExcelFile.Rows.Count;
            if (employeeExcel.Status)
            {                               
                foreach (DataRow row in employeeExcel.TableFormExcelFile.Rows)
                {


                    Employee emp = new Employee(row);
                    DateTime insuranceDate = DateTime.Now;                  
                    employeeRepository.Add(emp);
                    if (!string.IsNullOrEmpty(row["INSURANCE"].ToString()) && DateTime.TryParse(row["INSURANCE"].ToString(), out insuranceDate))
                    {
                        Insurance insuranceInserted = new Insurance();
                        Company comp = companyRepository.GetById(emp.CompanyId);
                        Position position = positionRepository.GetById(emp.PositionId);
                        double insuranceRate = position.InsuranceRate;
                        double companyRate = comp.CompanyInsuranceRatePercent;
                        insuranceInserted.Values = companyRate * insuranceRate;
                        insuranceInserted.InsuranceNo = string.Format("BH-{0}",emp.EmployeeCode);
                        insuranceInserted.IsMandatory = true;
                        insuranceInserted.EmployeeId = emp.Id;
                        insuranceInserted.DateOfIssue = insuranceDate;                    
                        insuranceRepository.Add(insuranceInserted);
                        emp.MadatoryInsurance = companyRate * insuranceRate;
                        emp.MadotoryInsuranceDate = insuranceDate;
                    }                   
                    EmployeeSalaryComponents empSalaryComponet = new EmployeeSalaryComponents()
                    {
                        SalaryComponentName = "Lương cơ bản",
                        IsSalary = true,
                        IsExtra = true,
                        IsMainSalary = true,
                        StartApplyDate = emp.JoinedDate,
                        SalaryPayFrequency = SalaryPayFerequency.Monthly,
                        EndApplyDate = DateTime.MaxValue,
                        Amount = emp.Salary,
                        EmployeeId = emp.Id,
                    };
                    empSalaryComponentRepository.Add(empSalaryComponet);       
                    SaveEmployee();
                    inserted++;
                    totalSalary += emp.Salary;
                    WriteExcelToDBProcess = Convert.ToInt32(Convert.ToDouble(inserted) / Convert.ToDouble(totalRows) * 100);                   
                }                
            }
            response.TotalRecorded = totalRows;
            response.TotalInserted = inserted;
            response.TotalSalary = totalSalary;
            return response;
        }
        private bool CheckRowInValid(DataRow row)
        {
            bool inValid = true;
            bool booleType;
            DateTime datetimeType;
            Double doubleType;
            inValid &= !string.IsNullOrEmpty(row["EMPLOYEECODE"].ToString());
            inValid &= !string.IsNullOrEmpty(row["LASTNAME"].ToString());
            inValid &= !string.IsNullOrEmpty(row["FIRSTNAME"].ToString());
            inValid &= bool.TryParse(row["GENDER"].ToString(), out booleType);
            inValid &= DateTime.TryParse(row["ISSUEDATE"].ToString(), out datetimeType);
            inValid &= !string.IsNullOrEmpty(row["NATIONAL"].ToString());
            inValid &= !string.IsNullOrEmpty(row["DEPARTMENT"].ToString());
            inValid &= DateTime.TryParse(row["JOINDATE"].ToString(), out datetimeType);
            inValid &= Double.TryParse(row["SALARY"].ToString(), out doubleType);
            inValid &= row["INSURANCE"].ToString() != "" ? DateTime.TryParse(row["INSURANCE"].ToString(), out datetimeType) : true;
            //
          
            if (inValid)
            {
                string employeeCode = row["EMPLOYEECODE"].ToString();
                var exits = employeeRepository.GetMany(emp => emp.EmployeeCode == employeeCode).FirstOrDefault();
                if (exits != null)
                {
                    inValid &= false;
                }
            }
            return inValid;
        }

        public Double GetEmployeeRealSalaryAtDate(Employee Employee,DateTime Date)
        {
            double result = 0;
            if (Employee != null)
            {
                double baseSalary = Employee.Salary;
                double madatoryInsurance = Employee.MadatoryInsurance;
                double extraSalary =  0;
                foreach (EmployeeSalaryComponents empSalary in Employee.EmployeeSalaryComponents)
                { 
                    if(Date.CompareBetweenDateByMonthAndYear(empSalary.StartApplyDate,empSalary.EndApplyDate) && !empSalary.IsSalary)
                    {
                        extraSalary += empSalary.Amount;
                    }
                }
                result = baseSalary + madatoryInsurance + extraSalary;
            }
            return result;
        }

        public String GetEmployeeCodeByCompanyId(int Id)
        {
            string result = "";
            Company cmp = companyRepository.GetById(Id);
            if (cmp != null)
            {
                string companyCode = cmp.CompanyCode;
                string maxEmployee = employeeRepository.GetMany(emp => emp.CompanyId == Id).Max(emp2 => emp2.EmployeeCode);
                if (maxEmployee != null)
                {
                    string currentEmployeeCode = maxEmployee.Substring(companyCode.Length);
                    long nexEMployeeCode;
                    if (long.TryParse(currentEmployeeCode, out nexEMployeeCode))
                    {
                        nexEMployeeCode = nexEMployeeCode + 1;
                        if (nexEMployeeCode >= cmp.NumberCodeStarRange && nexEMployeeCode <= cmp.NumberCodeEndRange)
                        {
                            result = companyCode + nexEMployeeCode.ToString();
                        }
                        else
                        {
                            result = "OFR";
                        }
                    }
                }
                else {
                    result = companyCode + cmp.NumberCodeStarRange;
                }
            }
            return result;
        }

        public Employee GetEmployeeByEmployeeCode(string EmployeeCode)
        {
            return employeeRepository.GetMany(em => em.EmployeeCode == EmployeeCode).FirstOrDefault();
        }
    }
}
