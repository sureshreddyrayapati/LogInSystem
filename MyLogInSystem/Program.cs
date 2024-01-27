using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace MyLogInSystem
{
    public class Program
    {
        public static void BankMenu()
        {
            Console.WriteLine("\n.......Welcome To SBI Bank........\n");
            Console.WriteLine("Do You have account type 'YES' or 'NO'");
            string choice=Console.ReadLine();
            if (choice.Equals("YES", StringComparison.InvariantCultureIgnoreCase))
            {
                MainMenu();
            }
            else if (choice.Equals("No", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Lets create Account");
                Console.WriteLine("Enter Your aadhar number");
                string aa=Console.ReadLine();
                Console.WriteLine("Enter phone Number");
                string pha=Console.ReadLine();
                SBIBank.newaccount(aa, pha);
            }
        }
        public static void MainMenu()
        {
            Console.WriteLine(".........Our Bank Services........");
            Console.WriteLine("1. Print passBook\n2. Deposite Money\n3. Withdrow Money\n4. Check Balence");
            int n=Convert.ToInt32(Console.ReadLine());
            switch (n)
            {
                case 1: Console.WriteLine("Enter Your Aadhar card number");
                    string ad=Console.ReadLine();
                    SBIBank.Display(ad);
                    break;
                case 2: Console.WriteLine("Enter Your Account number");
                    decimal mo=Convert.ToDecimal(Console.ReadLine());
                    SBIBank.Debit(mo);
                    break;
                case 3: Console.WriteLine("Enter Your Account number");
                    decimal moo= Convert.ToDecimal(Console.ReadLine());
                    SBIBank.Withdrow(moo);
                    break;
                case 4: Console.WriteLine("Enter Your Account number");
                    decimal sc=Convert.ToDecimal(Console.ReadLine());
                    SBIBank.CheckBalence(sc);
                    break;
                default: Console.WriteLine("Inavlid Choice please select again");
                    MainMenu();
                    break;
            }
        }
        public void newUser()
        {
            Console.WriteLine("welcome new Customer.....please Register with us\n");
            Console.WriteLine("Please enter Correct phone Number with 10 digits");
            Console.WriteLine("Enter Your First Name");
            string fn=Console.ReadLine();
            Console.WriteLine("Enter Your Last Name");
            string ln=Console.ReadLine();
            Console.WriteLine("Enter Your Phone Number");
            string ph=Console.ReadLine();
            Console.WriteLine("Enter Your Email Id");
            string em=Console.ReadLine();
            Console.WriteLine("Set Your Password");
            string pw=Console.ReadLine();
            LogIn.Insert(ph,fn,ln,pw,em);
        }
        public void Menu()
        {
            Console.WriteLine("1.New User\n2.LogIn");
            int n = Convert.ToInt32(Console.ReadLine());
            switch(n)
            {
                case 1: newUser();
                    break;
                case 2: Console.WriteLine("Enter Your Mobile Number");
                    string phn=Console.ReadLine();  
                    LogIn.SignUp(phn);
                    break;
               default: Console.WriteLine("Invalid Choice");
                    Menu();
                    break;
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Your Bank......!");
            Program x = new Program();
            x.Menu();

        }
    }
}
