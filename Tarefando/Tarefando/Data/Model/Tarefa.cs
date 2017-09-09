using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Tarefando.Data.Model
{
    public class Tarefa
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(30)]
        public string Nome { get; set; }
        [MaxLength(200)]
        public string Descricao { get; set; }
        [MaxLength(10)]
        public string DataCriada { get; set; }
        [MaxLength(2)]
        public bool Completa { get; set; }
        public int Prioridade { get; set; }
    }
}