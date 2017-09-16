using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Tarefando.Data.Dao;
using Tarefando.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Tarefando
{
    [Activity(Label = "Tarefando", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
    public class MainActivity : Activity
    {
        private FragmentoTarefas fragmento;
        private TextView texto;
        private Button btnVoltar;
        private Button btnProximaTela;
        private Banco _banco;
        private Button criarTarefa;
        private ListView _listView;
        private List<Tarefa> tarefas;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ActionBar.SetCustomView(Resource.Layout.ActionBarMenu);
            ActionBar.SetDisplayShowCustomEnabled(true);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            _listView = FindViewById<ListView>(Resource.Id.lvMainTarefas);

            btnProximaTela = FindViewById<Button>(Resource.Id.btnMenuCumpridas);
            texto = FindViewById<TextView>(Resource.Id.txtMenuTexto);
            criarTarefa = FindViewById<Button>(Resource.Id.btnMainC);
            texto.Text = "Lista de afazeres";

            btnVoltar = FindViewById<Button>(Resource.Id.btnMenuVoltar);

            btnVoltar.Visibility = ViewStates.Invisible;

            btnProximaTela.Click += BtnProximaTela_Click;
            //btnVoltar.Click += BtnVoltar_Click;
            criarTarefa.Click += CriarTarefa_Click;

            _listView.ItemClick += _listView_ItemClick;
            _listView.ItemLongClick += _listView_ItemLongClick;

            CriarBancoDeDados();
            EncontrarTarefas();

        }

        private void _listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            FragmentTransaction fragment = FragmentManager.BeginTransaction();
            fragmento = new FragmentoTarefas(tarefas[e.Position].ID, true, this);
            RunOnUiThread(() => { fragmento.Show(fragment, "Tarefas"); });
            EncontrarTarefas();
        }


        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle(tarefas[e.Position].Nome);
            alerta.SetMessage(tarefas[e.Position].Descricao);
            RunOnUiThread(() => { alerta.Show(); });
        }

        private void CriarTarefa_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CriarTarefaActivity));
        }

        private void EncontrarTarefas()
        {
            tarefas = _banco.BuscarTodasIncompletas();
            ListViewAdapter adapter = new ListViewAdapter(this, tarefas);
            _listView.Adapter = adapter;
        }

        private void CriarBancoDeDados()
        {
            _banco = new Banco();
            _banco.Db.CreateTable<Tarefa>();
        }

        protected override void OnStart()
        {
            EncontrarTarefas();
            base.OnStart();
        }

        private void BtnProximaTela_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(TarefasCumpridasActivity));
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}

