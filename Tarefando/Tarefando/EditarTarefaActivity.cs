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
using Tarefando.Data.Dao;
using Tarefando.Data.Model;

namespace Tarefando
{
    [Activity(Label = "EditarTarefaActivity", Theme = "@style/CustomActionBarTheme")]
    public class EditarTarefaActivity : Activity
    {
        private Banco _banco;
        private Button salvar;
        private EditText nome;
        private EditText obs;
        private Button mVoltar;
        private int _id;
        private Tarefa tarefa;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetCustomView(Resource.Layout.ActionBarMenu);
            ActionBar.SetDisplayShowCustomEnabled(true);
            mVoltar = FindViewById<Button>(Resource.Id.btnMenuVoltar);
            FindViewById<Button>(Resource.Id.btnMenuCumpridas).Visibility = ViewStates.Invisible;
            FindViewById<TextView>(Resource.Id.txtMenuTexto).Text = "Criar nova tarefa";
            SetContentView(Resource.Layout.CriarTarefa);
            // Create your application here
            _banco = new Banco();
            _id = Intent.GetIntExtra("id", 123);
            mVoltar.Click += MVoltar_Click;
            nome = FindViewById<EditText>(Resource.Id.edtCriarNome);
            obs = FindViewById<EditText>(Resource.Id.edtCriarObs);
            salvar = FindViewById<Button>(Resource.Id.btnCriarSalvar);
            EncontarTarefaEditada();
            nome.Text = tarefa.Nome;
            obs.Text = tarefa.Descricao;
            salvar.Click += Salvar_Click;
        }

        private void EncontarTarefaEditada()
        {
            tarefa = new Tarefa();
            var id = _id;
            tarefa = _banco.BuscarTarefa(id);
        }

        private void MVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                tarefa.DataCriada = DateTime.Now.ToString("dd/MM/yyyy");
                tarefa.Nome = nome.Text;
                tarefa.Descricao = obs.Text;

                _banco.Update(tarefa);
                this.Finish();
                throw new Exception("Tarefa atualizada com sucesso");
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}