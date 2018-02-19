using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuAmigo.Model;
using SQLite;
using Xamarin.Forms;

namespace MeuAmigo.DAO
{
    internal class Contexto
    {
        internal SQLiteConnection database;
        internal Contexto()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();


            database.CreateTable<Usuario>();

        }
    }
}
