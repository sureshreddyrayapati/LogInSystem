using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace MyLogInSystem
{
    public class SBIBank
    { 
        public static int rnd()
        {
            Random random = new Random();
            List<int> list = Enumerable.Range(1, 9).ToList();
            for(int i=list.Count-1; i>=0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            int randomNumber = list.Take(5).Aggregate((result, list1) => result * 10 + list1);
            return randomNumber;
        }
        public static void Display(string acc)
        {
            using(CompanyEntities cs=new CompanyEntities())
            {
                var bk=cs.Banks.Where(d=>d.AadharNo.Equals(acc)).FirstOrDefault();
                var em=cs.LogInCdts.Where(e=>e.PhoneNumber.Equals(bk.PhoneNumber)).FirstOrDefault();
                if (bk != null)
                {
                    Console.WriteLine("Account Number  :" + bk.AccountNumber);
                    Console.WriteLine("Aadhar Number   :XXXXXXXX" + bk.AadharNo.Substring(8));
                    Console.WriteLine("First Name      :" + em.FirstName);
                    Console.WriteLine("Last Name       :" + em.LastName);
                    Console.WriteLine("Total Balence   :" + bk.Balence);
                    Console.WriteLine("Email Id        :" + em.EMailID);
                    Console.WriteLine("1 for Contine 2 for Exit");
                    int ch = Convert.ToInt32(Console.ReadLine());
                    if (ch == 1)
                    {
                        Program.MainMenu();
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("Please Enter Correct Aadhar Number");
                    string aadh=Console.ReadLine();
                    Display(aadh);
                }
            }
        }
        public static void CheckBalence(decimal acc)
        {
            using(CompanyEntities n=new CompanyEntities())
            {
                var demo=n.Banks.Where(e=>e.AccountNumber==acc).FirstOrDefault();
                if (demo != null)
                {
                    Console.WriteLine("Total Available Balence  :" + demo.Balence);
                    Console.WriteLine("1 for Contine 2 for Exit");
                    int ch = Convert.ToInt32(Console.ReadLine());
                    if (ch == 1)
                    {
                        Program.MainMenu();
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("Correctly Enter Your Account number");
                    decimal ac=Convert.ToDecimal(Console.ReadLine());
                    CheckBalence(ac);
                }
            }
        }
        public static void newaccount(string aadhar,string ph)
        {
            int n = rnd();
            double openAmount = 500;
            using(CompanyEntities ce=new CompanyEntities())
            {
                Bank bank = new Bank()
                {
                    AccountNumber= n,
                    AadharNo=aadhar,
                    PhoneNumber=ph,
                    Balence=openAmount
                };
                ce.Banks.Add(bank);
                ce.SaveChanges();
                Console.WriteLine("Account created Succusfull\n");
                Console.WriteLine("Your Account number is  :"+n);
                Program.MainMenu();
            }
        }
        public static void Withdrow(decimal acc)
        {
            double amo = 0;
            using (CompanyEntities ce=new CompanyEntities())
            {
                var me=ce.Banks.Where(e=>e.AccountNumber==acc).FirstOrDefault();
                if (me != null)
                {
                    Console.WriteLine("enter amount");
                    double en = Convert.ToDouble(Console.ReadLine());
                    if (en > 500 && 500 < me.Balence)
                    {
                        amo = (double)me.Balence - en;
                        me.Balence = amo;
                        ce.SaveChanges();
                        Console.WriteLine("Remaing Balence  :" + amo);
                        Console.WriteLine("1 for Contine 2 for Exit");
                        int ch = Convert.ToInt32(Console.ReadLine());
                        if (ch == 1)
                        {
                            Program.MainMenu();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter min 500 and account balence is less then 500");
                        Withdrow(acc);
                    }
                }
                else
                {
                    Console.WriteLine("Correctly Enter Your Account number");
                    decimal ac = Convert.ToDecimal(Console.ReadLine());
                    Withdrow(ac);
                }
            } 
        }
        public static void Debit(decimal acc)
        {
            double amo = 0;
            using (CompanyEntities ce=new CompanyEntities())
            {
                var me = ce.Banks.Where(e => e.AccountNumber == acc).FirstOrDefault();
                if (me != null)
                {
                    Console.WriteLine("enter amount");
                    double en = Convert.ToDouble(Console.ReadLine());
                    amo = (double)me.Balence + en;
                    me.Balence = amo;
                    ce.SaveChanges();
                    Console.WriteLine("Total Balence  :" + amo);
                    Console.WriteLine("1 for Contine 2 for Exit");
                    int ch = Convert.ToInt32(Console.ReadLine());
                    if (ch == 1)
                    {
                        Program.MainMenu();
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("Correctly Enter Your Account number");
                    decimal ac = Convert.ToDecimal(Console.ReadLine());
                    Debit(ac);
                }
            }
        }
    }
}
