using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using Tarefando.Data.Model;
using System;

namespace Tarefando.Data.Dao
{
    public class Banco
    {
        public string DbPath { get; set; }
        public SQLiteConnection Db { get; set; }
        public Banco()
        {
            DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "tarefando.db3");
            Db = new SQLiteConnection(DbPath);
        }
        public void InserirTarefa(Tarefa tarefa)
        {
            if(Validador(tarefa.Nome))
            Db.Insert(tarefa);
            else
            throw new Exception("Tarefa existente");
        }
        private bool Validador(string nome)
        {
            var valida = Db.Table<Tarefa>().Where(x => x.Nome == nome).FirstOrDefault();
            if (valida == null)
            {
                return true;
            }
            return false;
        }
        public void ExcluirTarefa(int id)
        {
            Db.Delete(this.BuscarTarefa(id));
        }
        public Tarefa BuscarTarefa(int id)
        {
            return Db.Table<Tarefa>().Where(x => x.ID == id).FirstOrDefault();
        }
        public List<Tarefa> BuscarPorNome(string nome)
        {
            return Db.Table<Tarefa>().Where(x => x.Nome == nome).ToList();
        }

        public List<Tarefa> BuscarTodasIncompletas()
        {
            return Db.Table<Tarefa>().Where(x => x.Completa == false).ToList();
        }
        public List<Tarefa> BuscarTodasCompletas()
        {
            return Db.Table<Tarefa>().Where(x => x.Completa == true).ToList();
        }
        public void Update(Tarefa tarefa)
        {
            Db.Update(tarefa);
        }
    }
}