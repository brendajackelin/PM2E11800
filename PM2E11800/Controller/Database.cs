using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PM2E11800.Models;

namespace PM2E11800.Controller
{
    public class Database
    {
        readonly SQLiteAsyncConnection db;

        //Constructor
        public Database(String pathdb)
        {
            //Crear conexion a la BD
            db = new SQLiteAsyncConnection(pathdb);

            //Crear tabla sitios en SQLite
            db.CreateTableAsync<sitios>().Wait();
        }

        //READ
        public Task<List<sitios>> ObtenerListaUbicaciones()
        {
            return db.Table<sitios>().ToListAsync();

        }

        //READ
        public Task<sitios> ObtenerUbicacion(int pcodigo)
        {
            return db.Table<sitios>()
                .Where(i => i.id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarUbicacion(sitios ubicacion)
        {
            if (ubicacion.id != 0)
            {
                return db.UpdateAsync(ubicacion);
            }
            else
            {

                return db.InsertAsync(ubicacion);
            }
        }

        //DELETE
        public Task<int> EliminarUbicacion(sitios ubicacion)
        {
            return db.DeleteAsync(ubicacion);
        }
    }
}
