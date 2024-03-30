using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using Lab3.Domain;
using Lab3.Repository;
using Lab3.Service;
using Lab3.Utility;
using Lab3.Utility.Validation;
using Microsoft.Extensions.Configuration;

namespace Lab3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DbUtils dbUtils = new DbUtils();
            IRepositoryAccount repoAccount = new RepoAccount(dbUtils);
            IRepositoryReservation repoReservation = new RepoReservation(dbUtils);
            IRepositoryTrip repoTrip = new RepoTrip(dbUtils);
            Validator<Account> validatorAccount = new ValidatorAccount();
            Validator<Trip> validatorTrip = new ValidatorTrip();
            Validator<Reservation> validatorReservation = new ValidatorReservation();
            IServiceAccount serviceAccount = new ServiceAccount(repoAccount,validatorAccount);
            IServiceReservation serviceReservation = new ServiceReservation(repoReservation,validatorReservation);
            IServiceTrip serviceTrip = new ServiceTrip(repoTrip,validatorTrip);
            ServiceController serviceController = new ServiceController(serviceAccount,serviceTrip,serviceReservation);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(serviceController));
        }
    }
}