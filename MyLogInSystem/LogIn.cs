using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Policy;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;

namespace MyLogInSystem
{
    public class LogIn
    {
        public static SqlConnection Connect()
        {
            string cc = ConfigurationManager.ConnectionStrings["name"].ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = cc;
            return conn;
        }
        public static void Insert(string phone,string fn,string ln,string pw,string email)
        {
            using(CompanyEntities ce= new CompanyEntities())
            {
                LogInCdt lg = new LogInCdt()
                {
                    PhoneNumber = phone,
                    FirstName = fn,
                    LastName = ln,
                    Passwordd = pw,
                    EMailID = email
                };
                ce.LogInCdts.Add(lg);
                ce.SaveChanges();
                Console.WriteLine("Registation Successfull");
                Console.WriteLine("Please check it once\n");
                DisplayAll(phone);
                Console.WriteLine("type 'OK' if all details are correct else Type 'No' To correct the details");
                string inp=Console.ReadLine();
                if (inp.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Please LogIn");
                    Console.WriteLine("Enter your phone Number");
                    string p=Console.ReadLine();
                    SignUp(p);
                }
                else if (inp.Equals("NO", StringComparison.InvariantCultureIgnoreCase))
                {
                    UpdateD(phone);
                    Console.WriteLine("Please LogIn");
                    Console.WriteLine("Enter Phone Number");
                    string p1 = Console.ReadLine();
                    SignUp(p1);
                }
            }
        }
        public static void DisplayAll(string phone)
        {
            using (CompanyEntities ce = new CompanyEntities())
            {
                var emp = ce.LogInCdts.Where(e => e.PhoneNumber.Equals(phone)).FirstOrDefault();
                if (emp != null)
                {
                    Console.WriteLine("First Name   :" + emp.FirstName);
                    Console.WriteLine("Last Name    :" + emp.LastName);
                    Console.WriteLine("EMail Id     :" + emp.EMailID);
                    Console.WriteLine("Phone Number :" + emp.PhoneNumber);
                    Console.WriteLine("Passwors     :" + emp.Passwordd);
                }
                else
                {
                    Console.WriteLine("Correctly enter Your Phone Number");
                    string ph=Console.ReadLine();
                    DisplayAll(ph);
                }
            }
            
        }
        public static void DetailsByPhone(string phone) 
        { 
            using(CompanyEntities ce=new CompanyEntities())
            {
                var emp = ce.LogInCdts.Where(e => e.PhoneNumber.Equals(phone)).FirstOrDefault();
                if (emp != null)
                {
                    string EMail = emp.EMailID;
                    string Passwordd = emp.Passwordd;
                    Console.WriteLine("EMail Id     :" + EMail);
                    Console.WriteLine("Password     :" + Passwordd);
                    Console.WriteLine("1 for try again and 2 for Exit");
                    int n = Convert.ToInt32(Console.ReadLine());
                    if (n == 1)
                    {
                        SignUp(phone);
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("Enter Correct Phone number");
                    string phoness=Console.ReadLine();
                    DetailsByPhone(phoness);
                }
            }
        }
        public static void SignUp(string phone)
        {
            Program p=new Program();
            using (CompanyEntities ce = new CompanyEntities())
            {
                var emp = ce.LogInCdts.Where(e => e.PhoneNumber.Equals(phone)).FirstOrDefault();
                if(emp != null)
                {
                    Console.WriteLine("We Found Your Details Plase Enter EMail and Password");
                    string email = Console.ReadLine();
                    string password = Console.ReadLine();
                    if (emp.EMailID.Equals(email) && emp.Passwordd.Equals(password))
                    {
                        Console.WriteLine("Welcome");
                        Program.BankMenu();
                    }
                    else
                    {
                        Invalid();
                    }
                }
                else
                {
                    Console.WriteLine("Phone number doesnot Match");
                    Console.WriteLine("Enter Your Phone Number Correctly");
                    string p1= Console.ReadLine();
                    SignUp(p1);
                }
            }
        }
        public static void Invalid()
        {
            Console.WriteLine("Inavlid Password or Email");
            Console.WriteLine("1-->Try again\n2-->forgot password\n3-->Do you want to know your Email and password");
            int n=Convert.ToInt32(Console.ReadLine());
            if (n == 1)
            {
                Console.WriteLine("Enter your Mobile Number Again");
                string phone= Console.ReadLine();
                SignUp(phone);
            }
            else if(n==2)
            {
                Console.WriteLine("Enter Your phone Number");
                string phone= Console.ReadLine();

            }
            else if (n == 3)
            {
                Console.WriteLine("Enter Your Phone Number");
                string phone= Console.ReadLine();
                DetailsByPhone(phone);
            }
        }
        public static void UpdateD(string phone)
        {
            using(CompanyEntities cu=new CompanyEntities())
            {
                LogInCdt hg = cu.LogInCdts.Where(e => e.PhoneNumber.Equals(phone)).FirstOrDefault();
                if (hg != null)
                {
                    Console.WriteLine("Which one do you neeed to update");
                    Console.WriteLine("1. First Name\n2. Last Name\n3. Email\n4. Password");
                    int c = Convert.ToInt32(Console.ReadLine());
                    if (c == 1)
                    {
                        Console.WriteLine("Enter new Name");
                        hg.FirstName = Console.ReadLine();
                        cu.SaveChanges();
                    }
                    else if (c == 2)
                    {
                        Console.WriteLine("Enter new Name");
                        hg.LastName = Console.ReadLine();
                        cu.SaveChanges();
                    }
                    else if (c == 3)
                    {
                        Console.WriteLine("Enter new Email Id");
                        hg.EMailID = Console.ReadLine();
                        cu.SaveChanges();
                    }
                    else if (c == 4)
                    {
                        Console.WriteLine("Enter new Password");
                        hg.Passwordd = Console.ReadLine();
                        cu.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Choice");
                        UpdateD(phone);
                    }
                }
                else
                {
                    Console.WriteLine("Enter Correct Phone number");
                    string phoness = Console.ReadLine();
                    DetailsByPhone(phoness);
                }
            }
        }
    }
}
