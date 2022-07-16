using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PM2P2T3.Models
{
    public class audioDB
    {
        readonly SQLiteAsyncConnection db;

        public audioDB(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Audio>().Wait();
        }

        public Task<List<Audio>> ObtenerListaAudios()
        {
            return db.Table<Audio>().ToListAsync();
        }

        public Task<Audio> ObtenerAudio(int pcodigo)
        {
            return db.Table<Audio>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarAudio(Audio audio)
        {
            if (audio.Id != 0)
            {
                return db.UpdateAsync(audio);
            }
            else
            {
                return db.InsertAsync(audio);
            }
        }

        public Task<int> EliminarAudio(Audio audio)
        {
            return db.DeleteAsync(audio);
        }


    }
}
