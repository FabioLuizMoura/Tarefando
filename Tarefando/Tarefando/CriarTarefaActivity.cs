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
    [Activity(Label = "CriarTarefaActivity", Theme = "@style/CustomActionBarTheme")]
    public class CriarTarefaActivity : Activity
    {
        private Banco _banco;
        private Button salvar;
        private EditText nome;
        private EditText obs;
        private Button mVoltar;
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

            mVoltar.Click += MVoltar_Click;

            nome = FindViewById<EditText>(Resource.Id.edtCriarNome);
            obs = FindViewById<EditText>(Resource.Id.edtCriarObs);
            salvar = FindViewById<Button>(Resource.Id.btnCriarSalvar);

            salvar.Click += Salvar_Click;


        }

        private void MVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                Tarefa tarefa = new Tarefa() { Completa = false, DataCriada = DateTime.Now.ToString("dd/MM/yyyy"), Descricao = obs.Text, Nome = nome.Text };
                _banco.InserirTarefa(tarefa);
                this.Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}