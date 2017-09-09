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
using Tarefando.Data.Model;
using Tarefando.Data.Dao;

namespace Tarefando
{
    [Activity(Label = "TarefasCumpridasActivity", Theme = "@style/CustomActionBarTheme")]
    public class TarefasCumpridasActivity : Activity
    {
        private FragmentoTarefas fragmento;
        private TextView texto;
        private Button btnVoltar;
        private Button btnC;
        private Banco _banco;
        private ListView _listView;
        private List<Tarefa> tarefas;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Settar a barra de cima do app
            ActionBar.SetCustomView(Resource.Layout.ActionBarMenu);
            ActionBar.SetDisplayShowCustomEnabled(true);

            // settar a view especifica dessa activity
            SetContentView(Resource.Layout.TarefasCumpridas);
            // Create your application here
            btnC = FindViewById<Button>(Resource.Id.btnMenuCumpridas);
            texto = FindViewById<TextView>(Resource.Id.txtMenuTexto);
            _listView = FindViewById<ListView>(Resource.Id.llTarefasC);
            texto.Text = "Afazeres cumpridos";

            btnVoltar = FindViewById<Button>(Resource.Id.btnMenuVoltar);

            btnC.Visibility = ViewStates.Invisible;
            btnVoltar.Click += BtnVoltar_Click;

            _listView.ItemLongClick += _listView_ItemLongClick;
            _listView.ItemClick += _listView_ItemClick;

            CriarBancoDeDados();
            EncontrarTarefas();
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle(tarefas[e.Position].Nome);
            alerta.SetMessage(tarefas[e.Position].Descricao);
            RunOnUiThread(() => { alerta.Show(); });
        }

        private void _listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            FragmentTransaction fragment = FragmentManager.BeginTransaction();
            fragmento = new FragmentoTarefas(tarefas[e.Position].ID, false, this);
            RunOnUiThread(() => { fragmento.Show(fragment, "Tarefas"); });
            EncontrarTarefas();
        }

        private void CriarBancoDeDados()
        {
            _banco = new Banco();
            _banco.Db.CreateTable<Tarefa>();
        }

        private void EncontrarTarefas()
        {
            tarefas = _banco.BuscarTodasCompletas();
            ListViewAdapter adapter = new ListViewAdapter(this, tarefas);
            _listView.Adapter = adapter;
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}